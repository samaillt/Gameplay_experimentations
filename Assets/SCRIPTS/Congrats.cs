using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Congrats
 * 
 * The component used to launch congrats animation on victory
 * @author julian_bruxelle
 * @date 2019, Dec. 11th
 */

public class Congrats : MonoBehaviour
{
    /**
     * m_congratsParticleSystem - The particle system that will be used for player congrats animation
     */
    [SerializeField] ParticleSystem m_congratsParticleSystem = null;

    /**
    * m_player - The player whose score should be checked for congratulations
    */
    [SerializeField] GameObject m_player = null;

    /**
    * m_trophy - The GameObject used as trophy
    */
    [SerializeField] GameObject m_trophy = null;

    /**
    * m_playerGameModule - Used to store player PlayerMotor and avoiding getting it several times
    */
    private PlayerGameModule m_playerGameModule = null;

    /**
    * m_animationLauched - Prevents congrats animation from being launched several times
    */
    private bool m_animationLaunched = false;

    /**
    * Start : is called before the first frame update
    * 
    * Initializes m_playerGameModule with player PlayerGameModule
    * Sets the trophy to invisible
    * 
    * @author julian_bruxelle
    * @date 2019, Dec. 11th
    */
    private void Start()
    {
        if (m_player != null)
        {
            m_playerGameModule = m_player.GetComponent<PlayerGameModule>();
            if (m_playerGameModule == null)
            {
                Debug.LogWarning("No PlayerGameModule attached to the congrats component.");
            }
        } else
        {
            Debug.LogWarning("No player GameObject attached to the congrats component.");
        }
        
        if (m_trophy != null)
        {
            m_trophy.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        } else
        {
            Debug.LogWarning("No trophy set for congrats.");
        }
    }

    /**
    * Update : is called at each frame update
    * 
    * Launches animation (once) if player score matches victory score limit
    * 
    * @author julian_bruxelle
    * @date 2019, Dec. 11th
    */
    private void Update()
    {
        if (m_player != null && m_playerGameModule != null) 
        {
            if (m_playerGameModule.GetScore() == m_playerGameModule.GetVictoryLimitScore() && !m_animationLaunched)
            {
                StartCoroutine(LaunchAnimation());
            }
        }
    }

    /**
    * SetPlayerCameraLookAt : makes the player follow camera look at 
    * the given parameter transform
    * 
    * @param targetTransform - The transform the camera has to look at
    * 
    * @author julian_bruxelle
    * @date 2019, Dec. 11th
    */
    private void SetPlayerCameraLookAt(Transform targetTransform)
    {
        Transform followCameraTransform = m_player.transform.Find("FollowCamera");
        if (followCameraTransform != null)
        {
            followCameraTransform.LookAt(targetTransform);

        } else {
            Debug.LogWarning("Player follow camera does not match name 'Follow Camera'.");
        }
    }

    /**
    * LaunchAnimation : plays the congrats animation
    * 
    * @return animation coroutine
    * 
    * @author julian_bruxelle
    * @date 2019, Dec. 11th
    */
    private IEnumerator LaunchAnimation()
    {
        yield return new WaitForSeconds(1);
        m_congratsParticleSystem.Play();
        SetPlayerCameraLookAt(this.gameObject.transform);
        
        m_animationLaunched = true;
        if (m_trophy != null)
        {
            yield return new WaitForSeconds(3.0f);
            for (int i = 1; i <= 10; ++i)
            {
                yield return new WaitForSeconds(0.1f);
                m_trophy.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, (float)i/10.0f);

            }
        }
    }
}
