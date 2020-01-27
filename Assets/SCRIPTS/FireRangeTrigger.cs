using UnityEngine;

/**
 * Trigger event to check if some fire enteers tree range
 * Regroups every function or properties allowing to burn the element if fire enter its range.
 * @authors tom_samaille
 */
public class FireRangeTrigger : MonoBehaviour
{
    /** 
     * m_Tree refers to the (parent) GameObject attached to the range element.
     */
    public GameObject m_Tree;

    /**
     * Start is called before the first frame update
     * @authors tom_samaille
     */
    void Start()
    {
        m_Tree = transform.parent.gameObject; // Getting the parent of this range element
    }

    /**
     * Fire is the triggered function to call whenever the fire has to be increased on the element
     * @authors tom_samaille
     */
    private void Fire(Collider colliderInfo)
    {
        if (colliderInfo.GetComponent<FireSensitive>() != null && colliderInfo.GetComponent<FireSensitive>().GetInFire() > .7f && m_Tree.GetComponent<FireSensitive>().GetInFire() < 1)
        {
            m_Tree.GetComponent<FireSensitive>().StartFire();
        }
    }

    /**
     * Fire is the triggered function to call whenever the fire has to be increased on the element
     * @authors tom_samaille
     */
    private void LeavesFire(Collider colliderInfo)
    {
        if (colliderInfo.GetComponent<FireSensitive>() != null && colliderInfo.GetComponent<FireSensitive>().GetInFire() > .7f && m_Tree.GetComponent<FireSensitive>().GetInFire() < 0.7)
        {
            m_Tree.GetComponent<FireSensitive>().StopFire();
        }
    }

    /**
     * OnTriggerEnter is called when the collider enters the GameObject range
     * @authors tom_samaille
     */
    void OnTriggerEnter(Collider colliderInfo)
    {
        // Debug.Log("Enter");
        Fire(colliderInfo);
    }
    
    /**
     * OnTriggerEnter is called when the collider stays the GameObject range
     * @authors tom_samaille
     */
    private void OnTriggerStay(Collider colliderInfo)
    {
        // Debug.Log("Stay");
    }

    /**
     * OnTriggerExit is called when the collider leaves the GameObject range
     * @authors tom_samaille
     */
    private void OnTriggerExit(Collider colliderInfo)
    {
        // Debug.Log("Exit");
        LeavesFire(colliderInfo);
    }
}
