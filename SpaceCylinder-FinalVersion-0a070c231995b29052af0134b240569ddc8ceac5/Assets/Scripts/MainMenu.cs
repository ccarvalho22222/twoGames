using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public string startLevel;
    public string options;

	public void NewGame()
    {
        SceneManager.LoadScene(startLevel);
	}
	
	public void Options()
    {
        SceneManager.LoadScene(options);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}