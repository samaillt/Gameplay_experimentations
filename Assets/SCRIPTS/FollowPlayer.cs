using UnityEngine;

/**
 * Following the player movement
 * Regroups every function or properties allowing an object to follow the player.
 * @author tom_samaille
 */
public class FollowPlayer : MonoBehaviour
{
    /** 
     * m_Player refers to the player we want to follow.
     */
    public Transform m_Player;

    /**
     * Update is called once per frame
     * @author tom_samaille
     */
    void Update()
    {
        transform.position = m_Player.position;
    }
}
