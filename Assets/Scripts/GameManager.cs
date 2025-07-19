using UnityEngine;

namespace ECO
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Header("Puzzles Completados")]
        public bool[] puzzlesCompletados;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PuzzleCompleted(int puzzleID)
        {
            puzzlesCompletados[puzzleID] = true;

            // Aquí puedes añadir lógica general de cuando todos los puzzles estén completados, etc.
        }
    }
}
