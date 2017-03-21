using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

    public Transform canvas;
    public Transform PauseMenu;
	
	// Update is called once per frame
	void Update ()
    {
        Pause();
	}

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (canvas.gameObject.activeInHierarchy == false)
            {
                canvas.gameObject.SetActive(true);
                Time.timeScale = 0;
                AudioListener.volume = 0;
            }
            else
            {
                canvas.gameObject.SetActive(false);
                Time.timeScale = 1;
                AudioListener.volume = 1;
            }
        }
    }

    public void Resume()
    {
        canvas.gameObject.SetActive(false);
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
