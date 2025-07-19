using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

namespace ECO.Player
{
    public class InteractAbility : MonoBehaviour, IEcoAbility
    {
        [Header("Interac Settings")]
        public float irradiationRange = 3f;
        public float energyCost = 15f;
        public LayerMask contaminationLayer = 1;

        private EcoEnergySystem energySystem;
        // Start is called before the first frame update
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
            InteracObjet();
        }

        public void Stop()
        {
            // Instant ability
        }

        public string GetAbilityName()
        {
            return "Interac";
        }

        private void InteracObjet()
        {
            Debug.Log("InteracObjet");

        }
    }
}
