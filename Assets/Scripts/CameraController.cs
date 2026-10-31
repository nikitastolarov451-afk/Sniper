using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
    public Camera cam; //ссылка на компонент камеры
    public float normalFOV = 60f; //полезрение камеры в обычном режиме
    public float zoomFOV = 20f;   //суженое ну или конечное поле зрение

    //Добавлю переменные для дыхания снайпера(ввер вниз я сделал)
    [Header("Sway Settings")]
    public float swayPower = 1.0f;
    private float swayTimer = 0f;
    public float swaySpeed = 1.0f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//фиксирую курсос по центру
        Cursor.visible = false;//и делаю курсор невидимм
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
    }
}