using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * SceneTiming functions
 * Regroups every function or properties allowing to set a timer to the scene.
 * @author tom_samaille
 */
public class SceneTiming : MonoBehaviour
{
    /** 
     * m_EndExp refers to the EndExpermimentation gameObject
     */
    public GameObject m_EndExp;
    /** 
     * m_TimerDuration is a float value (in second) which represents maximum duration the scene.
     */
    public float m_TimerDuration = 300f; // Initialized to 5 minutes

    /** 
     * m_Timer is a float value (in second) which represents actual timer.
     */
    private float m_Timer = .0f;

    /**
     * Update is called once per frame.
     * @author tom_samaille
     */
    void Update()
    {
        if (m_Timer >= m_TimerDuration)
        {
            if (!m_EndExp.GetComponent<EndLevel>().GetEndStatus())
            {
                m_EndExp.GetComponent<EndLevel>().EndExperimentation("Temps maximal dépassé");
            }
        }
        else 
        {
            m_Timer += Time.deltaTime;
        }
    }

    /**
     * SetTimer a setter for m_Timer attribute.
     * @param time a float referring to a value (in second) we'll set for m_Timer attribute
     * @author tom_samaille
     */
    public void SetTimer(float time)
    {
        m_Timer = time;
    }

    /**
     * GetTimer a getter for m_Timer attribute.
     * @return a float corresponding to the actual timing in the scene
     * @author tom_samaille
     */
    public float GetTimer()
    {
        return m_Timer;
    }
}
