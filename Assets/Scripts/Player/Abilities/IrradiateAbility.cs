using UnityEngine;
using static UnityEngine.InputManagerEntry;

namespace ECO.Player
{
    public class IrradiateAbility : MonoBehaviour, IEcoAbility
    {
        [Header("Irradiate Settings")]
        public float irradiationRange = 3f;
        public float energyCost = 15f;
        public LayerMask contaminationLayer = 1;

        private EcoEnergySystem energySystem;

        public GameObject irradiateObject;
        private Collider irradiateCollider;

        private IrradiateHandler handler;

        void Start()
        {
            energySystem = GetComponent<EcoEnergySystem>();
            FindIrradiate();
        }

        public void FindIrradiate()
        {
            // Buscar el collider en la escena con el tag específico
            irradiateObject = GameObject.FindGameObjectWithTag("IrradiateAbilityTag");
            irradiateObject.SetActive(false);

            if (irradiateObject != null)
            {
                irradiateCollider = irradiateObject.GetComponent<Collider>();

                if (irradiateCollider != null)
                {
                    irradiateCollider.isTrigger = true;
                    //irradiateCollider.enabled = false; // Inicialmente desactivado

                    // Agregar el script de detección de colisiones
                    handler = irradiateObject.GetComponent<IrradiateHandler>();
                    if (handler == null)
                    {
                        handler = irradiateObject.AddComponent<IrradiateHandler>();
                    }

                }
                else
                {
                    Debug.LogError("El objeto con tag 'IrradiateAbilityTag' no tiene un Collider");
                }
            }
            else
            {
                Debug.LogError("No se encontró un objeto con tag 'IrradiateAbilityTag' en la escena");
            }

        }

        public bool CanExecute()
        {
            return energySystem.ConsumeEnergy(energyCost);
        }

        public void Execute()
        {
            PurifyArea();
        }

        public void Stop()
        {
            // Instant ability
            if (irradiateObject != null)
            {
                irradiateObject.SetActive(false);
            }
        }

        public string GetAbilityName()
        {
            return "Irradiate";
        }

        private void PurifyArea()
        {
            // Debug.Log("AbsorbNearbyEnergy");
            if (irradiateObject != null)
            {
                irradiateObject.SetActive(true);
            }

        }
    }
}