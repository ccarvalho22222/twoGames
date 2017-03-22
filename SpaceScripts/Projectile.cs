using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	private GameObject shotSpawn;
	private Transform temp;

    public float speed;
    public Vector3 movement;

    public int damage;

    private Rigidbody rb;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();

        shotSpawn = GameObject.Find("Bullet Cylinder");
		temp = this.GetComponent<Transform> ();
		temp.transform.parent = shotSpawn.transform;

        //add cylinder scroll speed to projectiles.
        speed += GameObject.Find("Level Controller").GetComponent<LevelController>().scrollSpeed;
		movement = new Vector3(0.0f, 0.0f, speed);
		rb.velocity = movement;
	}
	
}
