using UnityEngine;
using System.Collections;

public class SecurityBotBulletBehavior : MonoBehaviour {

    public Vector3 bulletVelocity;
    Rigidbody2D rB;
    int damage = 1;

    public AudioClip hitPlayer;

	// Use this for initialization
	void Start () {
        rB = GetComponent<Rigidbody2D>();
        rB.velocity = bulletVelocity;
    }
	
	// Update is called once per frame
	void Update () {
	    
        
	}

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.CompareTag("Player"))
    //    {
    //        col.gameObject.GetComponent<PlayerMovement>().hp--;
    //    }

    //    if (!col.CompareTag("Bullet") && !col.CompareTag("PlayerPeripheral") && !col.CompareTag("Alerter"))
    //    {
    //        Destroy(gameObject);
    //    }        
    //}
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.CompareTag("Player") || col.collider.gameObject.CompareTag("Spike"))
        {
            if (!col.gameObject.GetComponent<PlayerMovement>().invulnerable)
            {
                col.gameObject.GetComponent<PlayerMovement>().hp -= damage;
                col.gameObject.GetComponent<PlayerMovement>().invulnerable = true;
                AudioSource.PlayClipAtPoint(hitPlayer, transform.position);
            }
        }
        if (!col.collider.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
