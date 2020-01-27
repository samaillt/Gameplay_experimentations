using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
*   The class EndLevel represents the end of the test 
*   @author paul_chapelon
*   @author tom_samaille
*/
public class EndLevel : MonoBehaviour
{
    /**
    *   m_EndStatus refers to the status of the end of the app, true if the app has ended
    */
    private bool m_EndStatus = false;

    /**
    *   Timer when the panel appears (5 seconds)
    */
    private float m_TimerEndPanel = 10.0f;

    /**
    *   Background panel of final screen
    */
    public GameObject m_PanelLevel;

    /**
    *   Text of final screen
    */
    public GameObject m_TextEndLevel;

    /**
    *   Timer of the game
    */
    public GameObject m_TimerObject;

    /** 
     * m_BurnedCountDisplay refers to the BurnedCOuntDisplay GameObject
     */
    public GameObject m_BurnedCountDisplay;

    /**
    *   End of the game, display the panel and the timer
    *   @author paul_chapelon
    *   @author tom_samaille
    */
    public void EndExperimentation(string message)
    {
        m_EndStatus = true;
        m_PanelLevel.SetActive(true);   
        float time = m_TimerObject.GetComponent<SceneTiming>().GetTimer();
        m_TextEndLevel.GetComponent<Text>().text = "Fin de l'expérimentation en \n" + ConvertSecondsFormat(time) + "\n" + message + "\n" + m_BurnedCountDisplay.GetComponent<BurnedCountDisplay>().GetPourcent() + " de la forêt a brûlé";
    
        m_TextEndLevel.SetActive(true);

        m_BurnedCountDisplay.SetActive(false);

        // GameObject[] allGameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[]; /// ICI!
        // foreach (GameObject gameObject in allGameObjects)
        // {
        //     if(!(gameObject.transform.tag == "End"))
        //     {
        //         Destroy(gameObject);
        //     }   
        // }
    }

    /**
    *   Convert the seconds to a string format like X minutes X seconds
    *   @param seconds Number of seconds spent
    *   @return  Format of timer in string
    *   @author paul_chapelon
    */
    private string ConvertSecondsFormat(float seconds)
    {
        if(seconds >= 60f)
        {
            int nbMinutes = Mathf.RoundToInt(seconds) / 60;
            return nbMinutes + " minute(s) " + (Mathf.RoundToInt(seconds) % 60) + " seconde(s)";
        }
        else
        {
            return System.Math.Round(seconds,2) + " seconde(s)";
        }
    }

    public bool GetEndStatus()
    {
        return m_EndStatus;
    }

    /**
    *   End the experimentation after 5 seconds of displaying the panel End
    *   @author  paul_chapelon
    */
    void Update()
    {
        if(m_EndStatus)
        {
            if(m_TimerEndPanel < 0.0f)
            {
                gameObject.GetComponent<FindGameObject>().FindTheSingleton(0);
            }
            else
            {
                m_TimerEndPanel -= Time.deltaTime;
            }
        }
    }
}
