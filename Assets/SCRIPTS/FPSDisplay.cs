using UnityEngine;
using System.Collections;

/**
 * FPSDisplay functions
 * Regroups every function or properties to display FPS.
 * @author tom_samaille
 */
public class FPSDisplay : MonoBehaviour
{
	/** 
     * DeltaTime refers to current deltatime.
     */
	float m_DeltaTime = 0.0f;
 
	/**
     * Update is called once per frame.
     * @author tom_samaille
     */
	void Update()
	{
		m_DeltaTime += (Time.unscaledDeltaTime - m_DeltaTime) * 0.1f;
	}
 
	/**
     * OnGUI is called once frame draws.
     * @author tom_samaille
     */
	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;
 
		GUIStyle style = new GUIStyle();
 
		Rect rect = new Rect(w * 1 / 100, h * 2 / 100, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = new Color (.0f, .0f, .0f, 1.0f);
		float msec = m_DeltaTime * 1000.0f;
		float fps = 1.0f / m_DeltaTime;
		string text = string.Format("{0:0} fps", fps);
		GUI.Label(rect, text, style);
	}
}