using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_01 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GoToLevel_01()
    {
        Time.timeScale =1f;
        SceneManager.LoadScene("Level_01");
    } 
}
