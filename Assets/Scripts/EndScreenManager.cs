using UnityEngine;
using UnityEngine.SceneManagement;
using VNCreator;

public class EndScreenManager : MonoBehaviour
{
    private void Start()
    {
        VNCreator_MusicSource.Instance.PlayMusic("Credits");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
