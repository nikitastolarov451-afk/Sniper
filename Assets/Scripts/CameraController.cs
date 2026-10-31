using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
    public Camera cam; 
    public float normalFOV = 60f; 
    public float zoomFOV = 20f;

    //Добавлю переменные для дыхания снайпера(ввер вниз я сделал)
    [Header("Sway Settings")]
    public float swayPower = 1.0f;
    private float swayTimer = 0f;
    public float swaySpeed = 1.0f;


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

//само покачивание
        float swayx = 0f;
        if(Input.GetKey(KeyCode.LeftShift))
            swayTimer = 0f;
        else
            swayTimer = swayTimer + Time.deltaTime * swaySpeed;
            swayx = MathF.Sin(swayTimer) * swayPower;

        float finalXRotation = xRotation + swayx;
        

        transform.localRotation = Quaternion.Euler(finalXRotation, 0f, 0f);
        transform.root.Rotate(Vector3.up * mouseX);

        
        if (cam != null)
        {
            
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            
            if (scroll > 0f)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomFOV, Time.deltaTime * 15f);
            }
            
            else if (scroll < 0f)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, normalFOV, Time.deltaTime * 15f);
            }
        }
    }
}