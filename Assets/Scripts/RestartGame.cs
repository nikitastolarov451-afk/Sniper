using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    // Метод OnEnable вызывается автоматически, как только объект (кнопка) включается
    void OnEnable()
    {
        // Показываем курсор и делаем его свободным
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Этот метод вызывается при клике на кнопку
    public void RestartScene()
    {
        Time.timeScale = 1f; // Возвращаем время
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Рестарт
    }
}