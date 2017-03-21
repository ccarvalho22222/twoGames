using UnityEngine;
using System.Collections;

public class SSEnemy1BehaviourScript : MonoBehaviour {

    public int health = 3;
    public float speed;
    public float gravity = -1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //Default Enemy Behavior
        var rB = GetComponent<Rigidbody>();

        //gravitate toward cylinder
        //force should be based on y, z position compared to cylinder

        //get handle for cylinder
        var cylinder = GameObject.Find("Cylinder");

        float yDifference = transform.position.y - cylinder.transform.position.y;
        float zDifference = transform.position.z - cylinder.transform.position.z;

        ////rotate so that it is perpendicular to the cylinder
        Vector3 relativePos = new Vector3(0.0f, yDifference, zDifference);
        transform.rotation = Quaternion.LookRotation(relativePos);

        Vector3 gravityForce = new Vector3(0.0f, yDifference * gravity, zDifference * gravity);

        rB.AddForce(gravityForce);

        

	}
}
