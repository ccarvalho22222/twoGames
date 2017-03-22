using UnityEngine;
using System.Collections;

public class BulletBehaviourScript : MonoBehaviour {

    public int index;
	// Use this for initialization
	void Start () {

        //var player = GameObject.Find("SideShooterPlayer");

        //transform.position = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        var rB = GetComponent<Rigidbody2D>();

        rB.velocity = new Vector3(10.0f, 0.0f, 0.0f);

        var farWall = GameObject.Find("Far Wall");
        if(rB.IsTouching(farWall.GetComponent<Collider2D>()))
        {
           
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //print("collide" + index);
        var farWall = GameObject.Find("Far Wall");


        //can customize what happens on a collision based on what object the bullet collides with.

        //Far Wall collision
        if (coll.collider == farWall.GetComponent<Collider2D>())
        {
            var player = GameObject.Find("Player");
            var script = player.GetComponent<SideScrollingShooterPlayer>();

            script.destroy = true;
            script.destroyIndex = index;
        }

        
    }
}
