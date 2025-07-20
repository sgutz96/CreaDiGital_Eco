using UnityEngine;
using System.Collections.Generic;

namespace ECO.Player
{
    public class GameManagerPuzzleSwitcher : MonoBehaviour
    {
        [Header("Puzzle Settings")]
        public List<SimpleSwitch> switches = new List<SimpleSwitch>();
        

        [Header("Optional Action")]
        public GameObject objectToActivateWhenSolved; // Por ejemplo, una puerta o algo que se encienda

        public int puzzleID = 0;
        public float checkInterval = 1f;

        private float timer = 0f;
        private bool puzzleCompleted = false;

        private void Start()
        {
            // Detecta todos los SimpleSwitch que sean hijos
            switches.AddRange(GetComponentsInChildren<SimpleSwitch>());
        }

        void Update()
        {
            if (puzzleCompleted) return;

            timer += Time.deltaTime;
            if (timer >= checkInterval)
            {
                timer = 0f;
                CheckAllSwitches();
            }
        }

        public void CheckAllSwitches()
        {
            foreach (SimpleSwitch sw in switches)
            {
                if (!sw.isOn)
                {
                    // Si uno está apagado, el puzzle NO está completo
                    return;
                }
            }

            // Si llegó hasta acá, todos están encendidos
            if (!puzzleCompleted)
            {
                puzzleCompleted = true;
                GameManager.Instance.PuzzleCompleted(puzzleID);

                if (objectToActivateWhenSolved != null)
                {
                    objectToActivateWhenSolved.SetActive(false);
                }

                // Aquí podés agregar más lógica si querés que solo pase una vez
            }
        }
    }
}
