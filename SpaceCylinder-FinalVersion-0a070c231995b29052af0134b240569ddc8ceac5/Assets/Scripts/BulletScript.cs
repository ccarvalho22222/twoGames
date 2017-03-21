using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    GameObject player;

    public int index;

	// Use this for initialization
	void Start () {

        player = GameObject.Find("Player Ship");
        
	}
	
	// Update is called once per frame
	void Update () {

        var rB = GetComponent<Rigidbody>();

        rB.velocity = new Vector3(0.0f, 0.0f, 10.0f);

    }
}
