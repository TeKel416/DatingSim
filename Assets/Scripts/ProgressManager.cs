using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    MoveManager moveManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveManager = FindFirstObjectByType<MoveManager>();
    }

    public void TriggerStoryOnEnter(string storyScene)
    {
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
}
