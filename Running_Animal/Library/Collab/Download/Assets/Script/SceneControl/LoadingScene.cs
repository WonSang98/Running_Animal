using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public Slider slider;
    string SceneName;

    void Start()
    {
        SceneName = GameManager.Instance.goScene;
        StartCoroutine(LoadAsynSceneCoroutine());
    }
    IEnumerator LoadAsynSceneCoroutine()
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneName);

        while (!operation.isDone)
        {
            slider.value = operation.progress;
            yield return null;
        }

    }

}

