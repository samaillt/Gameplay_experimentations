using UnityEngine;

/**
 * Caracteristic of an object which is wet and will interact with FireSensitive elements
 * @author tom_samaille
 */
public class WaterElement : MonoBehaviour
{
    /** 
     * m_EndExp refers to the EndExpermimentation gameObject
     */
    public GameObject m_EndExp;
    /**
     * OnCollisionEnter is called when the collider enters the GameObject range
     * @authors tom_samaille
     */
    void OnCollisionEnter(Collision collisionInfo)
    {
        GameObject collider = collisionInfo.gameObject;
        if (collider.GetComponent<FireSensitive>() != null)
        {
            collider.GetComponent<FireSensitive>().GettingWet();
            if (collider.transform.name == "Player")
            {
                if (!m_EndExp.GetComponent<EndLevel>().GetEndStatus())
                {
                    m_EndExp.GetComponent<EndLevel>().EndExperimentation("Le joueur bleu a touché le joueur rouge");
                }
            }
        }
    }
}
