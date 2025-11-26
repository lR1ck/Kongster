using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El cubo/gorilla
    public float distance = 7f; // Distancia de la cámara
    public float height = 3f; // Altura de la cámara
    public float mouseSensitivity = 3f; // Sensibilidad del mouse
    public float smoothSpeed = 0.125f;

    private float currentX = 0f;
    private float currentY = 20f; // Ángulo vertical inicial

    void LateUpdate()
    {
        if (target == null)
            return;

        // Rotación con mouse
        currentX += Input.GetAxis("Mouse X") * mouseSensitivity;
        currentY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        // Limitar rotación vertical para que no gire de cabeza
        currentY = Mathf.Clamp(currentY, 5f, 80f);

        // Calcular posición de la cámara
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 desiredPosition = target.position + rotation * direction + Vector3.up * height;

        // Suavizar movimiento
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        // Siempre mirar al target
        transform.LookAt(target.position + Vector3.up * height);
    }
}