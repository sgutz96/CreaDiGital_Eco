using ECO.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECO.Player
{

    public class shootAbility : MonoBehaviour, IEcoAbility
    {
        [Header("Irradiate Settings")]
        public float energyCost = 15f;

        public Transform shootHandle;
        [Header("Projectile Settings")]
        public GameObject projectilePrefab;  // Asigna el prefab de la bala en el inspector
        public float shootForce = 1000f;     // Fuerza de disparo


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
            Disparo();
        }

        public void Stop()
        {

        }

        public string GetAbilityName()
        {
            return "shoot";
        }

        private void Disparo()
        {
            if (projectilePrefab == null || shootHandle == null)
            {
                Debug.LogWarning("Falta asignar el prefab de la bala o el punto de disparo (shootHandle).");
                return;
            }

            // Instanciar la bala en la posición y rotación del shootHandle
            GameObject bullet = Instantiate(projectilePrefab, shootHandle.position, shootHandle.rotation);
            // Asegurarse de que la bala tenga un Rigidbody
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = bullet.AddComponent<Rigidbody>();
                rb.mass = 1f; // Puedes ajustar esto según lo que necesites
            }

            // Aplicar fuerza si tiene Rigidbody
            rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(shootHandle.forward * shootForce, ForceMode.Impulse);
            }
        }

    }
}