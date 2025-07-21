using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    // Referencia al objeto que quieres activar/desactivar
    public GameObject objetoAActivar;

    private void Awake()
    {
        objetoAActivar.SetActive(false);
    }

    // Se llama cuando un Collider entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objetoAActivar.SetActive(true);
        }
    }

    // Se llama cuando un Collider sale del trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objetoAActivar.SetActive(false);
        }
    }
}
