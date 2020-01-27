using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/**
 * PlayerMotor
 * 
 * The component used to handle player movements
 * 
 * @author julian_bruxelle
 * @date 2019, Dec. 11th
 */
public class PlayerMotor : MonoBehaviour
{
    /**
    * m_speed is the speed used for player horizontal movements
    */
    public float m_speed = 10.00f;

    ////////////////////////////////////////////////////////////

    /**
    * m_controller - The CharacterController used for player movements
    */
    private CharacterController m_controller;

    /**
    * m_verticalVelocity - Used to control player jumps and falls speed
    */
    private float m_verticalVelocity;

    /**
    * m_gravityStrength - The gravity strength used for vertical attraction when player falls down
    */
    private float m_gravityStrength = 14.0f;

    /**
    * m_jumpForce - The force used to calculate player jumps height 
    */
    private float m_jumpForce = 7f;


    /**
    * Start : is called before the first frame update
    * 
    * Initializes m_controller with player PlayerGameModule
    * 
    * @throws MissingComponentException
    * 
    * @author julian_bruxelle
    * @date 2019, Dec. 11th
    */
    private void Start()
    {
        m_controller = GetComponent<CharacterController>();
        if (m_controller == null)
        {
            throw new MissingComponentException("CharacterController missing for player.");
        }
    }

    /**
    * Update : is called at each frame update
    * 
    * Handles player movements according to inputs
    * 
    * @author julian_bruxelle
    * @date 2019, Dec. 11th
    */
    private void Update()
    {
        if (m_controller.enabled)
        {
            if (m_controller.isGrounded)
            {
                m_verticalVelocity = -m_gravityStrength * Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_verticalVelocity = m_jumpForce;
                }
            }
            else
            {
                m_verticalVelocity -= m_gravityStrength * Time.deltaTime;
            }

            Vector3 moveVector = new Vector3(
                Input.GetAxis("Horizontal") * m_speed,
                0,
                Input.GetAxis("Vertical") * m_speed
            );

            Vector3 localMoveVector = transform.TransformDirection(moveVector);
            localMoveVector.y = m_verticalVelocity;
            
            m_controller.Move(localMoveVector * Time.deltaTime);
        }
    }
}
