using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


/**
*   The class Movement defines the controls of the player
*   @author paul_chapelon
*/
public class Movement : MonoBehaviour
{   
    /**
    *   True if the camera can move around the player
    */
    public bool m_CanLookAround = true;
    /**
    *   The speed when the player moves
    */
    public float m_Speed = 5;

    /**
    *   The speed when the player looks around
    */
    public float m_SpeedRotation = 3 ;

    /**
    *   The power of the jump of the player
    */
    public int m_HeightJump = 265;

    /**
    *   Radius of the circle where the stick value of the gamepad is not detected
    */
    public float m_DeadZoneStick = 0.3f;

    /**
    *   True if the player is on the ground
    */
    private bool m_OnGround = true;

    /**
    *   The component rigidbody of the player
    */
    private Rigidbody m_RigidBody;

    /**
    *   The component which define the inputs for the player
    */
    private PlayerInputs m_Controls;

    /**
    *   Define the vector movement of the camera
    */
    private Vector2 m_CameraLookPlayer;

    /**
    *   Define the vector movement of the player
    */
    private Vector2 m_MovementPlayer;
    
    /**
    *   Initialize the inputs
    *   @author  paul_chapelon
    */
    void Awake()
    {
        m_Controls = new PlayerInputs();
    }

    /**
    *   Enable the inputs scheme for the player
    *   @author  paul_chapelon
    */
    void OnEnable()
    {
        m_Controls.Player.Enable();
    }

    /**
    *   Disable the inputs scheme for the player
    *   @author  paul_chapelon
    */
    void OnDisable()
    {
        m_Controls.Player.Disable();
    }
    
    /**
    *   Initialize the Rigibody of the player and his vector movement by zero
    *   @author  paul_chapelon
    */
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_MovementPlayer = new Vector2(0,0);
    }

    

    /**
    *   Move the player and camera at each frame
    *   @author  paul_chapelon
    */
    void Update()
    {
        MovePlayer();
        MoveCameraAround();
    }

    /**
    *   Move the player according to the deadzone of the sticks
    *   @author  paul_chapelon
    */
    void MovePlayer()
    {   
        Vector3 movement = ValueInDeadZone(m_MovementPlayer.x, m_MovementPlayer.y) ? new Vector3(0,0,0) : new Vector3(m_MovementPlayer.x,0,m_MovementPlayer.y);
        movement.Normalize();
        transform.Translate(movement * m_Speed * Time.deltaTime);
    }

    /**
    *   Move the camera around the player according to the deadzone of the sitck
    *   @author  paul_chapelon
    */
    void MoveCameraAround()
    {
        if(m_CanLookAround)
        {
            Vector3 cameraMove = ValueInDeadZone(m_CameraLookPlayer.x, m_CameraLookPlayer.y) ? new Vector3(0,0,0) : new Vector3(m_CameraLookPlayer.x,0,m_CameraLookPlayer.y);

            cameraMove.Normalize();
            gameObject.transform.Rotate(new Vector3(0.0f, cameraMove.x * m_SpeedRotation, 0.0f), Space.World);
        }        
    }

    /**
    *   Calcul if the stick is in the deadzone or not
    *   @param valueStickX  Value of the stick on the X axe 
    *   @param valueStickY  Value of the stick on the Y axe
    *   @author  paul_chapelon
    */
    private bool ValueInDeadZone(float valueStickX, float valueStickY)
    {
        return Mathf.Sqrt(Mathf.Pow(valueStickX,2) + Mathf.Pow(valueStickY,2)) < m_DeadZoneStick ?  true :  false;
    }

    /**
    *   Function used by the controller which add a force to the player to jump
    *   @author  paul_chapelon
    */
    public void OnJump()
    {
    
        if(m_OnGround)
        {
            if(m_RigidBody)
            {
                m_RigidBody.AddForce(0,m_HeightJump ,0);
                m_OnGround = false;
            }
        }
    }

    /**
    *   Function used by the controller which get the value of the stick for the movement of the player
    *   @param  value  Value emitted by the controller
    *   @author  paul_chapelon
    */
    public void OnMove(InputValue value)
    {
        m_MovementPlayer = value.Get<Vector2>();
    }

    /**
    *   Function used by the controller which get the value of the stick for the movement of the camera
    *   @param  value  Value emitted by the controller
    *   @author  paul_chapelon
    */
    public void OnCameraLook(InputValue value)
    {   
        m_CameraLookPlayer = value.Get<Vector2>();
    }

    /**
    *   Check if the player is on the ground to jump
    *   @param  collision  Object in contact with the player
    *   @author  paul_chapelon
    */
    void OnCollisionEnter(Collision collision)
    {
        if(!m_OnGround && ( collision.gameObject.tag == "Player" || (collision.transform.parent && ( collision.transform.parent.gameObject.tag =="Plateform" || collision.transform.parent.gameObject.tag == "Ground"))))
        {
            m_OnGround = true;
        } 
    }

    

}



