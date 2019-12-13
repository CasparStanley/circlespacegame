using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlLoader : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        Debug.LogWarning("<color=yellow>ASYNC LOAD STARTED - </color>" + "<color=red>DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH</color>");

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.89f)
        {
            yield return null;
        }

        yield return new WaitForSeconds(3);

        asyncLoad.allowSceneActivation = true;
    }
}
