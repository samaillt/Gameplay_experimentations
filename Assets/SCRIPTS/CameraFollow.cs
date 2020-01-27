using UnityEngine;

/**
 * Following the player movement
 * Regroups every function or properties allowing an object to follow the player.
 * @author tom_samaille
 */
public class CameraFollow : MonoBehaviour
{
    /** 
     * m_Player refers to the player we want to follow.
     */
    public Transform m_Player;
    public Vector3 m_Offset = new Vector3(0f,1.5f,-5f);

    /**
     * Update is called once per frame
     * @author tom_samaille
     */
    void Update () {
        //Transforms the offset position into local position for the player and affects it to the camera's position
        Vector3 positionCamera = m_Player.TransformPoint(m_Offset);
        transform.position = positionCamera;
        //The camera keeps tracking the player target
        transform.LookAt(m_Player);
    }
}
