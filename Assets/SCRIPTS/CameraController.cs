using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetTransform;


    void LateUpdate() 
    {
        Vector3 tempVec3 = new Vector3();
            tempVec3.x = transform.position.x;
            tempVec3.y = transform.position.y;
            tempVec3.z = targetTransform.transform.position.z;

        transform.position = tempVec3;
    }
}
