using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.AI;

/**
*   Class Description: 
*   @Simon Descôteaux
*/
public class DynamicActorHunger : MonoBehaviour
{
    // private NavMeshAgent navMesh;
    private bool foodFound;
    private GameObject foodTarget;

    void Start()
    {
        // navMesh = GetComponent<NavMeshAgent>();
    }

    public void LookForFood()
    {
        Debug.Log("Move toward food");

        StartCoroutine(MoveTo());
    }

    IEnumerator MoveTo()
    {
        while(!foodFound)
        {
            //navMesh.destination = transform.position;
            yield return 0;
        }
    }

    void OnTriggerEnter(Collider collider)
    {

    }
}
