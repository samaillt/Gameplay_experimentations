using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public int m_Index;
    public string m_Desription;

    private DynamicMusic dynamicMusic;

    void Start()
    {
        GameObject dynamicMusicObject = GameObject.Find("AudioManager");
        if (dynamicMusicObject != null)
        {
            dynamicMusic = dynamicMusicObject.GetComponent<DynamicMusic>();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            dynamicMusic.PlayMusic(m_Index);
            dynamicMusic.m_InfoText.text = m_Desription;
        }
    }
}

