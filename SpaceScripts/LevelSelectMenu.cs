using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSelectMenu : MonoBehaviour {

    public string levelOne;
    public string levelTwo;
    public string levelThree;
    public string optionsMenu;

	// Update is called once per frame
	void Update ()
    {
	
	}

    public void LevelOne()
    {
        SceneManager.LoadScene(levelOne);
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene(levelTwo);
    }

    public void LevelThree()
    {
        SceneManager.LoadScene(levelThree);
    }

    public void ReturnToOptions()
    {
        SceneManager.LoadScene(optionsMenu);
    }
}