using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


/**
*   The class Movement defines the controls of the player
*   @author tom_samaille
*   @author paul_chapelon
*/
public class PlayerKeyboardMovement : MonoBehaviour
{
    /** 
     * PlayerTransform refers to our camera linked player transform.
     */
    private Transform m_PlayerTransform;
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
    public float m_RotationSpeed = 2f;
    /** 
     * Camera refers to our player linked camera.
     */
    public Camera m_Camera;
    /** 
     * Speed refers to our player movement speed.
     */
    public float m_Speed = 5f;
    /** 
     * RigidBody refers to the rigidbody component of the object.
     */
    private Rigidbody m_RigidBody;
    /**
    *   Gamepad controller for the player 1
    */
    public Keyboard m_Keyboard;
    
    /**
     * Start is called before the first frame update
     * @author tom_samaille
     */
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_PlayerTransform = transform;
        m_CameraOffset = m_Camera.transform.position - m_PlayerTransform.position;
        m_Keyboard = InputSystem.GetDevice<Keyboard>();
    }

    /**
     * FixedUpdate is called once per frame. Used for physics on the attached object
     * @author paul_chapelon
     */
    void FixedUpdate()
    {
        MovePlayer();
    }

    /**
     * LateUpdate is called after Update methods.
     * @author tom_samaille
     */
    void LateUpdate()
    {
        MoveCameraAround();
    }

    /**
     * MovePlayer regroups instructions to move the player GameObject within the scene
     * @author tom_samaille
     */
    void MovePlayer()
    {
        float xAxisValue = (m_Keyboard.dKey.isPressed || m_Keyboard.rightArrowKey.isPressed) ? 1 : (m_Keyboard.aKey.isPressed || m_Keyboard.leftArrowKey.isPressed) ? -1 : 0;
        float yAxisValue = (m_Keyboard.wKey.isPressed || m_Keyboard.upArrowKey.isPressed) ? 1 : (m_Keyboard.sKey.isPressed || m_Keyboard.downArrowKey.isPressed) ? -1 : 0;

        // Calculate the forward vector
        Vector3 camForward_Dir = Vector3.Scale(m_Camera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = yAxisValue * camForward_Dir + xAxisValue * m_Camera.transform.right;
        if (move.magnitude > 1f)
        {
            move.Normalize();
        }
        m_RigidBody.AddForce(move * m_Speed);
    }
    
    /**
     * MovePlayer regroups instructions to move the camera linked to player GameObject within the scene
     * @author tom_samaille
     */
    void MoveCameraAround()
    {
        float movementCamera = Input.GetAxis("Mouse X");
        if(m_RotateAroundPlayer)
        {
            Vector3 cameraMove = new Vector3(movementCamera,0,0);
            cameraMove.Normalize();
            Quaternion camTurnAngle = Quaternion.AngleAxis(cameraMove.x * m_RotationSpeed, Vector3.up);
            m_CameraOffset = camTurnAngle * m_CameraOffset;
        }
        Vector3 newPos = m_PlayerTransform.position + m_CameraOffset;
        m_Camera.transform.position = newPos;
        if (m_LookAtPlayer || m_RotateAroundPlayer)
        {
            m_Camera.transform.LookAt(m_PlayerTransform);
        }
    }
}
