using UnityEngine;
using System.Collections;

public class GenericRotation : MonoBehaviour {

    public float baseSpeed;
    public float speed;
	// Use this for initialization
	void Start () {
        speed = baseSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(
                0.0f,
                0.0f,
                speed
            );
	}
}
