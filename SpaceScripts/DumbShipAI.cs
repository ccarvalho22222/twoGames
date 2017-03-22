using UnityEngine;
using System.Collections;

public class DumbShipAI : MonoBehaviour {

    public int level;
	// Use this for initialization
	void Start () {
        if (level == 1)  //just move straight
        {
            GetComponentInChildren<EnemyShipCollisionHandler>().health = 2;
        }
        if (level == 2)  //slight slow wave pattern and slower movement and more health
        {
            GetComponent<WaveRotation>().waveAmplitude = 0.1f;
            GetComponent<WaveRotation>().waveSpeed = 0.5f;
            GetComponent<EnemyMovement>().speed += 0.5f;
            GetComponentInChildren<EnemyShipCollisionHandler>().health = 4;

        }
        if (level == 3)  //larger wave pattern more health
        {
            GetComponent<WaveRotation>().waveAmplitude = 0.5f;
            GetComponent<WaveRotation>().waveSpeed = 0.5f;
            GetComponentInChildren<EnemyShipCollisionHandler>().health = 6;
            GetComponent<EnemyMovement>().speed += 1.0f;
        }
        if (level == 4)  //faster wave pattern and faster speed more health
        {
            GetComponent<WaveRotation>().waveAmplitude = 0.25f;
            GetComponent<WaveRotation>().waveSpeed = 1.0f;
            GetComponent<EnemyMovement>().speed += -1f;
            GetComponentInChildren<EnemyShipCollisionHandler>().health = 3;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
        
	}
}
