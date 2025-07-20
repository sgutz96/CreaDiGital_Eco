using UnityEngine;

namespace ECO.Player
{
    public class SimpleSwitch : MonoBehaviour, IInteractable
    {
        [Header("Switch Settings")]
        public bool isOn = false;

        public void Interact()
        {
            isOn = !isOn;
            Debug.Log("Switch toggled: " + (isOn ? "ON" : "OFF"));
        }
    }
}
