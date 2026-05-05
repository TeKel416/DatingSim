using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public GameObject movementCanvas;

    public void TriggerStoryOnEnter(string storyScene)
    {
        ActivateMoveCanvas(false);
        StartCoroutine(LoadAsyncScene(storyScene));
    }

    IEnumerator LoadAsyncScene(string storyScene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(storyScene, LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(storyScene));
    }

    public void ActivateMoveCanvas(bool on)
    {
        if (movementCanvas) movementCanvas.SetActive(on);
    }
}
