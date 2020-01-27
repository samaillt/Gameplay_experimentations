using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
*   CLASS DESCRIPTION
*   Countdown before a new action is taken by dynamic actors
*   @Simon Descoteaux
*/

public class ActionTimer : MonoBehaviour
{
    public float m_TimerLength; /// Countdown before new action
    public Text m_CountdownText; /// Text object that shows the countdown

    void Start()
    {
        StartCoroutine(StartCountdown(m_TimerLength));

    }


    /**
    * CONSTRUCTOR StartCountdown: Loop that launch an action for object of tag DynamicObject
    * after a countdown of length timerLength
    */
    public IEnumerator StartCountdown(float countdown)
    {   
        countdown = m_TimerLength;   
        while (countdown > 0)
        {
            m_CountdownText.text = "Time Until Next Action : " + countdown + "s";
            countdown--;

            yield return new WaitForSeconds(1.0f);

            if (countdown <= 0)
            {
                countdown = m_TimerLength;
                StartAction("DynamicObject");
            }
        }
    }

    /**
    * StartAction: Look for all objects of tag DynamicObject in the scene and check them for actions
    */
    void StartAction(string tag)
    {
        GameObject[] dynamicObjects = GameObject.FindGameObjectsWithTag(tag);

        if (dynamicObjects != null)
        {
            for (int i = 0; i < dynamicObjects.Length; i++)
            {
                dynamicObjects[i].GetComponent<DynamicActorBehavior>().CheckForActions();
            }
        }
    }
}
