using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*   The class ButtonSpawn defines the button which do spawning the cubes on the balance
*   @author paul_chapelon
*/
public class ButtonSpawn : MonoBehaviour
{

    /**
    *   Defines the cube which spawn on the balance
    */
    public GameObject m_CubeBalance;

    /**
    *   Defines the second cube which spawn on the balance
    */
    public GameObject m_CubeJump;

    /**
    *   The two cubes over the balance spawn
    *   @param  colliderObject  Object on the button
    *   @author  paul_chapelon
    */
    void OnTriggerEnter(Collider colliderObject)
    {   
        m_CubeBalance.GetComponent<Renderer>().enabled = true;
        m_CubeBalance.GetComponent<Rigidbody>().useGravity = true;
        
        m_CubeJump.GetComponent<Renderer>().enabled = true;
        m_CubeJump.GetComponent<Rigidbody>().useGravity = true;
    }
}
