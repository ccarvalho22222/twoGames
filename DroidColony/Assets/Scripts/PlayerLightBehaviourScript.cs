using UnityEngine;
using System.Collections;

public class PlayerLightBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //move camera to where the player is
        var player = GameObject.Find("Player");
        Vector3 pos = player.transform.position;

        transform.position = new Vector3(pos.x, pos.y, transform.position.z);

	}
}
