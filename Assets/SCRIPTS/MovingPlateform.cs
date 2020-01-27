using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/**
 * MovingPlateform
 * 
 * The component used to handle player movements
 * 
 * @author julian_bruxelle
 * @date 2019, Dec. 11th
 */
public class MovingPlateform : MonoBehaviour
{
    /**
    * m_direction is the direction used for player horizontal movements
    */
    public Vector3 m_direction = new Vector3(1, 1, 1);

    /**
    * m_speed is the speed used for player horizontal movements
    */
    public float m_speed = 1.0f;

    /**
   * m_amplitude is the amplitude used for player horizontal movements
   */
    public float m_amplitude = 5.0f;

    ////////////////////////////////////////////////////////////

    private Vector3 m_originalPosition;

    /**
    * Start : is called before the first frame update
    * 
    * Initializes m_originalPosition with GameObject's position
    * 
    * @author julian_bruxelle
    * @date 2019, Dec. 11th
    */
    private void Start()
    {
        m_originalPosition = this.gameObject.transform.position;
    }

    /**
    * Update : is called at each frame update
    * 
    * Updated GameObject position in m_direction within m_amplitude range 
    * 
    * @author julian_bruxelle
    * @date 2019, Dec. 11th
    */
    private void Update()
    {
        float x = this.gameObject.transform.position.x;
        float y = this.gameObject.transform.position.y;
        float z = this.gameObject.transform.position.z;

        if (x < m_originalPosition.x - m_amplitude || x > m_originalPosition.x + m_amplitude || 
            y < m_originalPosition.y - m_amplitude || y > m_originalPosition.y + m_amplitude ||
            z < m_originalPosition.z - m_amplitude || z > m_originalPosition.z + m_amplitude
           )
        {
            m_speed *= -1;
        }

        this.gameObject.transform.position += new Vector3(m_speed * m_direction.x, m_speed * m_direction.y, m_speed * m_direction.z);
    }
}
