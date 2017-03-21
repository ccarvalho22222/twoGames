using UnityEngine;
using System.Collections;

public class PlayerDeathScript : MonoBehaviour {

    public int hp;
    public Vector3 checkpoint;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        hp = GetComponent<PlayerMovement>().hp;

        if(hp <= 0)
        {
            GetComponent<PlayerMovement>().hp = GetComponent<PlayerMovement>().maxHP;

            transform.position = checkpoint;

            GetComponent<PlayerCameraScript>().playerCamera.transform.position = 
                new Vector3(checkpoint.x, checkpoint.y, checkpoint.z - 10);

            GetComponent<PlayerCameraScript>().playerLight.transform.position = checkpoint;

        }
	}
}
