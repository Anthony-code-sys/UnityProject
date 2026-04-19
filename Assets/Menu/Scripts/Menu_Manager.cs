using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNavigation : MonoBehaviour
{
    public void LoadLevel(int sceneIndex)
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Game is Exiting...");
        Application.Quit();
    }
}