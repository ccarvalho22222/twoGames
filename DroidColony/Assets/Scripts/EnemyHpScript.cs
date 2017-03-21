using UnityEngine;
using System.Collections;

public class EnemyHpScript : MonoBehaviour {

    public int hp;
    public bool invulnerable;
    public int invulnerableTime;
    private int invulnerableCounter;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        if(hp <= 0)
        {  
           Destroy(gameObject);
        }

        if(invulnerable)
        {
            if(invulnerableCounter++ > invulnerableTime)
            {
                invulnerable = false;
                invulnerableCounter = 0;
            }
        }
	
	}
}
