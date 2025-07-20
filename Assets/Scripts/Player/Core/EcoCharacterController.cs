using UnityEngine;
using System.Collections.Generic;

namespace ECO.Player
{
    public class EcoCharacterController : MonoBehaviour
    {
        [Header("Controller References")]
        public EcoInputManager inputManager;
        public EcoEnergySystem energySystem;
        public EcoMovementSystem movementSystem;

        [Header("Camera Rotation Settings")]
        public Camera mainCamera;
        public Transform cameraTarget; // Punto al que la cámara sigue
        public float mouseSensitivity = 2f;
        public float verticalLookLimit = 80f;
        public bool invertY = false;

        [Header("Movement Settings")]
        public float deadZone = 0.1f;
        public float accelerationTime = 0.1f;
        public float decelerationTime = 0.15f;
        public bool strafeMode = false; // Para movimiento lateral sin rotar

        [Header("Rotation Smoothing")]
        public float rotationSpeed = 10f;
        public bool smoothRotation = true;

        private Dictionary<string, IEcoAbility> abilities;
        private Vector2 smoothedMovementInput;
        private Vector2 movementVelocity;

        // Variables para rotación de cámara
        private float mouseX;
        private float mouseY;
        private float verticalRotation;
        private bool cursorLocked = false;

        void Start()
        {
            InitializeComponents();
            InitializeAbilities();
            SetupCursor();
        }

        void InitializeComponents()
        {
            if (inputManager == null)
                inputManager = GetComponent<EcoInputManager>();
            if (energySystem == null)
                energySystem = GetComponent<EcoEnergySystem>();
            if (movementSystem == null)
                movementSystem = GetComponent<EcoMovementSystem>();
            if (mainCamera == null)
                mainCamera = Camera.main;

            // Si no hay cameraTarget, crear uno
            if (cameraTarget == null)
            {
                GameObject target = new GameObject("CameraTarget");
                target.transform.SetParent(transform);
                target.transform.localPosition = Vector3.up * 1.6f; // Altura aproximada de los ojos
                cameraTarget = target.transform;
            }
        }

        void InitializeAbilities()
        {
            abilities = new Dictionary<string, IEcoAbility>
            {
                {"absorb", GetComponent<AbsorberAbility>()},
                {"irradiate", GetComponent<IrradiateAbility>()},
                {"interact", GetComponent<InteractAbility>()}
            };
        }

        void SetupCursor()
        {
            Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !cursorLocked;
        }

        void Update()
        {
            HandleCameraRotation();
            HandleMovementInput();
            HandleCharacterRotation();
            HandleAbilityInput();
            HandleCursorToggle();
        }

        void HandleCameraRotation()
        {
            if (!cursorLocked) return;

            // Obtener input del mouse
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            if (invertY)
                mouseY = -mouseY;

            // Rotar horizontalmente el personaje
            transform.Rotate(Vector3.up * mouseX);

            // Rotar verticalmente la cámara
            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit, verticalLookLimit);

            // Aplicar rotación vertical solo a la cámara
            mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }

        void HandleMovementInput()
        {
            Vector2 rawInput = inputManager.GetAxis("movement");

            // Aplicar zona muerta
            if (rawInput.magnitude < deadZone)
            {
                rawInput = Vector2.zero;
            }
            else
            {
                // Normalizar después de la zona muerta
                rawInput = rawInput.normalized * ((rawInput.magnitude - deadZone) / (1f - deadZone));
            }

            // Suavizar el input
            bool isMoving = rawInput.magnitude > 0;
            float targetTime = isMoving ? accelerationTime : decelerationTime;

            smoothedMovementInput = Vector2.SmoothDamp(
                smoothedMovementInput,
                rawInput,
                ref movementVelocity,
                targetTime
            );

            // Convertir input 2D a movimiento 3D relativo al personaje
            Vector3 moveDirection = CalculateMovementDirection(smoothedMovementInput);

            // Pasar el movimiento al sistema de movimiento
            movementSystem.HandleMovement(new Vector2(moveDirection.x, moveDirection.z));

            // Manejar salto y planeo
            HandleJumpAndGlide();
        }

        Vector3 CalculateMovementDirection(Vector2 input)
        {
            if (strafeMode)
            {
                // Modo strafe: moverse relativo a la orientación actual del personaje
                return transform.right * input.x + transform.forward * input.y;
            }
            else
            {
                // Modo normal: moverse relativo a donde mira la cámara (proyectado al plano horizontal)
                Vector3 cameraForward = mainCamera.transform.forward;
                Vector3 cameraRight = mainCamera.transform.right;

                // Proyectar al plano horizontal
                cameraForward.y = 0f;
                cameraRight.y = 0f;
                cameraForward.Normalize();
                cameraRight.Normalize();

                return cameraRight * input.x + cameraForward * input.y;
            }
        }

        void HandleCharacterRotation()
        {
            // Solo rotar el personaje si no está en modo strafe y se está moviendo
            if (!strafeMode && smoothedMovementInput.y > 0.1f)
            {
                Vector3 moveDirection = CalculateMovementDirection(smoothedMovementInput);

                if (moveDirection.magnitude > 0.1f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

                    if (smoothRotation)
                    {
                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                    }
                    else
                    {
                        transform.rotation = targetRotation;
                    }
                }
            }

        }

        void HandleJumpAndGlide()
        {
            bool isGliding = inputManager.IsHeld("float");
            movementSystem.HandleGlide(isGliding);

            if (inputManager.IsPressed("jump"))
            {
                movementSystem.HandleJump();
            }
        }

        void HandleAbilityInput()
        {
            // Absorber - habilidad continua
            if (inputManager.IsHeld("absorb") || inputManager.IsHeld("absorb_mouse"))
            {
                ExecuteAbility("absorb");
            }
            else
            {
                StopAbility("absorb");
            }

            // Irradiar - habilidad de activación
            if (inputManager.IsHeld("irradiate"))
            {
                ExecuteAbility("irradiate");
            }
            else
            {
                StopAbility("irradiate");
            }

            // Interact - habilidad de activación
            if (inputManager.IsPressed("interact"))
            {
                ExecuteAbility("interact");
                
            }
        }

        void HandleCursorToggle()
        {
            // Alternar cursor con Escape o Tab
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
            {
                ToggleCursorLock();
            }
        }

        void ExecuteAbility(string abilityName)
        {
            if (abilities.ContainsKey(abilityName) && abilities[abilityName] != null)
            {
                if (abilities[abilityName].CanExecute())
                {
                    abilities[abilityName].Execute();
                }
            }
        }

        void StopAbility(string abilityName)
        {
            if (abilities.ContainsKey(abilityName) && abilities[abilityName] != null)
            {
                abilities[abilityName].Stop();
            }
        }

        // Métodos públicos para configuración
        public void ToggleCursorLock()
        {
            cursorLocked = !cursorLocked;
            SetupCursor();
        }

        public void SetStrafeMode(bool enabled)
        {
            strafeMode = true;
        }

        public void SetMouseSensitivity(float sensitivity)
        {
            mouseSensitivity = Mathf.Clamp(sensitivity, 0.1f, 10f);
        }

        public void SetVerticalLookLimit(float limit)
        {
            verticalLookLimit = Mathf.Clamp(limit, 10f, 90f);
        }

        // Método para configurar la cámara externamente
        public void SetupCamera(Camera camera)
        {
            mainCamera = camera;
            if (cameraTarget != null)
            {
                camera.transform.SetParent(cameraTarget);
                camera.transform.localPosition = Vector3.zero;
                camera.transform.localRotation = Quaternion.identity;
            }
        }

        // Debug información
        void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                // Mostrar dirección hacia donde mira el personaje
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2f);

                // Mostrar dirección de movimiento
                if (smoothedMovementInput.magnitude > 0.1f)
                {
                    Vector3 moveDir = CalculateMovementDirection(smoothedMovementInput);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(transform.position, transform.position + moveDir * 2f);
                }

                // Mostrar target de cámara
                if (cameraTarget != null)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireSphere(cameraTarget.position, 0.1f);
                }
            }
        }
    }
} 