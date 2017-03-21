using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SideScrollingShooterPlayer : MonoBehaviour {

    public bool destroy = false;
    public int destroyIndex = -1;
    float speed = 5.0f;
    public float gravity = 10.0f;

    GameObject cylinder;
    Rigidbody rB;
    Camera camera;

    public float camDistance;
    public float startingCamDistance;

    public float playerDistance;
    public float startingPlayerDistance;

	// Use this for initialization
	void Start () {
        cylinder = GameObject.Find("Cylinder");
        float y = cylinder.transform.position.y;
        float z = cylinder.transform.position.z;
        
        //start position at surface of cylinder
        transform.position = new Vector3(-12.0f, y, z -  ((cylinder.transform.localScale.z / 2f) + (transform.localScale.z)));

        //start camera near player
        float yDifference = transform.position.y - cylinder.transform.position.y;
        float zDifference = transform.position.z - cylinder.transform.position.z;
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        camera.transform.position = new Vector3(transform.position.x - 7f, yDifference * 1.2f, zDifference * 1.2f);

        //check camera's starting distance to make sure it doesnt travel away from the cylinder
        float yCamDifference = camera.transform.position.y - cylinder.transform.position.y;
        float zCamDifference = camera.transform.position.z - cylinder.transform.position.z;
        startingCamDistance = Mathf.Sqrt((yCamDifference * yCamDifference) + (zCamDifference * zCamDifference));

        //check player's starting distance to make sure it doesnt travel away from the cylinder
        startingPlayerDistance = Mathf.Sqrt((yDifference * yDifference) + (zDifference * zDifference));
    }


    GameObject bulletInstance;
    public List<GameObject> bullets = new List<GameObject>();

    // Update is called once per frame
    void Update () {

        rB = GetComponent<Rigidbody>();

        ////rotate so that it is perpendicular to the cylinder
        //get handle for cylinder
        cylinder = GameObject.Find("Cylinder");

        float yDifference = transform.position.y - cylinder.transform.position.y;
        float zDifference = transform.position.z - cylinder.transform.position.z;
        playerDistance = Mathf.Sqrt((yDifference * yDifference) + (zDifference * zDifference));
        Vector3 relativePos = new Vector3(0.0f, yDifference, zDifference);
        transform.rotation = Quaternion.LookRotation(relativePos);

        //gravitate toward cylinder
        Vector3 gravityForce = new Vector3(0.0f, -yDifference * gravity, -zDifference * gravity);
        //rB.AddForce(gravityForce);

        //set speed to the distance from the center of the cylinder
        speed = Mathf.Sqrt((Mathf.Pow(yDifference, 2) + Mathf.Pow(zDifference, 2)));

        //calculate angular velocities
        float theta = Mathf.Atan2(yDifference, zDifference);
        float yVelocity = speed * Mathf.Cos(theta);
        float zVelocity = speed * Mathf.Sin(theta);





        ////get handle for camera's RigidBody
        var cameraRB = camera.GetComponent<Rigidbody>();



        




        float yCamDifference = camera.transform.position.y - cylinder.transform.position.y;
        float zCamDifference = camera.transform.position.z - cylinder.transform.position.z;

        camDistance = Mathf.Sqrt((yCamDifference * yCamDifference) + (zCamDifference * zCamDifference));

        float camSpeed = Mathf.Sqrt((Mathf.Pow(yCamDifference, 2) + Mathf.Pow(zCamDifference, 2)));
        float camTheta = Mathf.Atan2(yCamDifference, zCamDifference);

        float camYVelocity = camSpeed * Mathf.Cos(camTheta);
        float camZVelocity = camSpeed * Mathf.Sin(camTheta);
        
        //rotate camera to look at player
        camera.transform.LookAt(transform, new Vector3(0.0f, Mathf.Sin(camTheta - (Mathf.PI/2)), Mathf.Cos(camTheta - (Mathf.PI / 2))));


        //Check for keyboard input
        if (Input.GetKey("s"))
        {
            rB.velocity = (new Vector3(rB.velocity.x, yVelocity, -zVelocity));
            cameraRB.velocity = new Vector3(rB.velocity.x, camYVelocity, -camZVelocity);
        }
            
        else if (Input.GetKey("w"))
        {
            rB.velocity = (new Vector3(rB.velocity.x, -yVelocity, zVelocity));
            cameraRB.velocity = new Vector3(rB.velocity.x, -camYVelocity, camZVelocity);
        }
        else
        {
            rB.velocity = (new Vector3(rB.velocity.x, 0.0f, 0.0f));
            cameraRB.velocity = new Vector3(rB.velocity.x, 0.0f, 0.0f);
        }
            



        if (Input.GetKey("d"))
            rB.velocity = (new Vector3(speed, rB.velocity.y, rB.velocity.z));
        else if (Input.GetKey("a"))
            rB.velocity = (new Vector3(-speed, rB.velocity.y, rB.velocity.z));
        else
            rB.velocity = (new Vector3(0.0f, rB.velocity.y, rB.velocity.z));

        //if Camera exceed's original distance, bring it back
        //(Not sure why, but it slowly travels away from the cylinder.
        // Probably has to do with a small rounding error somewhere)
        if (camDistance > startingCamDistance)
        {
            cameraRB.AddForce(10.0f * (new Vector3(0.0f, -Mathf.Sin(camTheta), -Mathf.Cos(camTheta))));
        }
        //if Player exceed's original distance, bring it back
        if (playerDistance > startingPlayerDistance)
        {
            rB.AddForce(10.0f * (new Vector3(0.0f, -Mathf.Sin(theta), -Mathf.Cos(theta))));
        }

        //var upBound = camera.ViewportToWorldPoint(new Vector3(0.0f, 0.7f, camera.nearClipPlane));
        //var downBound = camera.ViewportToWorldPoint(new Vector3(0.0f, 0.3f, camera.nearClipPlane));

        //create bullets and add them to a list
        var bullet = GameObject.Find("Bullet");
        if (Input.GetKeyDown("space"))
        {
            bulletInstance = Instantiate(bullet);
            bulletInstance.GetComponent<BulletBehaviourScript>().index = (bullets.Count);
            bullets.Add(bulletInstance);
            bulletInstance.transform.position = new Vector3(transform.position.x + 1.0f, transform.position.y, 0.0f);
        }

        //destroy bullet if a destroy message is sent from it's collision
        if(destroy)
        {
            //print("Destroy" + destroyIndex);
            Destroy(bullets[destroyIndex]);
            //bullets.RemoveAt(destroyIndex);
            destroy = false;
            destroyIndex = -1;
        }
    }
}
