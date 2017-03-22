using UnityEngine;
using System.Collections;

public class StationaryGunnerAI : MonoBehaviour {

    public int level;
    public int engageDistance;

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player Ship");
	}
	
	// Update is called once per frame
	void Update () {
	    
        if(Mathf.Abs(player.transform.position.z - transform.position.z) < engageDistance)
        {
            if(level == 1)  //1 straight shot, slow shots
            {
                GetComponent<Shooter>().numShots = 1;
                GetComponent<Shooter>().shootSpeed = 2f;
            }
            if(level == 2)  //3 shots spread out, slow shots
            {
                GetComponent<Shooter>().numShots = 3;
                GetComponent<Shooter>().spreadAngle = 0.1f;
                GetComponent<Shooter>().shootSpeed = 2f;
            }
            if(level == 3)  //3 shots with wave
            {
                GetComponent<Shooter>().numShots = 3;
                GetComponent<Shooter>().spreadAngle = 0.1f;
                GetComponent<Shooter>().bulletWaveAmp = 0.5f;
                GetComponent<Shooter>().bulletWaveSpeed = 0.5f;
                GetComponent<Shooter>().shootSpeed = 1.5f;
            }
            if(level == 4)  //5 shots no wave, fast
            {
                GetComponent<Shooter>().numShots = 5;
                GetComponent<Shooter>().spreadAngle = 0.2f;
                GetComponent<Shooter>().shootSpeed = 1f;
            }
            if(level == 5)  //5 shots with wave, fast
            {
                GetComponent<Shooter>().numShots = 5;
                GetComponent<Shooter>().spreadAngle = 0.15f;
                GetComponent<Shooter>().bulletWaveAmp = 0.35f;
                GetComponent<Shooter>().bulletWaveSpeed = 0.5f;
                GetComponent<Shooter>().shootSpeed = 1f;
            }
        }
	}
}
