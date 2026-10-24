using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{

    // Update is called once per frame
    public void GoToLevels()
    {
           SceneManager.LoadScene("Levels");
    }
}