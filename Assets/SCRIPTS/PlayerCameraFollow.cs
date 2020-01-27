using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * PlayerFollow of the player by the camera
 * Regroups every function or properties allowing camera to properly follow player. Thanks to the tutorial of Jayanam : https://www.youtube.com/watch?v=urNrY7FgMao
 * @author tom_samaille
 */
public class PlayerCameraFollow : MonoBehaviour
{
    /** 
     * PlayerTransform refers to our camera linked player transform.
     */
    public Transform m_PlayerTransform;
    /** 
     * CameraOffset refers to the offset of the camera with the player.
     */
    private Vector3 m_CameraOffset;
    /** 
     * LookAtPlayer if camera is looking at the player.
     */
    public bool m_LookAtPlayer = false;
    /** 
     * RotateAroundPlayer if camera has to turn around the player.
     */
    public bool m_RotateAroundPlayer = true;
    /** 
     * RotationsSpeed refers to the speed of camera rotation around player.
     */
    public float m_RotationsSpeed = 2.5f;

	/**
     * Start is called before the first frame update
     * @author tom_samaille
     */
	void Start ()
    {
        m_CameraOffset = transform.position - m_PlayerTransform.position;	
	}
	
    /**
     * LateUpdate is called after Update methods.
     * @author tom_samaille
     */
	void LateUpdate ()
    {
        if(m_RotateAroundPlayer)
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * m_RotationsSpeed, Vector3.up);
            m_CameraOffset = camTurnAngle * m_CameraOffset;
        }
        Vector3 newPos = m_PlayerTransform.position + m_CameraOffset;
        transform.position = newPos;
        if (m_LookAtPlayer || m_RotateAroundPlayer)
        {
            transform.LookAt(m_PlayerTransform);
        }
	}
}