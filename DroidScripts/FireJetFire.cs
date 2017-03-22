using UnityEngine;
using System.Collections;

public class FireJetFire : MonoBehaviour {

    public AudioClip hitPlayer;
    public int damage = 1;

    public int damageWait;
    private int damageWaitCounter;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (!col.gameObject.GetComponent<PlayerMovement>().invulnerable)
            {
                if (damageWaitCounter++ > damageWait)
                {
                    col.gameObject.GetComponent<PlayerMovement>().hp -= damage;
                    col.gameObject.GetComponent<PlayerMovement>().invulnerable = true;
                    AudioSource.PlayClipAtPoint(hitPlayer, col.gameObject.transform.position);
                    damageWaitCounter = 0;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            damageWaitCounter = 0;
            if (!col.gameObject.GetComponent<PlayerMovement>().invulnerable)
            {
                col.gameObject.GetComponent<PlayerMovement>().hp -= damage;
                col.gameObject.GetComponent<PlayerMovement>().invulnerable = true;
                AudioSource.PlayClipAtPoint(hitPlayer, col.gameObject.transform.position);
                damageWaitCounter = 0;
            }
        }
    }
}
