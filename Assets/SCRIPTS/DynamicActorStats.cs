using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
*   Class Description
*   Holder class for the statistic of the actor
*   @Simon Descôteaux
*/
public class DynamicActorStats : MonoBehaviour
{
    public float m_Speed; /// Asset speed movement. Multiply with Time.deltaTime
    public float m_RotationSpeed; /// Asset rotation speed around its pivot point
    public int m_Hunger; /// the level of hunger of the actor
    public int m_HungerMalus; /// how much hunger is removed for int hunger at each action
    public int m_HungerThreshold; /// the threshold that determine when an actor is hungry
    public Text m_HungerText; /// Text object that render the Hunger level on the screen
    public float m_LookRadius;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_LookRadius);

    }
}