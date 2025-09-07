using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "Game";

    public void StartGame()
    {
        Debug.Log("hitting");
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {

        if (SceneManager.GetActiveScene().name.Equals("FirstLevel")){
            SceneManager.LoadScene("Main Menu");
        }
        else
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;

            Application.Quit();
#endif
        }
    }
}
