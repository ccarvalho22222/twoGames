using UnityEngine;
using System.Collections;

public class BossMovement : MonoBehaviour {

	public float speed;

    private Rigidbody rb;
    public Vector3 movement;

	void Start() {
        rb = GetComponent<Rigidbody>();

        //add cylinder scroll speed to enemies.
        //speed += GameObject.Find("Level Controller").GetComponent<LevelController>().scrollSpeed;
    }

	void Update () {

        movement = new Vector3(0.0f, 0.0f, speed);
        
        rb.velocity = movement;

        transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y,
                    Mathf.Clamp(transform.position.z, -3f, 3f));
	}
}
