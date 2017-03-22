using UnityEngine;
using System.Collections;

public class HomingRotation : MonoBehaviour {

    GameObject player; 

    private float rotateVelocity;   //artificial angular velocity
    public float rotateAccel = 0.01f;
    
	// Use this for initialization
	void Start () {
	    player = GameObject.Find("Player Ship");
    }
	
	// Update is called once per frame
	void Update () {
        var script = this.GetComponent<GenericRotation>();

        if(transform.rotation.z > 0)
        {
            rotateVelocity -= rotateAccel * (transform.rotation.z / 5);
        }
        else
        {
            rotateVelocity += rotateAccel;
        }

        script.speed = rotateVelocity;
	}
}
