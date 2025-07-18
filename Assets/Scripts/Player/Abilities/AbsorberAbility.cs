using UnityEngine;
using static Unity.VisualScripting.Member;

namespace ECO.Player
{
    public class AbsorberAbility : MonoBehaviour, IEcoAbility
    {
        [Header("Absorber Settings")]
        public float irradiationRange = 3f;
        public float energyCost = 15f;
        public LayerMask contaminationLayer = 1;

        private EcoEnergySystem energySystem;

        public GameObject absorptionColliderObject;
        private Collider absorptionCollider;

        private AbsorberColliderHandler handler;

        void Start()
        {
            energySystem = GetComponent<EcoEnergySystem>();
            FindAbsorptionCollider();
        }

        void FindAbsorptionCollider()
        {
            // Buscar el collider en la escena con el tag específico
            absorptionColliderObject = GameObject.FindGameObjectWithTag("AbsorberAbilityTag");
            
            if (absorptionColliderObject != null)
            {
                absorptionCollider = absorptionColliderObject.GetComponent<Collider>();
                
                if (absorptionCollider != null)
                {
                    absorptionCollider.isTrigger = true;
                    absorptionCollider.enabled = false; // Inicialmente desactivado

                    // Agregar el script de detección de colisiones
                    handler = absorptionColliderObject.GetComponent<AbsorberColliderHandler>();
                    if (handler == null)
                    {
                        handler = absorptionColliderObject.AddComponent<AbsorberColliderHandler>();
                    }
                    
                }
                else
                {
                    Debug.LogError("El objeto con tag 'AbsorberAbilityTag' no tiene un Collider");
                }
            }
            else
            {
                Debug.LogError("No se encontró un objeto con tag 'AbsorberAbilityTag' en la escena");
            }
        }

        public bool CanExecute()
        {
            return energySystem.ConsumeEnergy(energyCost);
        }

        public void Execute()
        {
            AbsorbNearbyEnergy();
        }

        public void Stop()
        {
            if (handler != null)
            {
                handler.SetColliderEnabled(false);
            }
        }

        public string GetAbilityName()
        {
            return "Absorber";
        }

        private void AbsorbNearbyEnergy()
        {
           // Debug.Log("AbsorbNearbyEnergy");
            if (handler != null)
            {
                handler.SetColliderEnabled(true);
            }
        }

    }
}
