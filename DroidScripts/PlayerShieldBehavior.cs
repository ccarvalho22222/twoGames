using UnityEngine;
using System.Collections;

public class PlayerShieldBehavior : MonoBehaviour {


    public bool destroy = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (destroy)
        {
            if (col.collider.gameObject.CompareTag("Enemy Peripheral"))
            {
                GameObject.Destroy(col.collider.gameObject);
                col.collider.gameObject.GetComponentInParent<MediumEnemyAI>().spikeDestroyed = true;
            }
        }
    }

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (terminal)
    //    {
    //        if (col.gameObject.CompareTag("Enemy Peripheral"))
    //        {
    //            GameObject.Destroy(col.gameObject);
    //        }
    //    }
    //}
}
