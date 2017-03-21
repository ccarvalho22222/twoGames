using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	public float speed;
	public float tilt;

	private Rigidbody ship;

    public GameObject explosion;

//    public bool rotatingUp;
//    public bool rotatingDown;

    public int health;
    public bool invicible = false;
    public bool shieldOn = false;
    public int invicibleTime;
    public int invicibleCounter;
    public GameObject shipShield;
    public AudioClip shieldActive;

    

    //input variables
    float moveVertical;
    bool fire;

    // Use this for initialization
    void Start () {
		ship = GetComponent<Rigidbody> ();
        invicibleCounter = 0;
	}

    //need to put input in here or it isn't always recognized
    void Update()
    {
        if(health <= 0)
        {
            //GAME OVER
            Instantiate(explosion, transform.position, transform.rotation);
            health = 5;
            GameObject.Find("Level Controller").GetComponent<LevelController>().points -= 1000;
	    
        }

        if(invicible)
        {
            if (shieldOn == false)
            {
                shieldOn = true;
                AudioSource.PlayClipAtPoint(shieldActive, transform.position);
            }
            if (invicibleCounter < invicibleTime)
            {
                shipShield.SetActive(true);
                invicibleCounter++;
            }
            else
            {
                shieldOn = false;
                shipShield.SetActive(false);
                invicibleCounter = 0;
                invicible = false;
            }
        }

        // Lock ship in play area, and tilt when moving
        moveVertical = Input.GetAxis("Vertical");
        ship.position = new Vector3
            (
                transform.position.x,
                Mathf.Clamp(ship.position.y, -0.5f, 0.5f),
                Mathf.Clamp(ship.position.z, -3f, 3.0f)
            );
        ship.rotation = Quaternion.Euler(0.0f, 0.0f, tilt * moveVertical);
    }

    void OnTriggerEnter(Collider col)
    {
        if(!invicible)
        {
            if (col.CompareTag("Enemy Bullet"))
            {
                health--;
                invicible = true;
                Destroy(col.gameObject);

                //switch back to default weapon
                GetComponent<PlayerShooter>().gunType = 0;
            }
            if (col.CompareTag("Enemy"))
            {
                health--;
                invicible = true;

                //switch back to default weapon
                GetComponent<PlayerShooter>().gunType = 0;
            }
        }

        if (col.CompareTag("powerup1"))
        {
            health++;

            GetComponent<PlayerShooter>().gunType = 1;

            Destroy(col.gameObject);
        }
        if (col.CompareTag("powerup2"))
        {
            health++;

            GetComponent<PlayerShooter>().gunType = 2;

            Destroy(col.gameObject);
        }
        if (col.CompareTag("powerup3"))
        {
            health++;

            GetComponent<PlayerShooter>().gunType = 3;

            Destroy(col.gameObject);
        }
        if (col.CompareTag("powerup4"))
        {
            health++;

            GetComponent<PlayerShooter>().gunType = 4;

            Destroy(col.gameObject);
        }
    }
}
