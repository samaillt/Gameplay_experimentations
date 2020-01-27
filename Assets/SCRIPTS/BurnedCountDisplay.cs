using System;
using System.Linq;
using UnityEngine;

/**
 * BurnedCountDisplay functions
 * Regroups every function or properties to count pourcentage of burned trees and displaying it.
 * @author tom_samaille
 */
public class BurnedCountDisplay : MonoBehaviour
{
    /** 
     * m_EndExp refers to the EndExpermimentation gameObject
     */
    public GameObject m_EndExp;
    /** 
     * m_Ground refers to the Ground gameObject witch is used to generate the trees
     */
    public GameObject m_Ground;
    /** 
     * m_TreeNumber is an integer referring to the number of burned trees
     */
    private int m_BurnedTreeNumber = 0;
    /** 
     * m_Pourcent is the pourcentage of burned trees among all
     */
    private float m_Pourcent;

    /**
     * Update is called once per frame.
     * @author tom_samaille
     */
    public void IncrementBurnedTree()
    {
        m_BurnedTreeNumber += 1;
        m_Pourcent = (float)m_BurnedTreeNumber * 100 / (float)m_Ground.GetComponent<TreeGeneration>().GetTreeNumber();
        if (m_BurnedTreeNumber == m_Ground.GetComponent<TreeGeneration>().GetTreeNumber())
        {
            if (!m_EndExp.GetComponent<EndLevel>().GetEndStatus())
            {
                m_EndExp.GetComponent<EndLevel>().EndExperimentation("Le joueur rouge a enflammé toute la forêt");
            }
        }
    }

    /**
     * OnGUI is called once frame draws.
     * @author tom_samaille
     */
	void OnGUI()
	{
        int w = Screen.width, h = Screen.height;
 
		GUIStyle style = new GUIStyle();
 
		Rect rect = new Rect(w * 89 / 100, h * 94 / 100, w * 10 / 100, h * 4 / 100);
		style.alignment = TextAnchor.LowerRight;
		style.fontSize = h * 4 / 100;
		style.normal.textColor = new Color (0.5f, 0.0f, 0.0f, 1.0f);
		string text = string.Format("{0:0.0} %", m_Pourcent);
		GUI.Label(rect, text, style);
    }

    /**
     * GetPourcent a getter for m_Pourcent attribute as string.
     * @return a string corresponding to the actual pourcent of burned trees in the scene
     * @author tom_samaille
     */
    public string GetPourcent()
    {
        return string.Format("{0:0.0} %", m_Pourcent);
    }
}
