using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public System.Action OnEnemyDied;//поле события

    [Header("Настройки дуэли")]
    public float timeBetweenShots = 3f; 
    private float shotTimer;

    [Header("Движение")]
    private Vector3 hiddenPosition; 
    private Vector3 visiblePosition; 
    public float moveSpeed = 5f;
    private bool isActive = false; 
    private bool isDead = false;

    public void Setup(Vector3 hiddenPos, Vector3 visiblePos)
    {
        hiddenPosition = hiddenPos;
        visiblePosition = visiblePos;
        transform.position = hiddenPosition;
        shotTimer = timeBetweenShots; 
    }

    void Update()
    {
        if (isDead == true)
            return;

        if (isActive == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, visiblePosition, moveSpeed * Time.deltaTime);//MoveTowards это метод, который потепенно сближает текущую позииу у трансформа врага
            if (Vector3.Distance(transform.position, visiblePosition) < 0.1f) 
                isActive = true;//короче, когда мо энеми активен , то управление стрельбой переходит на else
        }
        else

        {
            shotTimer -= Time.deltaTime;
            if (shotTimer <= 0f)
            {
                SniperShoot player = FindFirstObjectByType<SniperShoot>();
                if (player != null)
                    player.TakeDamageSniper(50); 
                shotTimer = timeBetweenShots; 
            }
        }
    }

    public void TakeDamageEnemy(int damage)
    {
        if (isDead == true)
            return; 
        isDead = true;
        OnEnemyDied?.Invoke(); 
        Destroy(gameObject);
    }
}