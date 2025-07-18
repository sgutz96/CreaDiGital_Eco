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
            Debug.Log("Objeto entro");
            if (other.CompareTag("Contamination"))
            {
                Destroy(other.gameObject);
                Debug.Log("Objeto de contaminación eliminado.");
            }

            /*if (absorberAbility == null || !absorberAbility.enabled) return;

            if (other.CompareTag("Contamination"))
            {
                Destroy(other.gameObject);
                Debug.Log("Objeto de contaminación eliminado.");
            }*/
        }
    }
}
