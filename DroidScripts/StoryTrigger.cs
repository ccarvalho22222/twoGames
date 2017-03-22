using UnityEngine;
using System.Collections;

public class StoryTrigger : MonoBehaviour {

    public GameObject story;
    bool confirmed = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(PlayerMovement.storyend == 1 && confirmed == true)
        {
            story.SetActive(false);
            Time.timeScale = 1;
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DoorActivator"))
        {
            confirmed = true;
            Time.timeScale = 0.0001f;
            story.SetActive(true);
        }
    }

}
