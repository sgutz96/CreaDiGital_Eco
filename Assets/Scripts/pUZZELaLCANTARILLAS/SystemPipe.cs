using ECO.Player;
using System.Collections.Generic;
using UnityEngine;

public class SystemPipe : MonoBehaviour
{
    [Header("Puzzle Settings")]
    public List<SimplePipe> pipes = new List<SimplePipe>();
    public List<bool> pipesBool = new List<bool>();

    private bool puzzleCompleted = false;

    void Start()
    {
        // Detectar todos los SimplePipe que son hijos de este GameObject
        pipes.AddRange(GetComponentsInChildren<SimplePipe>());

        // Inicializar el estado de cada tuber�a
        for (int i = 0; i < pipes.Count; i++)
        {
            pipesBool.Add(pipes[i].isOn);
        }
    }

    public void SendPipesBool(int index, bool state)
    {
        if (index >= 0 && index < pipesBool.Count)
        {
            pipesBool[index] = state;
            CheckPuzzleCompletion();
        }
    }

    private void CheckPuzzleCompletion()
    {
        puzzleCompleted = pipesBool.TrueForAll(state => state);

        if (puzzleCompleted)
        {
            Debug.Log("�Puzzle completado!");
            // Aqu� puedes a�adir animaciones, sonidos o l�gica para abrir una puerta, etc.
        }
    }
}
