using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float maxLookAngle = 80f;
    private float xRotation = 0f;

    public Camera cam; 
    public float normalFOV = 60f; 
    public float zoomFOV = 20f;   

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.parent.Rotate(Vector3.up * mouseX);

        // --- ДОБАВЛЯЕМ ЛОГИКУ ЗУМА (в самом конце Update) ---
        if (cam != null)
        {
            // Получаем прокрутку колесика
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            // Крутим вверх (приближаем)
            if (scroll > 0f)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomFOV, Time.deltaTime * 15f);
            }
            // Крутим вниз (отдаляем)
            else if (scroll < 0f)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, normalFOV, Time.deltaTime * 15f);
            }
        }
        // ----------------------------------------------------
    }
}