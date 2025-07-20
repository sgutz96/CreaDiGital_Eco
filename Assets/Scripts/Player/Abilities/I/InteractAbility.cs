using UnityEngine;

namespace ECO.Player
{
    public class InteractAbility : MonoBehaviour, IEcoAbility
    {
        [Header("Interaction Settings")]
        public float interactionDistance = 3f;
        public float energyCost = 15f;
        public LayerMask interactableLayer;

        [Header("Origin of Interaction Ray")]
        public Transform interactionOrigin; // Puede ser la cabeza, torso, mano, etc.


        private EcoEnergySystem energySystem;

        void Start()
        {
            energySystem = GetComponent<EcoEnergySystem>();
        }

        public bool CanExecute()
        {
            return energySystem.ConsumeEnergy(energyCost);
        }

        public void Execute()
        {
            Interact();
        }

        public void Stop()
        {
            // Instant ability
        }

        public string GetAbilityName()
        {
            return "Interact";
        }

        private void Interact()
        {

           
            if ( interactionOrigin != null)
            {
               
                Ray ray = new Ray(interactionOrigin.position, interactionOrigin.forward);
                if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactableLayer))
                {

                    IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        Debug.Log("Interact");
                        interactable.Interact();
                    }
                }
            }

        }
    }
}