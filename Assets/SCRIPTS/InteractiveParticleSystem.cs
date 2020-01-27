using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * InteractiveParticleSystem
 * 
 * The component used to handle collisions between player and particle system
 * 
 * @author julian_bruxelle
 * @date 2019, Dec. 11th
 */
public class InteractiveParticleSystem : MonoBehaviour
{
    /**
     * m_pickable - Tells whether the particle system is pickable
    */
    public bool m_pickable = false;

    ////////////////////////////////////////////////////////////

    /**
    * m_particleSystem - The ParticleSystem to interact with
    */
    private ParticleSystem m_particleSystem;

    /**
    * m_collisionEvents - The list of collisions events detected
    */
    private List<ParticleCollisionEvent> m_collisionEvents;

    /**
    * m_pickable - Tells whether the particle system has been picked (if pickable)
    */
    private bool m_picked = false;

    /**
    * m_collisionsCount - Counts total collisions between particles of the ParticleSystem and the player
    */
    private int m_collisionsCount = 0;

    /**
    * Start : is called before the first frame update
    * 
    * Initializes m_particleSystem with GameObject's ParticleSystem
    * 
    * @author julian_bruxelle
    * @date 2019, Dec. 11th
    */
    private void Start() 
    {
        m_particleSystem = GetComponent<ParticleSystem>();
        if (m_particleSystem == null)
        {
            throw new MissingComponentException("ParticleSystem missing for " + this.gameObject.name + ".");
        }
        m_collisionEvents = new List<ParticleCollisionEvent>();
    }

    /**
    * ChangeParticleSystemOwner : changes ParticleSystem parent and position
    * 
    * @param newOwner - The GameObject to which the ParticleSystem will be attached
    * 
    * @author julian_bruxelle
    * @date 2019, Dec. 11th
    */
    private void ChangeParticleSystemOwner(GameObject newOwner)
    {
        m_particleSystem.transform.position = newOwner.transform.position;
        m_particleSystem.transform.parent = newOwner.transform; 
    }

    /**
    * CaptureParticleSystem : Makes a player capture the particle system
    * 
    * ParticleSystem is destroyed once captured
    * 
    * @param player - The player who will capture the ParticleSystem
    * 
    * @return capture coroutine
    * 
    * @author julian_bruxelle
    * @date 2019, Dec. 11th
    */
    private IEnumerator CaptureParticleSystem(GameObject player)
    {
        ChangeParticleSystemOwner(player);
        yield return new WaitForSeconds(1);
        m_particleSystem.Stop();
        yield return new WaitForSeconds(1);
        Destroy(m_particleSystem);
    }

    /**
    * OnParticleCollision : Handles particles collisions with player
    * 
    * Implies modifications of life and score values
    * 
    * @param player - The player who collides with particles
    * 
    * @author julian_bruxelle
    * @date 2019, Dec. 11th
    */
    private void OnParticleCollision(GameObject player) 
    {
        int numCollisionEvents = m_particleSystem.GetCollisionEvents(player, m_collisionEvents);
        m_collisionsCount += numCollisionEvents;

        PlayerGameModule playerGameModule = player.GetComponent<PlayerGameModule>();

        if (playerGameModule != null)
        {
            if (!m_pickable && numCollisionEvents >= 1)
            {
                playerGameModule.DecreaseLife();
                playerGameModule.SetLifeSliderValue();
            }
            if (m_pickable && m_collisionsCount >= 20 && !m_picked) 
            {
                StartCoroutine(CaptureParticleSystem(player));
                m_picked = true;
                StartCoroutine(playerGameModule.IncreaseScore());
                playerGameModule.SetScoreText();
            }
        }
    }
}