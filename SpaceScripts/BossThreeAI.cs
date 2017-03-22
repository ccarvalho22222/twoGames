using UnityEngine;
using System.Collections;

public class BossThreeAI : MonoBehaviour {


    public bool done = false;

    public GameObject ship1;
    public GameObject ship2;
    public GameObject player;
    public GameObject shot;
    
    public GameObject explosion;

    public int hp1;
    public int hp2;

    private bool rotating1 = true;
    private bool rotating2 = true;

    public float speed1;
    public float speed2;

    public float rotateSpeed1;
    public float rotateSpeed2;

    public float bulletSpeed1;
    public float bulletSpeed2;


    public int switchTime1;
    public int switchTime2;

    public int counter1;
    public int counter2;

    public bool forward1 = false;
    public bool forward2 = false;
    public bool backward1 = false;
    public bool backward2 = false;

    private int moveTime1 = 0;
    private int moveTime2 = 0;

    public bool crazy1 = false;
    public bool crazy2 = false;

    public int moveTimeMin1;
    public int moveTimeMin2;

    public int moveTimeMax1;
    public int moveTimeMax2;

    private bool firstShot1 = false;
    private bool firstShot2 = false;

    private bool boss1Dead = false;
    private bool boss2Dead = false;



    // Use this for initialization
    void Start () {

        //convert to frames time
        switchTime1 *= 60;
        switchTime2 *= 60;

        ship1.GetComponent<GenericRotation>().speed = rotateSpeed1;
        ship2.GetComponent<GenericRotation>().speed = rotateSpeed2;
        ship1.GetComponent<BossMovement>().speed = 0;
        ship2.GetComponent<BossMovement>().speed = 0;

        player = GameObject.Find("Player Ship");
    }
	
	// Update is called once per frame
	void Update () {

        
        if ((hp1 <= 0) && (hp2 <= 0))
        {
            done = true;
            GetComponentInChildren<MeshRenderer>().enabled = false;
            enabled = false;
        }
        if((hp1 <= 0) && (!boss1Dead))
        {
            Vector3 position = new Vector3(ship1.transform.position.x, ship1.transform.position.y, ship1.transform.position.z);
            Instantiate(explosion, position, transform.parent.transform.rotation, transform.parent.transform);
            Destroy(ship1.gameObject);
            boss1Dead = true;
        }
        if((hp2 <= 0) && (!boss2Dead))
        {
            Vector3 position = new Vector3(ship2.transform.position.x, ship2.transform.position.y, ship2.transform.position.z);
            Instantiate(explosion, position, transform.parent.transform.rotation, transform.parent.transform);
            Destroy(ship2.gameObject);
            boss2Dead = true;
        }

        if ((hp1 <= 10) && (!crazy1) && (rotating1))
        {
            //crazy mode
            //speed1 *= 1.5f;
            rotateSpeed1 *= 2;
            switchTime1 /= 3;
            bulletSpeed1 *= 2;
            
            //moveTimeMax1 /= 2;
            crazy1 = true;
            
        }
        if ((hp2 <= 10) && (!crazy2) && (rotating2))
        {
            //crazy mode
            //speed2 *= 1.5f;
            rotateSpeed2 *= 2;
            switchTime2 /= 3;
            bulletSpeed2 *= 2;
            
            //moveTimeMax2 /= 2;
            crazy2 = true;
        }



        if ((counter1 >= switchTime1) && (rotating1 == true))
        {
            
            counter1 = 0;
            rotating1 = false;
            forward1 = true;
            moveTime1 = Random.Range(moveTimeMin1, moveTimeMax1);
        }
        else if(rotating1 == true)
        {
            counter1++;
        }

        if ((counter2 >= switchTime2)&& (rotating2 == true))
        {
            
            counter2 = 0;
            rotating2 = false;
            forward2 = true;
            moveTime2 = Random.Range(moveTimeMin2, moveTimeMax2);
        }
        else if (rotating2 == true)
        {
            counter2++;
        }

        if ((rotating1 == true) && (!boss1Dead))
        {
            ship1.GetComponent<GenericRotation>().speed = rotateSpeed1;
        }
        if ((rotating2 == true) && (!boss2Dead))
        {
            ship2.GetComponent<GenericRotation>().speed = rotateSpeed2;
        }

        if ((rotating1 == false) && (!boss1Dead))
        {
            if (forward1)
            {
                ship1.GetComponent<GenericRotation>().speed = 0;
                ship1.GetComponent<BossMovement>().speed = -speed1;
                if ((counter1 > moveTime1 / 2) && (!firstShot1))
                {
                    firstShot1 = true;
                    //shoot a projectile up
                    GameObject tempShot = (GameObject)Instantiate(shot, ship1.transform.position, ship1.transform.rotation);
                    float bulletRotation = bulletSpeed1;
                    tempShot.GetComponent<GenericRotation>().baseSpeed = -bulletRotation;
                    tempShot.GetComponent<Projectile>().speed = 0;
                }
                if (counter1++ > moveTime1)
                {
                    forward1 = false;
                    backward1 = true;
                    firstShot1 = false;
                    counter1 = 0;

                    //shoot a projectile down
                    GameObject tempShot = (GameObject)Instantiate(shot, ship1.transform.position, ship1.transform.rotation);
                    float bulletRotation = bulletSpeed1;
                    tempShot.GetComponent<GenericRotation>().baseSpeed = bulletRotation;
                    tempShot.GetComponent<Projectile>().speed = 0;
                }
            }
            if(backward1)
            {
                ship1.GetComponent<BossMovement>().speed = speed1;
                if (counter1++ > moveTime1)
                {
                    backward1 = false;
                    rotating1 = true;
                    ship1.GetComponent<GenericRotation>().speed = rotateSpeed1;
                    ship1.GetComponent<BossMovement>().speed = 0;
                    counter1 = 0;
                }
            }
            
        }
        if ((rotating2 == false) && (!boss2Dead))
        {
            if (forward2)
            {
                ship2.GetComponent<GenericRotation>().speed = 0;
                ship2.GetComponent<BossMovement>().speed = -speed2;
                if ((counter2 > moveTime2 / 2) && (!firstShot2))
                {
                    firstShot2 = true;
                    //shoot a projectile down
                    GameObject tempShot = (GameObject)Instantiate(shot, ship2.transform.position, ship2.transform.rotation);
                    float bulletRotation = bulletSpeed2;
                    tempShot.GetComponent<GenericRotation>().baseSpeed = bulletRotation;
                    tempShot.GetComponent<Projectile>().speed = 0;
                }
                if (counter2++ > moveTime2)
                {
                    firstShot2 = false;
                    forward2 = false;
                    backward2 = true;
                    counter2 = 0;

                    //shoot a projectile down
                    GameObject tempShot = (GameObject)Instantiate(shot, ship2.transform.position, ship2.transform.rotation);
                    float bulletRotation = -bulletSpeed2;
                    tempShot.GetComponent<GenericRotation>().baseSpeed = bulletRotation;
                    tempShot.GetComponent<Projectile>().speed = 0;
                }
            }
            if (backward2)
            {
                ship2.GetComponent<BossMovement>().speed = speed2;
                if (counter2++ > moveTime2)
                {
                    backward2 = false;
                    rotating2 = true;
                    ship2.GetComponent<GenericRotation>().speed = rotateSpeed2;
                    ship2.GetComponent<BossMovement>().speed = 0;
                    counter2 = 0;
                }
            }
        }
    }
}
