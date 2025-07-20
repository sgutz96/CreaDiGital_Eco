using ECO.Player;
using System.Collections;
using UnityEngine;

public class CieloBehavior : MonoBehaviour
{
    private EcoEnergySystem ecoEnergySystem;

    public void TemporarilyDeactivate(float seconds)
    {
        if (ecoEnergySystem != null)
        {
            ecoEnergySystem.AddEnergy(5);
        }
        else
        {
            Debug.LogWarning("EcoEnergySystem reference is not set!");
        }

        StartCoroutine(DeactivateTemporarily(seconds));
    }

    private IEnumerator DeactivateTemporarily(float seconds)
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(true);
    }
}
