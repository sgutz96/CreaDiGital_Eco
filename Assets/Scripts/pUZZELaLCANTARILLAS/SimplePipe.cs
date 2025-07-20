using System.Collections;
using UnityEngine;

namespace ECO.Player
{
    public class SimplePipe : MonoBehaviour, IInteractable
    {
        public enum PipeRotation
        {
            Deg0 = 0,
            Deg90 = 90,
            Deg180 = 180,
            Deg270 = 270
        }

        [Header("Pipe Settings")]
        public PipeRotation correctRotation;
        public PipeRotation currentRotation;

        [Header("Switch Settings")]
        public bool isOn = false;

        private SystemPipe systemPipe;

        private void Start()
        {
            // Inicializar rotación
            transform.rotation = Quaternion.Euler(0, (float)currentRotation, 0);

            // Buscar referencia al SystemPipe en el padre
            systemPipe = GetComponentInParent<SystemPipe>();
        }

        public void Interact()
        {
            RotatePipe();
        }

        private void RotatePipe()
        {
            // Rotar 90° en cada interacción
            currentRotation = (PipeRotation)(((int)currentRotation + 90) % 360);
            transform.rotation = Quaternion.Euler(0, (float)currentRotation, 0);

            // Actualizar estado de encendido
            isOn = currentRotation == correctRotation;

            // Notificar al sistema
            SendBool();
        }

        public void SendBool()
        {
            if (systemPipe == null) return;

            int index = systemPipe.pipes.IndexOf(this);
            if (index != -1)
            {
                systemPipe.SendPipesBool(index, isOn);
            }
        }
    }
}
