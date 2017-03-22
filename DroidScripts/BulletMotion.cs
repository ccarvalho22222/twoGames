using UnityEngine;
using System.Collections;

public class BulletMotion : MonoBehaviour {

    public float speed;         //velocity magnitude
    public int damage;

    public Vector3 velocity;    //normalized velocity

    public Rigidbody2D rB;

	// Use this for initialization
	void Start () {
        rB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {


        rB.velocity = velocity;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<EnemyHpScript>().hp -= damage;
            //col.gameObject.GetComponent<EnemyHpScript>().invulnerable = true;
        }

        if (!col.CompareTag("Bullet") && !col.CompareTag("Alerter"))
        {
            Destroy(gameObject);
        }

        
    }
}
