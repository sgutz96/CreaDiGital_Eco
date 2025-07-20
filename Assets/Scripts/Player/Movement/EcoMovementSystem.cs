using UnityEngine;

namespace ECO.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class EcoMovementSystem : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float floatSpeed = 5f;
        public float glideSpeed = 3f;
        public float jumpForce = 8f;
        public float gravity = 20f;
        public float maxFallSpeed = 25f;

        [Header("Jump Settings")]
        public int maxJumps = 2; // Cambiá este valor a 2 (doble salto) o 3 (triple salto)

        private Rigidbody rb;
        private bool isGrounded;
        private int jumpCount;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody>();
            }
            rb.useGravity = false; // Usamos gravedad personalizada
        }

        void FixedUpdate()
        {
            ApplyGravity();
            CheckGrounded();
        }

        public void HandleMovement(Vector2 input)
        {
            Vector3 movement = new Vector3(input.x, 0, input.y) * floatSpeed;
            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        }

        public void HandleGlide(bool isGliding)
        {
            if (isGliding && rb.velocity.y < 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, -glideSpeed, rb.velocity.z);
            }
        }

        public void HandleJump()
        {
            if (jumpCount < maxJumps)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                jumpCount++;
            }
        }

        private void ApplyGravity()
        {
            if (!isGrounded)
            {
                rb.velocity += Vector3.down * gravity * Time.fixedDeltaTime;
                rb.velocity = new Vector3(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed), rb.velocity.z);
            }
            else if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, -1f, rb.velocity.z); // Empuje leve hacia abajo
            }
        }

        private void CheckGrounded()
        {
            // Este raycast chequea el suelo justo debajo del personaje
            isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 1.1f);
            if (isGrounded)
            {
                jumpCount = 0; // Resetear los saltos al tocar el suelo
            }
        }

        public void UpgradeSpeed(float multiplier)
        {
            floatSpeed *= multiplier;
        }
    }
}
