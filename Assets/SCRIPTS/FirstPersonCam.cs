using UnityEngine;
using System.Collections;

/**
 * FirstPersonCam
 * 
 * The component used to handle camera rotation with mouse
 * 
 * @author https://gamedev.stackexchange.com/questions/104693/how-to-use-input-getaxismouse-x-y-to-rotate-the-camera
 * @author julian_bruxelle
 * @date 2019, Dec. 12th
 */

public class FirstPersonCam : MonoBehaviour
{
    /**
    * m_xRotateSpeed - Speed for X axis camera rotation
    */
    public float m_xRotateSpeed = 2.0f;

    /**
    * m_yRotateSpeed - Speed for Y axis camera rotation
    */
    public float m_yRotateSpeed = 2.0f;

    /**
    * m_yaw - Initial camera m_yaw (X rotation)
    */
    private float m_yaw = 0.0f;

    /**
    * m_pitch - Initial camera pitch (Y rotation)
    */
    private float m_pitch = 0.0f;

    /**
    * Update : is called at each frame update
    * 
    * Handles camera rotation on mouse click and move
    * 
    * @author https://gamedev.stackexchange.com/questions/104693/how-to-use-input-getaxismouse-x-y-to-rotate-the-camera
    * @author julian_bruxelle
    * @date 2019, Dec. 12th
    */
    private void Update()
    {
        m_yaw += m_xRotateSpeed * Input.GetAxis("Mouse X");
        m_pitch -= m_yRotateSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(m_pitch, m_yaw, 0.0f);
    }
}