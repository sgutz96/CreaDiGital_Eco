using ECO.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrradiateHandler : MonoBehaviour
{
    private IrradiateAbility irradiateAbility;

    public void SetIrradiateAbility(IrradiateAbility ability)
    {
        irradiateAbility = ability;
    }

    public void SetColliderEnabled(bool isEnabled)
    {
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = isEnabled;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Frailejon"))
        {
            Destroy(other.gameObject);
            Debug.Log("Objeto de contaminación eliminado.");
        }
    }
}
