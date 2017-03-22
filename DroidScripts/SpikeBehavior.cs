using UnityEngine;
using System.Collections;

public class SpikeBehavior : MonoBehaviour {

    public int damage;

    public AudioClip playerHitEnemyWithDash;
    public AudioClip playerHitNonEnemyWithDash;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.CompareTag("Enemy"))
        {
            col.collider.gameObject.GetComponent<EnemyHpScript>().hp -= damage;
            col.gameObject.GetComponent<EnemyHpScript>().invulnerable = true;

            if (damage != 0)
            {
                AudioSource.PlayClipAtPoint(playerHitEnemyWithDash, transform.position);
            }           
        }
        else
        {
            if (damage != 0)
            {
                AudioSource.PlayClipAtPoint(playerHitNonEnemyWithDash, transform.position);
            }
        }
    }

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.CompareTag("Enemy"))
    //    {
    //        col.gameObject.GetComponent<EnemyBehaviourScript>().hp--;
    //    }
    //}
}
