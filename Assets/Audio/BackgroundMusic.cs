using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;

    void Awake()
    {
        if (instance == null)
        {
            // 2. Если его еще нет, то текущий объект становится главным
            instance = this;
            
            // говорим Unity НЕ удалять этот объект при смене сцен
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Если дубликат уже нашелся — уничтожаем его
            Destroy(gameObject);
        }
    }
}