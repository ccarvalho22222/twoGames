using UnityEngine;
using System.Collections;

public class EnemySpikeBehavior : MonoBehaviour {

    public int damage;

    public AudioClip hitPlayer;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.CompareTag("Player"))
        {
            if (!col.gameObject.GetComponent<PlayerMovement>().invulnerable && damage != 0)
            {
                col.gameObject.GetComponent<PlayerMovement>().hp -= damage;
                col.gameObject.GetComponent<PlayerMovement>().invulnerable = true;
                AudioSource.PlayClipAtPoint(hitPlayer, transform.position);
            }
        }
    }
}
