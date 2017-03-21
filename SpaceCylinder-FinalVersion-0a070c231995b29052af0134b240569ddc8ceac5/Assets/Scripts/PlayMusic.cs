using UnityEngine;
using System.Collections;

public class PlayMusic : MonoBehaviour {

    public AudioClip song;
    public GameObject camera;

	// Use this for initialization
	void Start () {
        camera = GameObject.Find("Main Camera");
        AudioSource.PlayClipAtPoint(song, camera.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
