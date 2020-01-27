using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
*   The class Multiplayer manages all the players in the game
*   @author  paul_chapelon
*/


public class Multiplayer : MonoBehaviour
{    
    /**
    *   The panel displayed to join 
    */
    public GameObject m_PanelJoinPlayer;

    /**
    *   The text displayed to join 
    */
    public GameObject m_TextJoinPlayer;

    /**
    *   The current number in the game
    */
    int m_NumberPlayer = 0;
    
    /**
    *   Define the colors the player can have when he joins the game
    */
    string[] m_ColorsArray = {"#FF0B00","#1600FF","#FFFE00", "#15AE1B"};

    /**
    *   Shuffle the array to have different colors when the player joins the game
    *   @author  paul_chapelon
    */
    void Start()
    {
        for (int i = 0; i < m_ColorsArray.Length - 1; i++)
        {
            int j = Random.Range(i, m_ColorsArray.Length);
            string colorTemp = m_ColorsArray[i];
            m_ColorsArray[i] = m_ColorsArray[j];
            m_ColorsArray[j] = colorTemp;
        }
        
    }


    /**
    *   Function triggered when a player joins the game, a color is affected to the player 
    *   @author  paul_chapelon
    */
    void OnPlayerJoined()
    {
        Debug.Log("A player has joined");
        m_PanelJoinPlayer.SetActive(false);
        m_TextJoinPlayer.SetActive(false);
        
        m_NumberPlayer ++;
        GameObject[] objectsArray = GameObject.FindGameObjectsWithTag("Player");
        Color colorPlayer;
        // Tries to affect the color code inside the array to a Color variable
        if(ColorUtility.TryParseHtmlString( m_ColorsArray[m_NumberPlayer - 1], out colorPlayer))
        {
            objectsArray[m_NumberPlayer - 1].GetComponent<Renderer>().material.color = colorPlayer; 
        }
        else
        {
            // Color by default in case the condition fails
            objectsArray[m_NumberPlayer - 1].GetComponent<Renderer>().material.color = Color.red;
        }     
    }

    /**
    *   Function triggered when a player leave the game, decrease the number of players
    *   @author  paul_chapelon 
    */
    void OnPlayerLeft()
    {
        Debug.Log("A player has left");
        m_NumberPlayer --;
    }
}
