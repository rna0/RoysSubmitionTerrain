using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void GoToGame(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("User Quit the Game");
    }
}
