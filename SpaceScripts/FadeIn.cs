using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Texture2D fadeTexture;
    float fadeSpeed = 0.15f;
    float drawDepth = -1000;

    private float alpha = 1.0f;
    private float fadeDir = -1f;

    int counter = 0;
    int timer = 60;

    void OnGUI()
    {
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);

        GUI.depth = (int)drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
        if (counter++ > timer)
        {
            alpha += fadeDir * fadeSpeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);

            

            
        }
        if(counter > 500)
        {
            enabled = false;
        }
    }
}
