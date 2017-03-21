using UnityEngine;
using System.Collections;

public class SpinPushAI : MonoBehaviour {
    Rigidbody2D rB;
    public float spinSpeed;
	// Use this for initialization
	void Start () {
        rB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        rB.angularVelocity = spinSpeed;
	}
}
