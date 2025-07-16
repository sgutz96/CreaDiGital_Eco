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

        private Dictionary<string, IEcoAbility> abilities;

        void Start()
        {
            InitializeComponents();
            InitializeAbilities();
        }

        void InitializeComponents()
        {
            if (inputManager == null)
                inputManager = GetComponent<EcoInputManager>();
            if (energySystem == null)
                energySystem = GetComponent<EcoEnergySystem>();
            if (movementSystem == null)
                movementSystem = GetComponent<EcoMovementSystem>();
        }

        void InitializeAbilities()
        {
            abilities = new Dictionary<string, IEcoAbility>
            {
                {"absorb", GetComponent<AbsorberAbility>()},
                {"irradiate", GetComponent<IrradiateAbility>()}
            };
        }

        void Update()
        {
            HandleMovementInput();
            HandleAbilityInput();
        }

        void HandleMovementInput()
        {
            Vector2 movementInput = inputManager.GetAxis("movement");
            movementSystem.HandleMovement(movementInput);

            bool isGliding = inputManager.IsHeld("float");
            movementSystem.HandleGlide(isGliding);

            if (inputManager.IsPressed("jump"))
            {
                movementSystem.HandleJump();
            }
        }

        void HandleAbilityInput()
        {
            if (inputManager.IsHeld("absorb") || inputManager.IsHeld("absorb_mouse"))
            {
                ExecuteAbility("absorb");
            }
            else
            {
                StopAbility("absorb");
            }

            if (inputManager.IsPressed("irradiate"))
            {
                ExecuteAbility("irradiate");
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
    }
}