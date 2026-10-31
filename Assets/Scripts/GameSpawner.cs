using UnityEngine;
using TMPro;

public class GameSpawner : MonoBehaviour
{
    [Header("Спавн")]
    public GameObject criminalPrefab; 
    public Transform[] spawnPoints;   
    public int maxCriminals = 5;      
    
    [Header("UI")]
    public TextMeshProUGUI killCounterText; 
    
    [Header("Экран Победы")]
    public GameObject endScreenPanel; 

    private int spawnedCount = 0;     
    private int kills = 0; 
    private GameObject currentCriminal; 
    private bool gameEnded = false;

    void Start()
    {
        Time.timeScale =1f;
        if (killCounterText != null)
            killCounterText.text = "Убито: 0";
        Invoke("SpawnNewCriminal", 2f);
    }

    void Update()
    {
        if (gameEnded == false && currentCriminal == null && spawnedCount < maxCriminals)
        {
            Invoke("SpawnNewCriminal", 1.5f);
        }
    }

    void SpawnNewCriminal()
    {
        if (gameEnded == true || currentCriminal != null)
            return;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenPoint = spawnPoints[randomIndex];


        currentCriminal = Instantiate(criminalPrefab, chosenPoint.position, chosenPoint.rotation);// функция берет префаб и спавнит на заданных координатах
        
        EnemyHealth enemy = currentCriminal.GetComponent<EnemyHealth>();//ищу скрипт EnemyHealth на созданом враге
        
        if (enemy != null)
        {
            Vector3 hiddenPos = chosenPoint.position;
            Vector3 visiblePos = chosenPoint.position + (chosenPoint.forward * 2f);
            
            enemy.Setup(hiddenPos, visiblePos);
            enemy.OnEnemyDied += HandleEnemyDeath;//событие
        }

        spawnedCount++;
    }

    void HandleEnemyDeath()
    {
        currentCriminal = null; 
        
        kills++;
        if (killCounterText != null) killCounterText.text = "Убито: " + kills;

        if (spawnedCount >= maxCriminals)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        gameEnded = true;
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (endScreenPanel != null) endScreenPanel.SetActive(true);
//Далее опционально
        if (killCounterText != null)
        {
            killCounterText.text = "Враги убиты!";
            killCounterText.color = Color.green;
        }
    }
}