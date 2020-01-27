using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*   The class ActivatorPlateform activate the plateforms if the player succeed
*   @author  paul_chapelon
*/
public class ActivatorPlatefrom : MonoBehaviour
{
    /**
    *   Check if the right button is pushed
    */
    private bool m_PushedRight;

    /**
    *   Check if the left button is check
    */
    private bool m_PushedLeft;

    /**
    *   Status of the plateforms (activated : true)
    */
    private bool m_DisplayPlateform;

    /**
    *   Initialiaze the status to false
    *   @author  paul_chapelon
    */
    void Start()
    {
        m_PushedLeft = false;
        m_PushedRight = false;
        m_DisplayPlateform = false;
    }

    /**
    *   Check if the two buttons are pushed and the plateforms is not displayed, in that case the plateforms appears
    *   @author  paul_chapelon
    */
    void Update()
    {
        if(m_PushedLeft && m_PushedRight && !m_DisplayPlateform)
        {
            // Access to all the plateforms to display them
            GameObject plateform =  GameObject.FindGameObjectsWithTag("Plateform")[0];
            for(int i = 0 ; i < plateform.transform.childCount ; i++)
            {
                GameObject child = plateform.transform.GetChild(i).gameObject;
                child.GetComponent<Renderer>().enabled = true;
                child.GetComponent<Collider>().enabled = true;
            }
            m_DisplayPlateform = true;
        }
    }

    /**
    *   Change the value of the right button
    *   @param  value  The value affected to the right button
    *   @author  paul_chapelon
    */
    public void setPushedRight(bool value)
    {
        m_PushedRight = value;
    }

    /**
    *   Change the value of the left button
    *   @param  value  The value affected to the left button
    *   @author  paul_chapelon
    */
    public void setPushedLeft(bool value)
    {
        m_PushedLeft = value;
    }
}











