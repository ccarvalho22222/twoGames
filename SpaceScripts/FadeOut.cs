using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour {

    public bool fade = false;
    public int currentLevel;

	// Use this for initialization
	void Start () {
        currentLevel = GameObject.Find("Level Controller").GetComponent<LevelController>().currentLevel;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Texture2D fadeTexture;
    public Texture2D endGame;
    float fadeSpeed = 0.2f;
    float drawDepth = -1000;

    private float alpha = 0.0f;
    private float fadeDir = 1f;

    void OnGUI()
    {
        if(currentLevel == 3)
        {
            fadeTexture = endGame;
        }
        if (fade)
        {
            //fade down music
            GetComponent<AudioSource>().volume -= 0.001f;

            alpha += fadeDir * fadeSpeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);

            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);

            GUI.depth = (int)drawDepth;

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
            if(alpha == 1.0)
            {
                
                //switch to next level
                if(currentLevel == 1)
                {
                    Application.LoadLevel("Level2");
                }
                if(currentLevel == 2)
                {
                    Application.LoadLevel("Level3");
                }
                if (currentLevel == 3)
                {
                    Application.LoadLevel("MainMenu");
                }
            }
        }
    }
}
