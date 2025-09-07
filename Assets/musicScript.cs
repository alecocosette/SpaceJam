using UnityEngine;
using UnityEngine.SceneManagement;

public class musicScript : MonoBehaviour
{
    void Start()
    {
        
        musicScript[] managers = FindObjectsOfType<musicScript>();
        if (managers.Length > 1)
        {
            Destroy(gameObject);
            return; 
        }
        DontDestroyOnLoad(gameObject); 
    }

    void Update()
    {
       
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            Destroy(gameObject); 
        }
    }
}
