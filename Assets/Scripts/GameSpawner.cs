using UnityEngine;
using TMPro;

public class GameSpawner : MonoBehaviour
{
    [Header("Настройки Спавна")]
    public GameObject criminalPrefab; 
    public Transform[] spawnPoints;   
    public int maxCriminals = 5;      
    
    [Header("UI (Интерфейс)")]
    public TextMeshProUGUI killCounterText; 
    
    [Header("Экран Победы")]
    public GameObject endScreenPanel; // Сюда перетащим панель

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
        if (!gameEnded && currentCriminal == null && spawnedCount < maxCriminals)
        {
            Invoke("SpawnNewCriminal", 1.5f);
        }
    }

    void SpawnNewCriminal()
    {
        if (gameEnded || currentCriminal != null) return;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenPoint = spawnPoints[randomIndex];

        currentCriminal = Instantiate(criminalPrefab, chosenPoint.position, chosenPoint.rotation);
        
        EnemyHealth enemy = currentCriminal.GetComponent<EnemyHealth>();
        
        if (enemy != null)
        {
            Vector3 hiddenPos = chosenPoint.position;
            Vector3 visiblePos = chosenPoint.position + (chosenPoint.forward * 2f);
            
            enemy.Setup(hiddenPos, visiblePos);
            enemy.OnEnemyDied += HandleEnemyDeath;
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
        Debug.Log("<color=green>ПОБЕДА!</color>");
        
        // 1. Останавливаем время
        Time.timeScale = 0f;
        
        // 2. Показываем мышь
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // 3. Включаем панель
        if (endScreenPanel != null) endScreenPanel.SetActive(true);
        
        // 4. Меняем текст (по желанию)
        if (killCounterText != null)
        {
            killCounterText.text = "МИССИЯ ВЫПОЛНЕНА!";
            killCounterText.color = Color.green;
        }
    }
}