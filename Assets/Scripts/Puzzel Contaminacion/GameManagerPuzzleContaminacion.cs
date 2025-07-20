using UnityEngine;

namespace ECO
{
    public class GameManagerPuzzleContaminacion : MonoBehaviour
    {
        public int puzzleID = 0;
        public float checkInterval = 1f;

        private float timer = 0f;
        private bool puzzleCompleted = false;

        void Update()
        {
            if (puzzleCompleted) return;

            timer += Time.deltaTime;
            if (timer >= checkInterval)
            {
                timer = 0f;
                CheckContaminants();
            }
        }

        void CheckContaminants()
        {
            bool hasContaminants = false;

            foreach (Transform child in transform)
            {
                if (child.CompareTag("Contamination"))
                {
                    hasContaminants = true;
                    break;
                }

                // Si quieres que revise también nietos o más profundidad, usa recursión o GetComponentsInChildren
            }

            if (!hasContaminants)
            {
                puzzleCompleted = true;
                GameManager.Instance.PuzzleCompleted(puzzleID);
            }
        }
    }
}
