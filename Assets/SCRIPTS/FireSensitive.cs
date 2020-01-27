using UnityEngine;
using System.Collections;

/**
 * Caracteristic of an object which will burn nest to fire
 * Regroups every function or properties allowing to be sensitive to fire.
 * @author tom_samaille
 */
 public class FireSensitive : MonoBehaviour
{
    /** 
     * m_GreenColor is a Color that refers first color of heat range (green).
     */
    private Color m_GreenColor = new Color(0.3509174f, 0.8313726f, 0.07450978f, 1f); // each param of Color contructor has to be betwenn 0 & 1
    /** 
     * m_RedColor is a Color that refers last color of heat range (red).
     */
    private Color m_RedColor = new Color(1f, 0.04444444f, 0f, 1f);
    /** 
     * m_StartWetColor is a Color that refers to the actual color before wetting the element.
     */
    private Color m_StartWetColor;
    /** 
     * m_EndWetColor is a Color that refers to the the End color calculated from m_StartWetColor.
     */
    private Color m_EndWetColor;
    /** 
     * m_InFire is a float between 0 & 1 which represents fire value of element.
     */
    private float m_InFire;
    /** 
     * m_InFire is a float between 0 & 1 which represents wet value of element.
     */
    private float m_Wet;
    /** 
     * m_FireResistanceDuration is a float value (in second) which represents duration of firing.
     */
    private float m_FireResistanceDuration = 3f;
    /** 
     * m_Range describes values of the range of FireDetection
     */
    public float m_Range = 6f;
    /** 
     * m_FireDetectionRange is a sphere which will be used as range detector for any FireSensitive GameObject
     */
     private GameObject m_FireDetectionRange;
     /** 
     * m_BurnedCountDisplay refers to the BurnedCOuntDisplay GameObject
     */
    public GameObject m_BurnedCountDisplay;
    /** 
     * m_FireState is an enumeration describing state of fire
     * @author tom_samaille
     */
    public enum m_States
    {
        WAITING, /**< When the object waits to be put on fire. */
        FIRING, /**< When the object is becoming on fire */
        ON_FIRE, /**< When the object is on fire (maximum value (1) reached) */
        RESETING, /**< When the object is resetting */
        KILLED, /**< When the object is killed (by water for example) */
        GETTING_WET, /**< When the object is getting wet */
        WET, /**< When the object is wet */
    };
    /** 
     * m_States refers to the actual FireState of the object (WAITING, FIRING or ON_FIRE)
     */
    private m_States m_FireState;

    /**
     * RangeInit creates and inittialize the FireDetectionRange GameObject as child of FireSensitive GameObject 
     * @author tom_samaille
     */
    private void RangeInit()
    {
        switch (transform.tag)
        {
            case "Tree":
                m_FireDetectionRange = GameObject.CreatePrimitive(PrimitiveType.Cube);
                break;
            case "Sphere":
                m_FireDetectionRange = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                break;
            default:
                m_FireDetectionRange = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                break;
        }
        m_FireDetectionRange.transform.position = transform.position;
        m_FireDetectionRange.transform.localScale = new Vector3(transform.localScale.x + m_Range, transform.localScale.y + m_Range, transform.localScale.z + m_Range);
        m_FireDetectionRange.transform.localRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        m_FireDetectionRange.GetComponent<Renderer>().enabled = false;
        m_FireDetectionRange.GetComponent<Collider>().isTrigger = true;
        m_FireDetectionRange.transform.parent = transform;
        m_FireDetectionRange.transform.name = "FireDetectionRange";
        m_FireDetectionRange.AddComponent<FireRangeTrigger>();
    }

    /**
     * Start is called before the first frame update
     * @author tom_samaille
     */
    void Start()
    {
        RangeInit();
        m_Wet = 0f;
        m_InFire = (transform.name != "Player") ? 0f : 1f;
        m_FireState = (transform.name != "Player") ? m_States.WAITING : m_States.ON_FIRE;
        GetComponent<Renderer>().material.color = Color.Lerp(m_GreenColor, m_RedColor, m_InFire); // Set initial color 
        m_BurnedCountDisplay = GameObject.FindGameObjectsWithTag("BurnedCountDisplay")[0];
    }

    /**
     * Update is called once per frame
     * @author tom_samaille
     */
    void Update()
    {
        switch (m_FireState)
        {
            case m_States.FIRING:
                IncrementFire();
                break;
            case m_States.GETTING_WET:
                IncrementWet();
                break;
            default:
                break;
        }
            
    }

    /**
     * IncrementFire is the function to call to update the element values and color ++
     * @author tom_samaille
     */
    public void IncrementFire()
    {
        if (m_InFire < 1f)
        {
            // Check surroundings to burn everything which already is in the range
            if (m_InFire < .7f && m_InFire + Time.deltaTime / m_FireResistanceDuration > .7f)
            {
                Collider[] hitColliders;
                float semiRange = m_Range / 2;  // Divide by 2 to respect real range
                switch (transform.tag)
                {
                    case "Tree":
                        hitColliders = Physics.OverlapBox(new Vector3(transform.position.x, transform.position.y + transform.localScale.y / 2, transform.position.z), new Vector3(transform.localScale.x + semiRange, transform.localScale.y + semiRange, transform.localScale.z + semiRange));
                        break;
                    case "Sphere":
                        hitColliders = Physics.OverlapSphere(transform.position, semiRange);
                        break;
                    default:
                        hitColliders = Physics.OverlapSphere(transform.position, semiRange);
                        break;
                }
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    Collider currentElement = hitColliders[i];
                    // if (currentElement.transform.parent != null) {
                    //     print(currentElement.transform.name + " " + currentElement.transform.parent.name);
                    // } else {
                    //     print(currentElement.transform.name);
                    // }
                    if (currentElement.transform.name != "FireDetectionRange" && currentElement.GetComponent<FireSensitive>() != null && currentElement.GetComponent<FireSensitive>().GetFireState() == m_States.WAITING)
                    {
                        currentElement.GetComponent<FireSensitive>().StartFire(); 
                    }
                }
            }
            GetComponent<Renderer>().material.color = Color.Lerp(m_GreenColor, m_RedColor, m_InFire);
            m_InFire += Time.deltaTime / m_FireResistanceDuration;
        }
        else 
        {
            EndFire();
        }
    }

    /**
     * DecrementFire is the function to call to update the element values and color --
     * @author tom_samaille
     */
    public void DecrementFire()
    {
        // Debug.Log(m_InFire);
        if (m_InFire > 0f)
        {
            GetComponent<Renderer>().material.color = Color.Lerp(m_GreenColor, m_RedColor, m_InFire);
            m_InFire -= Time.deltaTime / m_FireResistanceDuration;
        } 
        else 
        {
            KillFire();
        }
    }

    /**
     * IncrementWet is the function to call to update the element wet value and updtate wetColor
     * @author tom_samaille
     */
    public void IncrementWet()
    {
        // Debug.Log(m_InFire);
        if (m_Wet < 1f)
        {
            GetComponent<Renderer>().material.color = Color.Lerp(m_StartWetColor, m_EndWetColor, m_Wet);
            m_Wet += Time.deltaTime / (m_FireResistanceDuration / 3);
        }
        else 
        {
            SetWet();
        }
    }

    /**
     * Setter of m_InFire value
     * @author tom_samaille
     */
    public void SetWetColors()
    {
        m_StartWetColor = GetComponent<Renderer>().material.color;
        m_EndWetColor = ChangeColorBrightness(m_StartWetColor, -.5f);
    }

    /**
     * Setter of m_InFire value
     * @param value a float value which is the value we want to set m_InFire attribute
     * @author tom_samaille
     */
    public void SetFire(float value)
    {
        m_InFire = value;
    }

    /**
     * Getter of m_InFire value
     * @return float value of m_InFire attribute
     * @author tom_samaille
     */
    public float GetInFire()
    {
        return m_InFire;
    }

    /**
     * Getter of m_FireResistanceDuration value
     * @return float value of m_FireResistanceDuration attribute
     * @author tom_samaille
     */
    public float GetFireResistanceDuration()
    {
        return m_FireResistanceDuration;
    }

    /**
     * StartFire sets fire state from WAITING to FIRING
     * @author tom_samaille
     */
    public void StartFire()
    {
        if (m_FireState == m_States.WAITING)
        {
            m_FireState = m_States.FIRING;
        }
    }

    /**
     * EndFire sets fire state from FIRING to ON_FIRE
     * @author tom_samaille
     */
    public void EndFire()
    {
        if (m_FireState == m_States.FIRING)
        {
            transform.tag = "Burned";
            m_BurnedCountDisplay.GetComponent<BurnedCountDisplay>().IncrementBurnedTree();
            m_FireState = m_States.ON_FIRE;
        }
    }

    /**
     * ResetFire sets fire state from ON_FIRE to RESETING
     * @author tom_samaille
     */
    public void ResetFire()
    {
        if (m_FireState == m_States.ON_FIRE)
        {
            m_FireState = m_States.RESETING;
        }
    }

    /**
     * StopFire sets fire state from RESETING to WAITING
     * @author tom_samaille
     */
    public void StopFire()
    {
        if (m_FireState == m_States.FIRING)
        {
            m_FireState = m_States.WAITING;
        }
    }

    /**
     * StopFire sets fire state from RESETING to WAITING
     * @author tom_samaille
     */
    public void KillFire()
    {
        if (m_FireState == m_States.RESETING)
        {
            m_FireState = m_States.KILLED;
        }
    }

    /**
     * SetWet sets fire state to GETTING_WET
     * @author tom_samaille
     */
    public void GettingWet()
    {
        if (m_FireState != m_States.GETTING_WET && m_FireState != m_States.WET)
        {
            SetWetColors();
            m_FireState = m_States.GETTING_WET;
        }
    }

    /**
     * SetWet sets fire state to WET
     * @author tom_samaille
     */
    public void SetWet()
    {
        if (m_FireState == m_States.GETTING_WET)
        {
            m_InFire = .0f;
            m_FireState = m_States.WET;
        }
    }

    /**
     * Return FireState of the object
     * @author tom_samaille
     */
    public m_States GetFireState()
    {
        return m_FireState;
    }

    /**
     * Return true if current object is firing, false otherwise
     * @author tom_samaille
     */
    public bool IsFiring()
    {
        return m_FireState == m_States.FIRING;
    }

    /**
     * Creates color with corrected brightness.
     * Inspired by pavel_vladov from : https://www.pvladov.com/2012/09/make-color-lighter-or-darker.html
     * @param the Color to correct.
     * @param correctionFactor a float referring to the brightness correction factor. Must be between -1 and 1. Negative values produce darker colors.
     * @return a Color
     * @author tom_samaille
     */
    private static Color ChangeColorBrightness(Color color, float correctionFactor)
    {
        float red = (float)color.r * 255;
        float green = (float)color.g * 255;
        float blue = (float)color.b * 255;

        if (correctionFactor < 0)
        {
            correctionFactor = 1 + correctionFactor;
            red *= correctionFactor;
            green *= correctionFactor;
            blue *= correctionFactor;
        }
        else
        {
            red = (255 - red) * correctionFactor + red;
            green = (255 - green) * correctionFactor + green;
            blue = (255 - blue) * correctionFactor + blue;
        }

        red /= 255;
        green /= 255;
        blue /= 255;

        return new Color(red, green, blue);
    }
}
