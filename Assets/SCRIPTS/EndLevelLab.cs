using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
*   The class EndLevel represents the end of the test 
*   @author paul_chapelon
*/
public class EndLevelLab : MonoBehaviour
{

    /**
    *   The number of player in the area
    */
    private int m_NumberPlayersIn = 0;


    /**
    *   Panel to display the end of the experimentation
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
    *   Timer when the panel appears (5 seconds)
    */
    private float m_TimerEndPanel = 5.0f;

    /**
    *   Boolean to begin the timer of displaying the panel
    */
    private bool m_EndExperimentationEnded = false;

    /**
    *   End the experimentation after 5 seconds of displaying the panel End
    *   @author  paul_chapelon
    */
    void Update()
    {
        if(m_EndExperimentationEnded)
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

    /**
    *   Increment the number when a player enters and end the experimentation if all players are in it
    *   @param colliderObject The object which enters the area
    *   @author paul_chapelon
    */
    void OnTriggerEnter(Collider colliderObject)
    {
        if(colliderObject.tag == "Player")
        {
            m_NumberPlayersIn ++;
        }

        if(GameObject.FindGameObjectsWithTag("Player").Length == m_NumberPlayersIn)
        {
            EndExperimentation();
        }
    }

    /**
    *   Decrement the number when a player exits 
    *   @param colliderObject The object which exits the area
    *   @author paul_chapelon
    */
    void OnTriggerExit(Collider colliderObject)
    {
        if(colliderObject.tag == "Player")
        {
            m_NumberPlayersIn --;
        }
    }  

    /**
    *   End of the game, display the panel and the timer
    *   @author paul_chapelon
    */
    public void EndExperimentation()
    {
        m_PanelLevel.SetActive(true);   
        float time = m_TimerObject.GetComponent<TimerScene>().GetTimer();
        Debug.Log(time);
        m_TextEndLevel.GetComponent<Text>().text = "Fin de l'expérimentation en\n" + ConvertSecondsFormat(time);
    
        m_TextEndLevel.SetActive(true);
        m_EndExperimentationEnded = true;
        
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
}
