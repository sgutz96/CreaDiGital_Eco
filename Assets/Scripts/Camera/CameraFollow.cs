using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5f, -10f);
    public float DesfasYCam = 1.5f; // Esto har� que mire un poco por encima del jugador
    public float smoothTime = 0.3f;
    public float rotationSmoothTime = 0.5f;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        if (target == null) return;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Calcula el offset rotado seg�n la rotaci�n del jugador
        Vector3 rotatedOffset = target.rotation * offset;
        Vector3 desiredPosition = target.position + rotatedOffset;

        // Suaviza la posici�n de la c�mara
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.position = smoothedPosition;

        // Punto al que la c�mara debe mirar, con un desplazamiento en Y
        Vector3 lookAtTarget = target.position + new Vector3(0, DesfasYCam, 0);

        // Suaviza la rotaci�n para mirar hacia el punto
        Quaternion desiredRotation = Quaternion.LookRotation(lookAtTarget - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime / rotationSmoothTime);
    }
}
