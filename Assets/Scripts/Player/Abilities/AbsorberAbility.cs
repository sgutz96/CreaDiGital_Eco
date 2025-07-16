using UnityEngine;
using static Unity.VisualScripting.Member;

namespace ECO.Player
{
    public class AbsorberAbility : MonoBehaviour, IEcoAbility
    {
        [Header("Absorber Settings")]
        public float absorptionRange = 5f;
        public float absorptionRate = 20f;
        public LayerMask energySourceLayer = 1;

        private EcoEnergySystem energySystem;
        private bool isAbsorbing;

        void Start()
        {
            energySystem = GetComponent<EcoEnergySystem>();
        }

        public bool CanExecute()
        {
            return energySystem != null && energySystem.CurrentEnergy < energySystem.MaxEnergy;
        }

        public void Execute()
        {
            if (CanExecute())
            {
                isAbsorbing = true;
            }
        }

        public void Stop()
        {
            isAbsorbing = false;
        }

        public string GetAbilityName()
        {
            return "Absorber";
        }

        void Update()
        {
            if (isAbsorbing)
            {
                AbsorbNearbyEnergy();
            }
        }

        private void AbsorbNearbyEnergy()
        {
            Debug.Log("AbsorbNearbyEnergy");
            Collider[] energySources = Physics.OverlapSphere(transform.position, absorptionRange, energySourceLayer);

            foreach (Collider source in energySources)
            {
                if (source.CompareTag("EnergySource"))
                {
                    energySystem.AddEnergy(absorptionRate * Time.deltaTime);
                    Debug.Log("Absorbing energy from: " + source.name);
                }
            }
        }
    }
}
