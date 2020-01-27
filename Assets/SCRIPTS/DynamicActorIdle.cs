using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
*   Description classe
*   State and idle behavior of dynamic actors
*   @Simon Descôteaux
*/
public class DynamicActorIdle : MonoBehaviour
{
    public enum MovementStates
    {
        IDLE,   /**< enum value IDLE State when the actor is not moving > */
        WALK    /**< enum value WALK State when the actor is moving at a normal pace */
    }

    public MovementStates m_MovementType; /// Compare desired state to the enum MovementStates

    /**  The following variables allow the actor those move within the constraint of a square
    They could ultimely be replaced by a NavMesh */
    public float m_MinDistance;
    public float m_MaxDistance;
    private bool m_IsMoving = false;
    private Vector3 m_WayPoint;
    private DynamicActorBehavior dynamicActorBehavior;
    private NavMeshAgent m_Agent;

    void Start()
    {
        dynamicActorBehavior = GetComponent<DynamicActorBehavior>();
        m_Agent = GetComponent<NavMeshAgent>();
    }

    public void IdleAction()
    {
        StartCoroutine(IdlingTarget());
    }

    IEnumerator IdlingTarget()
    {
        while (dynamicActorBehavior.isActive == true)
        {
            RandomIdle();

            if (!m_IsMoving)
            {
                if (m_MovementType == MovementStates.WALK)
                {
                    CreateWaypoint();

                    // Set anim state for walking
                    
                    if (m_IsMoving)
                    {
                        while (transform.position != m_WayPoint)
                        {
                            Movement();
                        }

                        m_IsMoving = false;         
                    }
                }

                if (m_MovementType == MovementStates.IDLE)
                {
                    // Set anim state for idle
                }
            }

            yield return dynamicActorBehavior.isActive = false;
        }
    }

    void RandomIdle()
    {
        int num = Random.Range(0, 2);
        if (num == 0)
        {
            m_MovementType = MovementStates.IDLE;
        }
        if (num == 1)
        {
            m_MovementType = MovementStates.WALK;
        }
    }

    void CreateWaypoint()
    {
        float distanceX = transform.position.x + Random.Range(m_MinDistance, m_MaxDistance);
        float distanceZ = transform.position.z + Random.Range(m_MinDistance, m_MaxDistance);
        m_WayPoint = new Vector3(distanceX, transform.position.y, distanceZ);

        m_IsMoving = true;
    }

    void Movement()
    {
        // m_Agent.SetDestination(m_WayPoint);
        // transform.position = Vector3.MoveTowards(transform.position, m_WayPoint, GetComponent<DynamicActorStats>().m_Speed * Time.deltaTime);
        // var rotation = Quaternion.LookRotation(m_WayPoint - transform.position);
        // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, GetComponent<DynamicActorStats>().m_RotationSpeed * Time.deltaTime);
    }
}
