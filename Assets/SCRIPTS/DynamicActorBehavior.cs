using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    /**
    *   Description
    *   Read what are the possible behavior for the actor and launch the appropriate component
    *   @Simon Descôteaux
    */
public class DynamicActorBehavior : MonoBehaviour
{
    /**
    *   PRIVATE
    */
    private DynamicActorStats dynamicActorStats;
    public bool isActive = false;

    void Start()
    {
        dynamicActorStats = GetComponent<DynamicActorStats>();
    }

    public void CheckForActions()
    {
        isActive = true;

        if (dynamicActorStats.m_Hunger > dynamicActorStats.m_HungerThreshold)
        {
            GetComponent<DynamicActorIdle>().IdleAction();
            
        }
        if (dynamicActorStats.m_Hunger < dynamicActorStats.m_HungerThreshold)
        {
            GetComponent<DynamicActorHunger>().LookForFood();
        }

        dynamicActorStats.m_Hunger -= dynamicActorStats.m_HungerMalus;
        dynamicActorStats.m_HungerText.text = ("Hunger : " + dynamicActorStats.m_Hunger);
    }
}
