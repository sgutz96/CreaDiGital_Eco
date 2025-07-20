using ECO.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrradiateHandler : MonoBehaviour
{
    private IrradiateAbility irradiateAbility;

    public LayerMask petrifiedLayer; // Para mayor claridad

    public Material FrailejonMat; // Asigna la nueva textura desde el inspector

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
        GameObject obj = other.gameObject;

        // Verifica si el objeto está en el layer "Petrificado"
        if (obj.layer == LayerMask.NameToLayer("Petrificado") && obj.CompareTag("Hongo"))
        {
            // Cambiar textura (material)
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null && FrailejonMat != null)
            {
                renderer.material = FrailejonMat;
            }

            // Cambiar el tag a "Untagged" y layer
            obj.tag = "Untagged";
            obj.layer = LayerMask.NameToLayer("Default");

        }
    }
}
