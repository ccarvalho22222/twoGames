using UnityEngine;
using System.Collections;

public class AlerterScript : MonoBehaviour {

    public bool active;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
        
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (active)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                col.gameObject.GetComponent<EnemyEnabledScript>().engaged = true;
            }
        }
        else if(transform.parent.gameObject.CompareTag("Enemy"))
        {
            if(col.gameObject.CompareTag("Player"))
            {
                active = true;
                GetComponentInParent<EnemyEnabledScript>().engaged = true;
            }
        }
    }
}
