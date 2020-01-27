using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindGameObject : MonoBehaviour
{
    public void FindTheSingleton(int index)
    {
        GameObject go = GameObject.Find("_Singleton");
        if (go != null)
        {
            go.GetComponent<SceneLoader>().OnClick(index);
        }

    }
}
