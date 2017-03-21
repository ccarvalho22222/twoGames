using UnityEngine;
using System.Collections;

public class SmartEnemyMovement : MonoBehaviour {

    public float speed;

    GameObject player;

    public GameObject shot;
    GameObject tempShot;
    private float shootDelay;
    public float shootSpeed;

    private Rigidbody rb;
    public Vector3 movement;

    public float bulletSpeed;
    private float bulletRotation = 0.0f;

    public float lifeTime;
    private int counter;

    public int level;

    bool moving = true;
    
    void Start()
    {
        lifeTime = lifeTime * 60;

        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player Ship");

        //add cylinder scroll speed to enemies.
        speed += GameObject.Find("Level Controller").GetComponent<LevelController>().scrollSpeed;

        //seed rng
        Random.InitState((int)(Time.time * 1000));
    }

    void Update()
    {
        if((transform.position.z - player.transform.position.z) < 5.0f)
        {
            speed = 0;

            if(moving)
            {
                moving = false;
            }
        }
        if(!moving)
        {

            if(counter++ > lifeTime)
            {
                moving = true;
                speed = -2f;
            }

            //start shooting at player
            if (Time.time > shootDelay)
            {
                shootDelay = Time.time + shootSpeed;
                if(level >= 1)
                {
                    tempShot = (GameObject)Instantiate(shot, transform.position, transform.rotation);
                    shootSpeed = 2f;
                }
                if(level >= 2)
                {
                    //tempShot = (GameObject)Instantiate(shot, transform.position, transform.rotation);
                    ////randomize bullet rotation and speed
                    //bulletSpeed = Random.value + 0.15f;

                    ////change rotation speed of individual bullets
                    //bulletRotation = bulletSpeed;
                    //tempShot.GetComponent<GenericRotation>().baseSpeed = bulletRotation;
                    //tempShot.GetComponent<Projectile>().speed = bulletSpeed;
                    shootSpeed = 1f;
                }
                if(level >= 3)
                {
                    //tempShot = (GameObject)Instantiate(shot, transform.position, transform.rotation);
                    ////randomize bullet rotation and speed
                    //bulletSpeed = Random.value + 0.15f;

                    ////change rotation speed of individual bullets
                    //bulletRotation = -bulletSpeed;
                    //tempShot.GetComponent<GenericRotation>().baseSpeed = bulletRotation;
                    //tempShot.GetComponent<Projectile>().speed = bulletSpeed;
                    shootSpeed = 0.5f;
                }
                if (level >= 4)
                {
                    //shootSpeed = 2;
                    //tempShot = (GameObject)Instantiate(shot, transform.position, transform.rotation);
                    ////randomize bullet rotation and speed
                    //bulletSpeed = Random.value + 0.25f;

                    ////change rotation speed of individual bullets
                    //bulletRotation = bulletSpeed;
                    //tempShot.GetComponent<GenericRotation>().baseSpeed = bulletRotation;
                    //tempShot.GetComponent<Projectile>().speed = bulletSpeed;

                    //tempShot = (GameObject)Instantiate(shot, transform.position, transform.rotation);
                    ////randomize bullet rotation and speed
                    //bulletSpeed = Random.value + 0.25f;

                    ////change rotation speed of individual bullets
                    //bulletRotation = -bulletSpeed;
                    //tempShot.GetComponent<GenericRotation>().baseSpeed = bulletRotation;
                    //tempShot.GetComponent<Projectile>().speed = bulletSpeed;
                }
            }
        }

        movement = new Vector3(0.0f, 0.0f, speed);
        rb.velocity = movement;
    }
}
