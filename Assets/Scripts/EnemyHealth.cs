using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public System.Action OnEnemyDied;

    [Header("Настройки дуэли")]
    public float timeBetweenShots = 2f; 
    private float shotTimer;

    [Header("Движение")]
    public Vector3 hiddenPosition; 
    public Vector3 visiblePosition; 
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
        if (isDead)
            return;

        if (!isActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, visiblePosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, visiblePosition) < 0.1f) 
                isActive = true;
        }
        else
        {
            shotTimer -= Time.deltaTime;
            if (shotTimer <= 0f)
            {
                SniperShoot player = FindFirstObjectByType<SniperShoot>();
                if (player != null) player.TakeDamage(50); 
                shotTimer = timeBetweenShots; 
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return; 
        isDead = true;
        OnEnemyDied?.Invoke(); 
        Destroy(gameObject);
    }
}