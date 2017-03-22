using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    //public string inGameOptions;
    //public string returnToPauseMenu;
    public string mainMenu;
    public string restart;

    public GameObject pauseMenuCanvas;
    public bool isPaused;
    //public GameObject inGameOptionsCanvas;
    //public bool isOptionsPressed;

    // Update is called once per frame
    void Update ()
    {
        if (isPaused == true)
        {
            pauseMenuCanvas.SetActive(true);
        } else {
            pauseMenuCanvas.SetActive(false);
        }

        //if (isOptionsPressed == true)
        //{
        //    inGameOptionsCanvas.SetActive(true);
        //    pauseMenuCanvas.SetActive(false);
        //} else {
        //    inGameOptionsCanvas.SetActive(false);
        //}

        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Debug.Log("Pause menu shown, the game is frozen");
            Time.timeScale = 0.0F;
            isPaused = true;
        } else if (Input.GetKeyDown(KeyCode.Escape) && isPaused) {
            Debug.Log("Pause menu removed, the game is resumed");
            Time.timeScale = 1.0F;
            isPaused = false;
        }
	}

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1.0F;
    }

    //public void InGameOptions()
    //{
    //    Debug.Log("The base pause menu should close and the options menu should open");
    //    isOptionsPressed = true;
    //}

    //public void ReturnToPauseMenu()
    //{
    //    Debug.Log("Return to Main Menu has been pressed");
    //    isOptionsPressed = false;
    //}

    public void Restart()
    {
        //Debug.Log(SceneManager.sceneCountInBuildSettings);

        //int scene = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(scene, LoadSceneMode.Single);
        SceneManager.LoadScene(restart);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1.0F;
    }
}