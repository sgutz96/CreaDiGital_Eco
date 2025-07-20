using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECO.Player
{
    public class EcoEnergySystem : MonoBehaviour
    {
        [Header("Energy Settings")]
        public int maxEnergyOrbs = 3;
        public float energyPerOrb = 100f;
        public float regenRate = 10f;
        public float regenDelay = 2f;

        [SerializeField]
        private float currentEnergy;
        private float lastDamageTime;

        public float CurrentEnergy => currentEnergy;
        public float MaxEnergy => maxEnergyOrbs * energyPerOrb;
        public int CurrentOrbs => Mathf.CeilToInt(currentEnergy / energyPerOrb);

        void Start()
        {
            currentEnergy = MaxEnergy;
        }

        void Update()
        {
            if (Time.time - lastDamageTime > regenDelay)
            {
                RegenerateEnergy();
            }
        }

        public bool ConsumeEnergy(float amount)
        {
            if (currentEnergy >= amount)
            {
                currentEnergy -= amount;
                return true;
            }
            return false;
        }

        public void AddEnergy(float amount)
        {
            currentEnergy = Mathf.Min(currentEnergy + amount, MaxEnergy);
        }

        public void TakeDamage(float damage)
        {
            currentEnergy -= damage;
            lastDamageTime = Time.time;
            if (currentEnergy <= 0)
            {
                currentEnergy = energyPerOrb;
            }
        }

        private void RegenerateEnergy()
        {
            if (currentEnergy < MaxEnergy)
            {
                currentEnergy += regenRate * Time.deltaTime;
                currentEnergy = Mathf.Min(currentEnergy, MaxEnergy);
            }
        }

        public void IncreaseMaxOrbs(int amount)
        {
            maxEnergyOrbs += amount;
        }

        internal bool HasEnergy(float energyCost)
        {
            throw new NotImplementedException();
        }
    }
}