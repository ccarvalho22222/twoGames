using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public float speed;

    private Rigidbody rb;
    public Vector3 movement;

	void Start() {
        rb = GetComponent<Rigidbody>();

        //add cylinder scroll speed to enemies.
        speed += GameObject.Find("Level Controller").GetComponent<LevelController>().scrollSpeed;
    }

	void Update () {

        movement = new Vector3(0.0f, 0.0f, speed);
        rb.velocity = movement;
	}
}
