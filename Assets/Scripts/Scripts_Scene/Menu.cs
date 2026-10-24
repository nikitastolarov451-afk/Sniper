using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    // Update is called once per frame
    public void GoToMenu()
    {
           SceneManager.LoadScene("Menu");
    }
}
