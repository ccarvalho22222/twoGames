using UnityEngine;
using System.Collections;

public class WallerBotBulletBehavior : MonoBehaviour {

    int lifetime = 210;
    int counter = 0;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
        if(counter++ > lifetime)
        {
            Destroy(gameObject);
        }
	}
}
