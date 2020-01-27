using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*   The class UnlockLabyrinth open the labyrinth for the player
*   @author  paul_chapelon
*/
public class UnlockLabyrinth : MonoBehaviour
{
    /**
    *   Light of the scene
    */
    public GameObject m_LightScene;

    /**
    *   Camera over the labyrinth
    */
    public GameObject m_CameraLabyrinth;

    /**
    *   Invisible wall at the entrance of the labyrinth
    */
    public GameObject m_EntranceLabyrinth;
    
    /**
    *   Number of players in the entrance
    */
    private int m_NumberPlayerEntrance = 0;


    /**
    *   Decrements the number of player when they exit the trigger
    *   @param  colliderObject  Object which exits the trigger
    *   @author  paul_chapelon
    */
    void OnTriggerExit(Collider colliderObject)
    {
        if(colliderObject.tag == "Player")
        {
            m_NumberPlayerEntrance --;
        }
    }

    /**
    *   Increments the number of player when they enter the trigger, activate all the parameters of the scene
    *   @param  colliderObject  Object which enters the trigger
    *   @author  paul_chapelon
    */
    void OnTriggerEnter(Collider colliderObject)
    {
        if(colliderObject.tag == "Player")
        {
            m_NumberPlayerEntrance ++;
        }
        //Search all players in the game 
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if(players.Length == m_NumberPlayerEntrance)
        {
            // Change the camera
            m_CameraLabyrinth.GetComponent<Camera>().enabled = true;
            m_LightScene.GetComponent<Light>().shadows = LightShadows.None;
            // For each player activate the gameplay for a 2D view
            foreach(GameObject player in players)
            {
                player.GetComponent<Transform>().rotation = new Quaternion(0,0,0,0);
                player.GetComponentInChildren<Camera>().enabled = false;
                player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                player.GetComponent<Movement>().m_CanLookAround = false;
                
            }
            // Open the entrance of the labyrinth
            m_EntranceLabyrinth.GetComponent<Collider>().enabled = false;

        }
        
    }
}
