using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SniperShoot : MonoBehaviour
{
    [Header("Настройки стрельбы")]
    public float range = 200f;
    public float fireSpeed = 1.5f; //1.5 выстреоа в секунду
    private float nextFireTime = 0f;//таймер, который следит, когда пройдет время fireSpeed

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
    public GameObject endScreenPanel; 

    void Start()
    {
        Time.timeScale =1f;

        currentHealth = maxHealth; 
        if (healthText != null)
            healthText.text = "HP: " + currentHealth;

        if (damageFlashImage != null)
            damageFlashImage.gameObject.SetActive(false);

        // Прячем панель при старте
        if (endScreenPanel != null)
            endScreenPanel.SetActive(false);
    }

    void Update()
    {
        
        if (damageFlashImage != null && damageFlashImage.gameObject.activeSelf)//включена и существует
        {
            damageFlashImage.color = Color.Lerp(damageFlashImage.color, Color.clear, flashSpeed * Time.deltaTime);
            if (damageFlashImage.color.a < 0.01f)
                damageFlashImage.gameObject.SetActive(false);
        }

        // Квадратик мой
        if (crosshairImage != null)
            crosshairImage.color = (Time.time >= nextFireTime) ? readyColor : reloadColor;

        // Стрельба
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
            Shoot();
    }

    void Shoot()
    {
        nextFireTime = Time.time + fireSpeed;

        if (audioSource != null)
        {
            if (shotSound != null) audioSource.PlayOneShot(shotSound);
            if (reloadSound != null) Invoke("PlayReloadSound", 0.5f);
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))//c нашей позиции летит(с середины) луч и ,направлен он вперед, куда уперся луч
        {
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
            if (enemy != null)//допустим, если яне попал в обьект у которого нет EnemyHealth то переменная enemy будет пустой
                enemy.TakeDamageEnemy(50);  
        }
    }

    void PlayReloadSound()
    {
        audioSource.PlayOneShot(reloadSound);
    }

    public void TakeDamageSniper(int damage)
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

        Time.timeScale = 0f;

        // 2. Показываем мышь
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        if (endScreenPanel != null) endScreenPanel.SetActive(true);
    }
}