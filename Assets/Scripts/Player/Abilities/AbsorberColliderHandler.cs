    using UnityEngine;

    namespace ECO.Player
    {
        public class AbsorberColliderHandler : MonoBehaviour
        {
            private AbsorberAbility absorberAbility;


        public void SetAbsorberAbility(AbsorberAbility ability)
            {
                absorberAbility = ability;
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

            


                if (other.CompareTag("Contamination"))
                {
                    Destroy(other.gameObject);
                    Debug.Log("Objeto de contaminación eliminado.");
                }
                if (other.CompareTag("Cielo"))
                {
                    // Intentamos obtener el componente
                    CieloBehavior cielo = other.GetComponent<CieloBehavior>();

                    // Si no lo tiene, se lo agregamos
                    if (cielo == null)
                    {
                        cielo = other.gameObject.AddComponent<CieloBehavior>();
                        Debug.Log("Script 'CieloBehavior' agregado al objeto.");
                    }

                    // Llamamos a la función
                    cielo.TemporarilyDeactivate(5f);
                    Debug.Log("Cielo desactivado temporalmente.");
                }
            
            
        }
    }
}
