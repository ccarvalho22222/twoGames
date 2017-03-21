using UnityEngine;
using System.Collections;

public class TimedDeath : MonoBehaviour {

    private int time;
    public int lifeTime;
    private int counter = 0;
	// Use this for initialization
	void Start () {
        time = lifeTime * 60; //convert from real time to frames time
	}
	
	// Update is called once per frame
	void Update () {

        counter++;
        if( counter >= time )
        {
            Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
        
	}
}
