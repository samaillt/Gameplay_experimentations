using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    /** 
     * m_TimerDuration is a float value (in second) which represents maximum duration the scene.
     */
    public float m_TimerDuration = 300f; // Initialized to 5 minutes
    
    [SerializeField]
    private float m_CurrentTime;

    void Start()
    {
        m_CurrentTime = m_TimerDuration;
    }
    void Update()
    {

            if(m_CurrentTime <= 0.0f)
            {
                gameObject.GetComponent<FindGameObject>().FindTheSingleton(0);
            }
            else
            {
                m_CurrentTime -= Time.deltaTime;
            }      
    }
}
