using UnityEngine;

namespace ECO.Player
{
    public class IrradiateAbility : MonoBehaviour, IEcoAbility
    {
        [Header("Irradiate Settings")]
        public float irradiationRange = 3f;
        public float energyCost = 15f;
        public LayerMask contaminationLayer = 1;

        private EcoEnergySystem energySystem;

        void Start()
        {
            energySystem = GetComponent<EcoEnergySystem>();
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
        }

        public string GetAbilityName()
        {
            return "Irradiate";
        }

        private void PurifyArea()
        {
            Collider[] contaminatedAreas = Physics.OverlapSphere(transform.position, irradiationRange, contaminationLayer);

            foreach (Collider area in contaminatedAreas)
            {
                if (area.CompareTag("Contamination"))
                {
                    Debug.Log("Purifying: " + area.name);
                    // Implement purification logic
                    Destroy(area.gameObject);

                }
            }
        }
    }
}