using UnityEngine;
using System.Collections;

public class MineBossBulletBehaviour : MonoBehaviour {

    public Vector3 bulletVelocity;
    private Rigidbody2D rB;
    public int damage;

    public AudioClip hitPlayer;

    // Use this for initialization
    void Start () {
        rB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        rB.velocity = bulletVelocity;
	}

    //void OnTriggerEnter2D(Collider2D col)
    //{
        
    //    if (col.gameObject.CompareTag("Player"))
    //    {
    //        col.gameObject.GetComponent<PlayerMovement>().hp -= damage;
    //    }

    //    if (!col.CompareTag("Bullet") && !col.CompareTag("PlayerPeripheral") && !col.CompareTag("Enemy") && !col.CompareTag("Alerter"))
    //    {
    //        Destroy(gameObject);
    //    }
        
    //}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.CompareTag("Player") || col.collider.gameObject.CompareTag("Spike"))
        {
            if(!col.gameObject.GetComponent<PlayerMovement>().invulnerable)
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
