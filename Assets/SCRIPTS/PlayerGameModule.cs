using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/**
 * PlayerGameModule
 * 
 * The component used store player life, score and operate on it
 * 
 * @author julian_bruxelle
 * @date 2019, Dec. 11th
 */
public class PlayerGameModule : MonoBehaviour
{
    /**
    * m_lifeSlider - The slider for player life display
    */
    public Slider m_lifeSlider = null;

    /**
    * m_scoreText - Displays player score as a text
    */
    public Text m_scoreText = null;

    /**
    * m_victoryPanel - Displays the victory message on player victory
    */
    public GameObject m_victoryPanel = null;

    /**
    * m_defeatPanel - Displays the defeat message on player defeat
    */
    public GameObject m_defeatPanel = null;

    ////////////////////////////////////////////////////////////

    /**
    * m_victoryLimitScore - The score for which player will win the game
    */
    private int m_victoryLimitScore;

    /**
    * m_life - Player's remaining life
    */
    private int m_life = 200;

    /**
    * m_score - Player's score
    */
    private int m_score = 0;

    /**
    * m_EndTimer refers to a timer at the end of the experimentation
    */
    private float m_EndTimer = 5.0f; // Time in second

    /**
    * m_ExpEnded is true only when experimentation has ended
    */
    private bool m_ExpEnded = false;

    /**
    * Start : is called before the first frame update
    * 
    * Initializes class attributes and components
    * 
    * @author julian_bruxelle
    * @date 2019, Dec. 11th
    */
    private void Start()
    {
        if (m_lifeSlider == null)
        {
            throw new MissingComponentException("Life slider missing.");
        }
        if (m_scoreText == null)
        {
            throw new MissingComponentException("Score text missing.");
        }
        if (m_victoryPanel == null)
        {
            throw new MissingComponentException("Victory text missing.");
        }
        if (m_defeatPanel == null)
        {
            throw new MissingComponentException("Defeat text missing.");
        }

        m_victoryLimitScore = GameObject.FindGameObjectsWithTag("Bonus").Length;

        SetScoreText();
        SetLifeSliderValue();

        m_victoryPanel.SetActive(false);
        m_defeatPanel.SetActive(false);
    }

    /**
    * Update : is called at each frame update
    * 
    * Handles player death if players falls into the void
    * 
    * @author julian_bruxelle
    * @date 2019, Dec. 12th
    */
    private void Update()
    {
        if (this.transform.position.y < -5 && m_life > 0)
        {
            m_life = 0;
            DecreaseLife();
        }
        if (m_ExpEnded)
        {
             if (m_EndTimer < 0)
            {
                gameObject.GetComponent<FindGameObject>().FindTheSingleton(0);
            } 
            else
            {
                m_EndTimer -= Time.deltaTime;
            }
        }
    }

    /**
    * GetScore : gets player's current score
    * 
    * @return score value
    * 
    * @author julian_bruxelle
    * @date 2019, Dec. 11th
    */
    public int GetScore()
    {
        return m_score;
    }

    /**
    * GetVictoryLimitScore : gets victory limit score
    * 
    * @return limit score value
    * 
    * @author julian_bruxelle
    * @date 2019, Dec. 11th
    */
    public int GetVictoryLimitScore()
    {
        return m_victoryLimitScore;
    }

    /**
    * SetScoreText : sets m_scoreText text content to current player's score
    * 
    * @author julian_bruxelle
    * @date 2019, Dec. 11th
    */
    public void SetScoreText()
    {
        m_scoreText.text = "SCORE : " + m_score.ToString() + " / " + m_victoryLimitScore;
    }

    /**
    * SetLifeSliderValue : sets m_lifeSlider value to current player's life
    * 
    * @author julian_bruxelle
    * @date 2019, Dec. 11th
    */
    public void SetLifeSliderValue()
    {
        m_lifeSlider.value = m_life;
    }

    /**
    * DisableController : disables player's CharacterController
    * 
    * Used at the end of game
    * 
    * @author julian_bruxelle
    * @date 2019, Dec. 11th
    */
    private void DisableController()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
        } else
        {
            // Should throw exception if reused in the game.
            Debug.Log("No controller assigned to the player. Can't disable controller.");
        }
    }

    /**
    * DecreaseLife : decreases player's life value
    * 
    * Ends the game when player's life reaches 0
    * 
    * @author julian_bruxelle
    * @date 2019, Dec. 11th
    */
    public void DecreaseLife()
    {
        if (m_life > 0)
        {
            m_life--;
        } 
        if (m_life == 0 && m_score != m_victoryLimitScore)
        {
            m_defeatPanel.SetActive(true);
            DisableController();
            m_ExpEnded = true;
        }
    }

    /**
    * IncreaseScore : increases player's score value
    * 
    * Ends the game if player reaches the victory limit score
    * 
    * @return score increase coroutine
    *
    * @author julian_bruxelle
    * @date 2019, Dec. 11th
    */
    public IEnumerator IncreaseScore()
    {
        if (m_score <= m_victoryLimitScore)
        {
            ++m_score;
            if (m_score == m_victoryLimitScore)
            {
                yield return new WaitForSeconds(7);
                m_victoryPanel.SetActive(true);
                DisableController();
                m_ExpEnded = true;
            }
        }

    }

}
