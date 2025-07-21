using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scarecrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Este método se llama cuando otro collider entra en el trigger de este objeto
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("IrradiateAbilityTag"))
        {
            Destroy(other.gameObject); // Destruye el objeto con el tag "bullplayer"
            Destroy(gameObject);       // Se destruye a sí mismo
        }
    }
}
