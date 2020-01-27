using UnityEngine;

/**
 * Caracteristic of a camera that doesn't display shadow
 * @author tom_samaille
 */
public class WithoutShadow : MonoBehaviour
{
    /** 
     * m_StoredShadowDistance refers to the distance of shadow to display.
     */
    float m_StoredShadowDistance;

    /**
     * OnPreRender is called when calculating pre render
     * @authors tom_samaille
     */
    void OnPreRender() {
        m_StoredShadowDistance = QualitySettings.shadowDistance;
        QualitySettings.shadowDistance = 0;
    }

    /**
     * OnPreRender is called at rendering
     * @authors tom_samaille
     */
    void OnPostRender() {
        QualitySettings.shadowDistance = m_StoredShadowDistance;
    }
}
