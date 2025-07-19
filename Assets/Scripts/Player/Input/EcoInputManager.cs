using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECO.Player
{
    public class EcoInputManager : MonoBehaviour, IInputHandler
    {
        [Header("Input Settings")]
        public bool useNewInputSystem = true;

        private Dictionary<string, KeyCode> keyboardBindings;
        private Dictionary<string, int> mouseBindings;

        void Start()
        {
            InitializeBindings();
        }

        void InitializeBindings()
        {
            keyboardBindings = new Dictionary<string, KeyCode>
            {
                {"interact", KeyCode.E},
                {"absorb", KeyCode.LeftShift},
                {"irradiate", KeyCode.Q},
                {"connect", KeyCode.F},
                {"shoot", KeyCode.LeftControl},
                {"explosion", KeyCode.R},
                {"float", KeyCode.Space},
                {"jump", KeyCode.Space}
            };

            mouseBindings = new Dictionary<string, int>
            {
                {"absorb_mouse", 1},
                {"shoot_mouse", 0}
            };
        }

        public bool IsPressed(string action)
        {
            if (keyboardBindings.ContainsKey(action))
                return Input.GetKeyDown(keyboardBindings[action]);
            if (mouseBindings.ContainsKey(action))
                return Input.GetMouseButtonDown(mouseBindings[action]);
            return false;
        }

        public bool IsHeld(string action)
        {
            if (keyboardBindings.ContainsKey(action))
                return Input.GetKey(keyboardBindings[action]);
            if (mouseBindings.ContainsKey(action))
                return Input.GetMouseButton(mouseBindings[action]);
            return false;
        }

        public bool IsReleased(string action)
        {
            if (keyboardBindings.ContainsKey(action))
                return Input.GetKeyUp(keyboardBindings[action]);
            if (mouseBindings.ContainsKey(action))
                return Input.GetMouseButtonUp(mouseBindings[action]);
            return false;
        }

        public Vector2 GetAxis(string action)
        {
            if (action == "movement")
            {
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                return new Vector2(horizontal, vertical);
            }
            return Vector2.zero;
        }
    }
}