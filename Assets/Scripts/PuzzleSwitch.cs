using UnityEngine;

namespace ECO.Player
{
    public class PuzzleSwitch : MonoBehaviour, IInteractable
    {
        public bool IsActivated = false;

        public void Interact()
        {
            IsActivated = !IsActivated;
            Debug.Log($"{gameObject.name} -> IsActivated: {IsActivated}");
        }
    }
}
