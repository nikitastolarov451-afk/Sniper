using UnityEngine;

public class Pravila : MonoBehaviour
{
    public GameObject panel;
    private bool isOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void TogglePanel()
    {
        if(panel != null)
        {
            isOpen = !isOpen;
            panel.SetActive(isOpen);
        }
    }

}
