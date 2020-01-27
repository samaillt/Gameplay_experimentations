using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*   The class ButtonScript modify the button when it is pushed
*   @author  paul_chapelon
*/
public class ButtonScript : MonoBehaviour
{
    /**
    *   Reference to the activator of the plateforms
    */
    public GameObject m_GO;

    /**
    *   The initial material of the button
    */
    public Material m_Material;

    /**
    *   Number of objects on the button
    */
    private int m_NumberObjectIn = 0;



    /**
    *   Change the color of the button and set it true for the right or the left button
    *   @param  colliderObject  The object which push the button
    *   @author  paul_chapelon
    */
    void OnTriggerEnter(Collider colliderObject)
    {
        GetComponent<Renderer>().material.color = Color.white;
        if (gameObject.name == "ButtonRight") 
        {
            m_GO.GetComponent<ActivatorPlatefrom>().setPushedRight(true);
        } 
        else if(gameObject.name == "ButtonLeft")
        {
            m_GO.GetComponent<ActivatorPlatefrom>().setPushedLeft(true);
        }
        m_NumberObjectIn ++;
    }

    /**
    *   Give the original color if there is no more object on it 
    *   @param  colliderObject  Object which exits the button
    *   @author  paul_chapelon
    */
    void OnTriggerExit(Collider colliderObject)
    {
        
        m_NumberObjectIn --;
        if(m_NumberObjectIn == 0)
        {
            GetComponent<Renderer>().material = m_Material;
            if (gameObject.name == "ButtonRight") 
            {
                m_GO.GetComponent<ActivatorPlatefrom>().setPushedRight(false);
            } 
            else if(gameObject.name == "ButtonLeft")
            {
                m_GO.GetComponent<ActivatorPlatefrom>().setPushedLeft(false);
            }
        }
    }
}
