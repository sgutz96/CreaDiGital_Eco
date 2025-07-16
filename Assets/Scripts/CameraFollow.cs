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

        // Calcula la posición deseada con offset (puede adaptarse según la dirección del jugador si se desea)
        Vector3 desiredPosition = target.position + currentOffset;

        // Suaviza el movimiento (como en Journey, no es brusco)
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.position = smoothedPosition;

        // Interpolación suave del lookAt para que no sea brusco
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime / rotationSmoothTime);
    }
}