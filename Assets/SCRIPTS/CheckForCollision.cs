using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForCollision : MonoBehaviour
{
    private bool movingDown;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Resources")
        {

        }
    }
}
