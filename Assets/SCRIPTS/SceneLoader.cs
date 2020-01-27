using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
*   DESCRIPTION
*   Allow scene management without dealing with the Scene manager of Build setting
*   As found on the Unity API
*/

public class SceneLoader : MonoBehaviour
{

    public string[] m_Scenes;

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Scene scene = SceneManager.GetActiveScene();

            if (scene.buildIndex != 0)
            {
                OnClick(0);
            }
        }
    }

    public void OnClick(int index)
    {
        StartCoroutine(LoadYourAsyncScene(m_Scenes[index]));
    }

    IEnumerator LoadYourAsyncScene(string sceneName)
    {
        /** The Application loads the Scene in the background as the current Scene runs.
        * This is particularly good for creating loading screens.
        * You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        * a sceneBuildIndex of 1 as shown in Build Settings. */

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
