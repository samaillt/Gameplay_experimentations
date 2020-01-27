using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*   The class InitialPosition define the initial Transform component of an object
*   @author  paul_chapelon
*/
public class InitialPosition : MonoBehaviour
{

    /**
    *   The initial position of the object
    */
    private Vector3 m_StartPosition;

    /**
    *   The initial rotation of the object
    */
    private Quaternion m_StartRotation;

    /**
    *   Initialize the initial position and rotation of the object when he spawns
    *   @author  paul_chapelon
    */
    void Start()
    {
        m_StartPosition = GetComponent<Transform>().position;
        m_StartRotation = GetComponent<Transform>().rotation;
    }

    /**
    *   Check if the object is out of the level, in that case the object returns to his spawn
    *   @author  paul_chapelon
    */
    void Update()
    {
        if(IsDead())
        {
            RestartLevel();
        }
    }

    /**
    *   Return that the object is dead if he falls under -10 in y position
    *   @return True if the object is under -10 in y position
    *   @author  paul_chapelon
    */
    bool IsDead()
    {
        if(GetComponent<Transform>().position.y < -10)
        {
            return true;
        }
        return false;
    }

    /**
    *   Translate and rotate the object at his position and rotation of spawn
    *   @author  paul_chapelon
    */
    void RestartLevel()
    {
        GetComponent<Transform>().position = m_StartPosition;
        GetComponent<Transform>().rotation = m_StartRotation;
    }
}
