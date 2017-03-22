using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

    
	private float shootDelay;
	private Transform self;
	public float shootSpeed;

	public GameObject shot;
    public float spreadAngle;
    public float bulletWaveAmp;
    public float bulletWaveSpeed;

    public int numShots;

//	private GameObject shotSpawn;
	// Use this for initialization
	void Start () {
		self = GetComponent<Transform> ();
//		shotSpawn = GameObject.Find("Bullet Cylinder");
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > shootDelay) {
			shootDelay = Time.time + shootSpeed;

            //creates multiple shots and gives them rotations depending on spreadAngle
            for (int i = 0; i < numShots; i++)
            {
                GameObject tempShot = (GameObject)Instantiate(shot, self.position, self.rotation);
                
                //change rotation speed of individual bullets
                var tempShotScript = tempShot.GetComponent<GenericRotation>();
                tempShotScript.baseSpeed = (-spreadAngle * (int)(numShots / 2) + (spreadAngle * i));

                //change wave style of individual bullets
                var tempShotScript2 = tempShot.GetComponent<WaveRotation>();
                tempShotScript2.waveAmplitude = bulletWaveAmp;
                tempShotScript2.waveSpeed = bulletWaveSpeed;
            }
		}
	}
}
