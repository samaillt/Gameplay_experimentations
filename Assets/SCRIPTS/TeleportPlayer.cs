using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*   The class TeleporterPlayer defines a one way teleporter
*   @author paul_chapelon
*/
public class TeleportPlayer : MonoBehaviour
{
    /**
    *   The teleporter target 
    */
    public GameObject m_TeleporterPlayer;

    /**
    *   Teleports the object on it to the teleporter target
    *   @param  colliderObject  Object which triggers the teleporter
    *   @author paul_chapelon
    */
    void OnTriggerEnter(Collider colliderObject)
    {
        if(colliderObject.gameObject.tag == "Player")
        {
            colliderObject.transform.position = m_TeleporterPlayer.transform.position;
        }
    }
}
