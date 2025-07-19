using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5f, -10f);
    public float smoothTime = 0.3f;
    public float rotationSmoothTime = 0.5f;
    public float distanceDamp = 2f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 currentOffset;



    void Start()
    {
        if (target == null) return;
        currentOffset = offset;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Calcula el offset rotado según la rotación del jugador
        Vector3 rotatedOffset = target.rotation * offset;
        Vector3 desiredPosition = target.position + rotatedOffset;

        // Suaviza la posición de la cámara
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.position = smoothedPosition;

        // Hace que la cámara siempre mire al jugador desde atrás
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime / rotationSmoothTime);

    }
}