using UnityEngine;
using System.Collections;

public class PlayerShooter : MonoBehaviour {
    
    private Transform parent;
    public float shootSpeed;

    private float shotDelay = 0f;

    public GameObject shot;
    public GameObject shot2;
    public GameObject shot3;
    public GameObject shot4;
    public GameObject shot5;

    public GameObject redLight;
    public GameObject blueLight;
    public AudioClip[] shotS;

    private GameObject currentShot; //set this to what shot you want to use

    public float spreadAngle;
    public float bulletWaveAmp;
    public float bulletWaveSpeed;

    public bool overheated;
    private int cooldownCounter;            //counts down to see if gun should start cooling down

    public int cooldownTime;                //time it takes for the gun to start cooling down
    public float overheatCooldownAmmount;   //ammount that is cooled off from gun if its overheated
    public float normalCooldownAmmount;     //ammount that is cooled off from gun if it isn't overheated
    public int maxOverheat;                 //maximum heat the gun can get to before it shuts down to cool
    public float weaponHeat;                //current heat of the weapon
    public float bulletHeat;                //heat that each shot adds to the weapon
    public int damage;                      //damage that each bullet does
    public int numShots;                    //number of shots gun shoots
    public float shotSpeed;                 //speed of projectile

    public int gunType;                     //set to number 0 - n to show what type of gun is being used.

    public bool cooldownPlayed = false;

    //	private GameObject shotSpawn;
    // Use this for initialization
    void Start()
    {
        parent = GameObject.Find("Bullet Cylinder").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        //normal 1 bullet gun
        if(gunType == 0)
        {
            //set parameters here.
            damage = 1;
            bulletWaveAmp = 0;
            bulletWaveSpeed = 0;
            spreadAngle = 0;
            cooldownTime = 30;
            overheatCooldownAmmount = 0.2f;
            normalCooldownAmmount = 0.4f;
            maxOverheat = 15;
            bulletHeat = 1;
            numShots = 1;
            shotSpeed = 8;
            currentShot = shot;
        }

        //gun with more damage but slower
        if(gunType == 1)
        {
            damage = 3;
            bulletWaveAmp = 0;
            bulletWaveSpeed = 0;
            spreadAngle = 0;
            cooldownTime = 60;
            overheatCooldownAmmount = 0.1f;
            normalCooldownAmmount = 0.5f;
            maxOverheat = 21;
            bulletHeat = 3;
            numShots = 1;
            shotSpeed = 5;
            currentShot = shot2;
        }

        //normal damage wave split gun
        if (gunType == 2)
        {
            //set parameters here.
            damage = 1;
            bulletWaveAmp = 0.4f;
            bulletWaveSpeed = 5f;
            spreadAngle = 0.2f;
            cooldownTime = 30;
            overheatCooldownAmmount = 0.1f;
            normalCooldownAmmount = 0.25f;
            maxOverheat = 12;
            bulletHeat = 2;
            numShots = 3;
            shotSpeed = 6;
            currentShot = shot3;
        }

        //better damage wave gun
        if (gunType == 3)
        {
            //set parameters here.
            damage = 2;
            bulletWaveAmp = 4f;
            bulletWaveSpeed = 2f;
            spreadAngle = 0.2f;
            cooldownTime = 30;
            overheatCooldownAmmount = 0.1f;
            normalCooldownAmmount = 0.25f;
            maxOverheat = 8;
            bulletHeat = 1;
            numShots = 1;
            shotSpeed = 8;
            currentShot = shot4;
        }

        //bigger split no wave
        if(gunType == 4)
        {
            //set parameters here.
            damage = 1;
            bulletWaveAmp = 0.0f;
            bulletWaveSpeed = 0f;
            spreadAngle = 0.45f;
            cooldownTime = 30;
            overheatCooldownAmmount = 0.2f;
            normalCooldownAmmount = 0.4f;
            maxOverheat = 15;
            bulletHeat = 3;
            numShots = 5;
            shotSpeed = 8;
            currentShot = shot5;
        }



        if ((overheated && (weaponHeat > 0)))
        {
            weaponHeat -= overheatCooldownAmmount;
        }
        if (cooldownCounter == 0 && weaponHeat > 0)
        {
            if(!cooldownPlayed)
            {
                AudioSource.PlayClipAtPoint(shotS[4], transform.position, 1.0f);
                cooldownPlayed = true;
            }
            
            weaponHeat -= normalCooldownAmmount;
        }
        if (weaponHeat <= 0)
        {
            
            weaponHeat = 0;
        }
        if (weaponHeat <= 0 && overheated == true)
        {
            redLight.SetActive(false);
            blueLight.SetActive(true);
            overheated = false;
        }

        if (!overheated && cooldownCounter > 0)
        {
            cooldownCounter--;
        }
        else if(cooldownCounter < 0)
        {
            cooldownCounter = 0;
        }

        if (weaponHeat >= maxOverheat)
        {
            blueLight.SetActive(false);
            redLight.SetActive(true);
            AudioSource.PlayClipAtPoint(shotS[3], transform.position);
            overheated = true;
            weaponHeat = maxOverheat;
            weaponHeat -= overheatCooldownAmmount;
        }
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire3"))
        {

            if (Time.time < shotDelay)
                return;
            shotDelay = Time.time + 0.1f;

            cooldownCounter = cooldownTime;
            cooldownPlayed = false;

            if (!overheated)
            {
                //add heat to gun
                weaponHeat += bulletHeat;

                //Weapon sounds change pitch depending on level of heat
                if (weaponHeat / maxOverheat >= 0f && weaponHeat / maxOverheat < 1f/3f)
                {
                    AudioSource.PlayClipAtPoint(shotS[0], transform.position);
                }
                if(weaponHeat / maxOverheat >= 1f/3f && weaponHeat / maxOverheat < 2f/3f)
                {
                    AudioSource.PlayClipAtPoint(shotS[1], transform.position);
                }
                if (weaponHeat / maxOverheat >= 2f/3f && weaponHeat / maxOverheat < 3f/3f)
                {
                    AudioSource.PlayClipAtPoint(shotS[2], transform.position);
                }
                
                //creates multiple shots and gives them rotations depending on spreadAngle
                for (int i = 0; i < numShots; i++)
                {
                    Vector3 bulletPosition = new Vector3(parent.position.x, parent.position.y, transform.position.z + 0.05f);
                    Quaternion bulletRotation = new Quaternion(
                                            parent.rotation.x,
                                            parent.rotation.y,
                                            parent.rotation.z + (transform.position.y * 0.125f),
                                            parent.rotation.w);

                    GameObject tempShot = (GameObject)Instantiate(currentShot, bulletPosition, bulletRotation);

                    //set damage of bullet
                    tempShot.GetComponent<Projectile>().damage = damage;
                    tempShot.GetComponent<Projectile>().speed = shotSpeed;

                    //tempShot.transform.parent = GameObject.Find("Bullet Cylinder").transform;
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
}
