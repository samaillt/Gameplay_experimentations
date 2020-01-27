using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
*   DESCRIPTION
*   A script to dynamically switch between music using their time samples location
*   @Simon_Descoteaux
*/
public class DynamicMusic : MonoBehaviour
{

    //public AudioSource[] m_AudioSources;
    public AudioClip[] m_AudioStates;

    //public float m_FadeSpeed;
    public Text m_InfoText;

    /**
    *   Allow the interruption of fading for one state to the other
    */
    private bool m_IsFadingIn;
    private bool m_IsFadingOut;
    private bool m_IsPlaying0;
    private bool m_IsPlaying1;
    private AudioSourceCrossfade m_AudioSourceCrossfade;

    void Start()
    {
        m_AudioSourceCrossfade = GetComponent<AudioSourceCrossfade>();
    }

    public void PlayMusic(int index)
    {
        // if (m_IsPlaying0 && !m_IsPlaying1)
        // {
        //     m_AudioSources[1].timeSamples = m_AudioSources[0].timeSamples;
        //     m_AudioSources[0].Stop();

        //     m_AudioSources[1].clip = m_AudioStates[index];
        //     m_AudioSources[1].Play();

        //     // StartCoroutine(FadeIn(1, 1));
        //     // StartCoroutine(FadeOut(0, 0));            

        //     m_IsPlaying0 = false;
        //     m_IsPlaying1 = true;
        // }
        // if (m_IsPlaying1 && !m_IsPlaying0)
        // {
        //     m_AudioSources[0].timeSamples = m_AudioSources[1].timeSamples;
        //     m_AudioSources[1].Stop();
           

        //     m_AudioSources[0].clip = m_AudioStates[index];
        //     m_AudioSources[0].Play();

        //     // StartCoroutine(FadeOut(1, 0)); 
        //     // StartCoroutine(FadeIn(0, 1));

        //     m_IsPlaying0 = true;  
        //     m_IsPlaying1 = false;

        // } else
        // {
        //     m_AudioSources[0].clip = m_AudioStates[index];
        //     m_AudioSources[0].Play();

        //     // StartCoroutine(FadeIn(0, 1));
            
        //     m_IsPlaying0 = true;
        //     m_IsPlaying1 = false;              
        // }

        m_AudioSourceCrossfade.Play(m_AudioStates[index]);
    }

    // IEnumerator FadeIn(int audioSourceIndex, float maxVolume)
    // {
    //     m_IsFadingIn = true;
    //     m_IsFadingOut = false;

    //     m_AudioSources[audioSourceIndex].volume = 0;
    //     float audioVolume = m_AudioSources[audioSourceIndex].volume;

    //     while (m_AudioSources[audioSourceIndex].volume < maxVolume && m_IsFadingIn) 
    //     {
    //        audioVolume += m_FadeSpeed;
    //        m_AudioSources[audioSourceIndex].volume = audioVolume;
    //        yield return new WaitForSeconds(0.1f);
    //     }
    // }

    // IEnumerator FadeOut(int audioSourceIndex, float minVolume)
    // {
    //     m_IsFadingIn = false;
    //     m_IsFadingOut = true;

    //     float audioVolume = m_AudioSources[audioSourceIndex].volume;

    //     while (m_AudioSources[audioSourceIndex].volume > minVolume && m_IsFadingOut)
    //     {
    //        audioVolume -= m_FadeSpeed;
    //        m_AudioSources[audioSourceIndex].volume = audioVolume;
    //        yield return new WaitForSeconds(0.1f);
    //     }
    // }
}