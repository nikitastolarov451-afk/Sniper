using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SniperShoot : MonoBehaviour
{
    [Header("Настройки стрельбы")]
    public float range = 200f;
    public float fireRate = 1.5f; 
    private float nextFireTime = 0f;

    [Header("Прицел")]
    public Image crosshairImage; 
    public Color readyColor = Color.black; 
    public Color reloadColor = Color.red;  

    [Header("Звуки")]
    public AudioSource audioSource; 
    public AudioClip shotSound;     
    public AudioClip reloadSound;   

    [Header("Здоровье")]
    public int maxHealth = 100;
    private int currentHealth;
    public TextMeshProUGUI healthText; 
    public Image damageFlashImage; 
    public float flashSpeed = 5f;  
    private Color flashColor = new Color(1f, 0f, 0f, 0.5f); 

    [Header("Экран Смерти")]
    public GameObject endScreenPanel; // Панель с кнопками

    void Start()
    {
        Time.timeScale =1f;

        currentHealth = maxHealth; 
        if (healthText != null)
            healthText.text = "HP: " + currentHealth;

        if (damageFlashImage != null)
        {
            damageFlashImage.gameObject.SetActive(false);
            damageFlashImage.color = Color.clear; 
        }

        // Прячем панель при старте
        if (endScreenPanel != null) endScreenPanel.SetActive(false);
    }

    void Update()
    {
        // Вспышка
        if (damageFlashImage != null && damageFlashImage.gameObject.activeSelf)
        {
            damageFlashImage.color = Color.Lerp(damageFlashImage.color, Color.clear, flashSpeed * Time.unscaledDeltaTime);
            if (damageFlashImage.color.a < 0.01f)
                damageFlashImage.gameObject.SetActive(false);
        }

        // Прицел
        if (crosshairImage != null)
            crosshairImage.color = (Time.time >= nextFireTime) ? readyColor : reloadColor;

        // Стрельба
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
            Shoot();
    }

    void Shoot()
    {
        nextFireTime = Time.time + fireRate;

        if (audioSource != null)
        {
            if (shotSound != null) audioSource.PlayOneShot(shotSound);
            if (reloadSound != null) Invoke("PlayReloadSound", 0.4f);
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                float hitY = hit.point.y;
                float enemyCenterY = hit.transform.position.y;
                
                if (hitY > enemyCenterY + 0.3f) enemy.TakeDamage(100); 
                else enemy.TakeDamage(50);  
            }
        }
    }

    void PlayReloadSound()
    {
        if (audioSource != null && reloadSound != null) audioSource.PlayOneShot(reloadSound);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (healthText != null) healthText.text = "HP: " + currentHealth;

        if (damageFlashImage != null) 
        {
            damageFlashImage.gameObject.SetActive(true); 
            damageFlashImage.color = flashColor;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("<color=red>ИГРОК ПОГИБ</color>");

        // 1. Останавливаем игру
        Time.timeScale = 0f;

        // 2. Показываем мышь
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // 3. Открываем панель с кнопками
        if (endScreenPanel != null) endScreenPanel.SetActive(true);
    }
}