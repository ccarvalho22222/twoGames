using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class OptionsMenu : MonoBehaviour {

    public string levelSelect;
    public string mainMenu;

	// Update is called once per frame
	void Update ()
    {
	
	}

    public void LevelSelect()
    {
        SceneManager.LoadScene(levelSelect);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}