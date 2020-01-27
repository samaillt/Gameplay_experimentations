using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


/**
 *   The class Movement defines the controls of the player
 *   @author tom_samaille
 */
public class PlayerControllerMovement : MonoBehaviour
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
    public float m_RotationSpeed = 1.5f;
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
    *   Radius of the circle where the stick of the gamepad is not detected
    */
    public float m_DeadZoneStick = .2f;
    /**
    *   Gamepad controller for the player 1
    */
    public Gamepad m_Gamepad;
    
    /**
     * Start is called before the first frame update
     * @author tom_samaille
     */
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_PlayerTransform = transform;
        m_CameraOffset = m_Camera.transform.position - m_PlayerTransform.position;
        m_Gamepad = InputSystem.GetDevice<Gamepad>();
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
        Vector2 movementPlayer = new Vector2(m_Gamepad.leftStick.x.ReadValue(), m_Gamepad.leftStick.y.ReadValue());
        if (!ValueInDeadZone(movementPlayer.x, movementPlayer.y))
        {
            // Calculate the forward vector
            Vector3 camForward_Dir = Vector3.Scale(m_Camera.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 move = movementPlayer.y * camForward_Dir + movementPlayer.x * m_Camera.transform.right;
            if (move.magnitude > 1f)
            {
                move.Normalize();
            }
            m_RigidBody.AddForce(move * m_Speed);
        }
    }

    /**
     * MovePlayer regroups instructions to move the camera linked to player GameObject within the scene
     * @author tom_samaille
     */
    void MoveCameraAround()
    {
        float movementCamera = m_Gamepad.rightStick.x.ReadValue();
        if (!ValueInDeadZone(movementCamera))
        {
            if(m_RotateAroundPlayer)
            {
                Vector3 cameraMove = new Vector3(movementCamera,0,0);
                cameraMove.Normalize();
                Quaternion camTurnAngle = Quaternion.AngleAxis(cameraMove.x * m_RotationSpeed, Vector3.up);
                m_CameraOffset = camTurnAngle * m_CameraOffset;
            }
        }
        Vector3 newPos = m_PlayerTransform.position + m_CameraOffset;
        m_Camera.transform.position = newPos;
        if (m_LookAtPlayer || m_RotateAroundPlayer)
        {
            m_Camera.transform.LookAt(m_PlayerTransform);
        }
    }

    /**
     * ValueInDeadZone 
     * Indicates if the values are in the specified deadzone
     * @param valueStickX float argument correponding to the value of X axis of the stick.
     * @param valueStickY float argument correponding to the value of Y axis of the stick.
     * @return bool as true if the value is inside the specified deadzone, false otherwise
     * @author tom_samaille
     */
    private bool ValueInDeadZone(float valueStickX, float valueStickY)
    {
        return (Mathf.Sqrt(Mathf.Pow(valueStickX,2) + Mathf.Pow(valueStickY,2)) < m_DeadZoneStick);
    }

    /**
     * ValueInDeadZone 
     * Indicates if a value is in the specified deadzone
     * @param value float argument we want to check if it's inside deadzone of the stick.
     * @return bool as true if the value is inside the specified deadzone, false otherwise
     * @author tom_samaille
     */
    private bool ValueInDeadZone(float value)
    {
        return (value > -m_DeadZoneStick && value < m_DeadZoneStick);
    }
}
