using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
