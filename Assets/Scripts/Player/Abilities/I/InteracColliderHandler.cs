// InteractColliderHandler.cs
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace ECO.Player
{
    public class InteractColliderHandler : MonoBehaviour
    {
        private InteractAbility interactAbility;
        private List<GameObject> interactableObjects = new List<GameObject>();

        public void SetInteractAbility(InteractAbility ability)
        {
            interactAbility = ability;
        }

        public bool HasInteractableObjects()
        {
            // Clean up null references
            interactableObjects.RemoveAll(obj => obj == null);
            return interactableObjects.Count > 0;
        }

        public void ExecuteInteraction()
        {
            if (interactableObjects.Count == 0) return;

            // Get the closest interactable object
            GameObject closestObject = GetClosestInteractable();
            if (closestObject == null) return;

            IInteractable interactable = closestObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
            else
            {
                Debug.LogWarning($"Object {closestObject.name} doesn't have IInteractable component.");
            }
        }

        public string GetCurrentPrompt()
        {
            if (interactableObjects.Count == 0) return "";

            GameObject closestObject = GetClosestInteractable();
            if (closestObject == null) return "";

            // Since we're keeping the interface simple, return a generic prompt
            return "Press to Interact";
        }

        private GameObject GetClosestInteractable()
        {
            if (interactableObjects.Count == 0) return null;

            return interactableObjects
                .Where(obj => obj != null)
                .OrderBy(obj => Vector3.Distance(transform.position, obj.transform.position))
                .FirstOrDefault();
        }

        private void OnTriggerEnter(Collider other)
        {
            // Check for interactable tags
            if (other.CompareTag("Switch") || other.CompareTag("Interactable"))
            {
                // Verify the object has an IInteractable component
                if (other.GetComponent<IInteractable>() != null)
                {
                    if (!interactableObjects.Contains(other.gameObject))
                    {
                        interactableObjects.Add(other.gameObject);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (interactableObjects.Contains(other.gameObject))
            {
                interactableObjects.Remove(other.gameObject);
            }
        }

        // Debug visualization
        void OnDrawGizmosSelected()
        {
            if (interactableObjects.Count > 0)
            {
                Gizmos.color = Color.cyan;
                foreach (var obj in interactableObjects)
                {
                    if (obj != null)
                    {
                        Gizmos.DrawWireCube(obj.transform.position, Vector3.one * 0.5f);
                        Gizmos.DrawLine(transform.position, obj.transform.position);
                    }
                }
            }
        }
    }
}
