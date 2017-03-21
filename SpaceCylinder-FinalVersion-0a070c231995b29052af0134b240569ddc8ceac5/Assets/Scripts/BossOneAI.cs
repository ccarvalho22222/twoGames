using UnityEngine;
using System.Collections;

public class BossOneAI : MonoBehaviour {

    public bool done = false;


	public float shootDelay;
	public float shotPause;
	public int shotBurst;
	public float shootSpeed;
	public float burstSpeed;

	public float bossSpeed;
	public float overShoot = 10.0f;

	public GameObject shot;
	private bool firePause = true;
	private int bulletCount;

	private Transform boss;
	private Quaternion tracking;

    public int health;
    private int startingHealth;

    public GameObject explosion;
    public GameObject enemies;

    public bool upgrade = false;
    public bool upgraded = false;

    void Start () {

        startingHealth = health;

        enemies = GameObject.Find("Enemies");

        boss = GetComponent<Transform> ();
		tracking = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        GetComponent<GenericRotation>().baseSpeed = bossSpeed;

    }

	void Update () {

        if((health < (startingHealth / 2f)) && !upgraded)
        {
            upgrade = true;
            upgraded = true;
        }
        if(upgrade)
        {
            shootDelay /= 2f;
            shotPause /= 2f;
            burstSpeed /= 1.5f;
            bossSpeed *= 3f;
            shotBurst *= 3;
            upgrade = false;
        }

        if(health <= 0)
        {
            
            Vector3 position = new Vector3(enemies.transform.position.x, enemies.transform.position.y, transform.position.z);
            Instantiate(explosion, position, enemies.transform.rotation, enemies.transform);
            done = true;

            GetComponentInChildren<MeshRenderer>().enabled = false;
            enabled = false;
        }


		//if(Quaternion.Angle(boss.rotation, tracking) == 0.0f) {
        if(overShoot > 0)
        {
            if ((transform.rotation.z * 180) > overShoot)
            {
                overShoot *= -1.0f;
                GetComponent<GenericRotation>().speed *= -1.0f;
                //tracking = Quaternion.Euler (0.0f, 0.0f, overShoot);//overShoot * -1.0f);
            }
        }
        else
        {
            if ((transform.rotation.z * 180) < overShoot)
            {
                overShoot *= -1.0f;
                GetComponent<GenericRotation>().speed *= -1.0f;
                //tracking = Quaternion.Euler (0.0f, 0.0f, overShoot);//overShoot * -1.0f);
            }
        }
        

		//boss.rotation = Quaternion.RotateTowards (boss.rotation, tracking, bossSpeed);
        
		//if (Quaternion.Angle(boss.rotation, tracking) < 30.0f) {
			
			if (firePause) {
				if (Time.time > shotPause) {
					shotPause = Time.time + shootDelay;
					firePause = false;
					bulletCount = 0;
				}
			}

			if (!firePause) {
				if (bulletCount < shotBurst) {
					if (Time.time > shotPause) {
						bulletCount++;
						GameObject tempShot = (GameObject)Instantiate (shot, boss.position, boss.rotation);
						tempShot.GetComponent<Projectile> ().speed = shootSpeed * -1.0f;
						shotPause = Time.time + burstSpeed;
					}
				} else {
					firePause = true;
					shotPause = Time.time + burstSpeed;
				}
			}
		//}
	}

    

}
