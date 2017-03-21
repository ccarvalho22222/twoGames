using UnityEngine;
using System.Collections;

public class MineBossBehaviour : MonoBehaviour {

    public AudioClip enemyShoot;
    public AudioClip pushBackSound;

    public int phase = 0;   //what phase the boss is in

    private Rigidbody2D rB;
    public GameObject Bullet;
    public GameObject PushBullet;

    public Vector3 bulletVelocity;

    public float bulletStartX;
    public float bulletStartY;

    public float shootAngle;
    public float shootAngleIncrement;
    public float bulletSpeed;

    public int shootCooldown;
    public int shootCounter = 0;
    public int shootTime;
    public int shootTimeCounter = 0;
    public int shootDelayTime;
    public int shootDelayCounter = 0;
    public int numShots;

    public int hp;
    public int phaseOneHP;
    public int phaseTwoHP;
    public int phaseThreeHP;

    public int moveCooldown;
    public int moveCooldownCounter;

    public float dashSpeed;
    public float brakes;

    private float shootDelayTimeOne;
    private float shootAngleIncrementOne;

    public GameObject enemy;

    public int enemySpawnCooldown;
    private int enemySpawnCounter;
    
    public Vector3 enemySpawn1;
    public Vector3 enemySpawn2;
    public Vector3 enemySpawn3;
    public Vector3 enemySpawn4;


    // Use this for initialization
    void Start () {
        rB = GetComponent<Rigidbody2D>();
        Random.InitState((int)Time.time);
	}
	
	// Update is called once per frame
	void Update () {

        hp = GetComponent<EnemyHpScript>().hp;

        if (GetComponent<EnemyEnabledScript>().engaged)
        {
            if (hp <= phaseOneHP && phase == 0)
            {
                shootDelayTimeOne = 30;
                shootAngleIncrementOne = 1.5f;
                moveCooldown = (int)(moveCooldown * 0.75f);
                phase = 1;
                pushBack();
            }
            if (hp <= phaseTwoHP && phase == 1)
            {
                shootDelayTimeOne = 60;
                shootAngleIncrementOne = 1.5f;
                phase = 2;
                pushBack();
            }
            if (hp <= phaseThreeHP && phase == 2)
            {
                shootDelayTime = (int)(shootDelayTime * 0.5f);
                shootAngleIncrement *= 2f;
                shootCooldown *= 2;
                bulletSpeed *= 0.5f;
                moveCooldown = (int)(moveCooldown * 0.75f);

                phase = 3;
                pushBack();
            }

            if (phase == 0)
            {
                phaseZeroShooting();
                phaseZeroMovement();
            }
            if (phase == 1)
            {
                phaseOneShooting();
                phaseZeroMovement();
            }
            if (phase == 2)
            {
                phaseOneShooting();
                phaseZeroMovement();
                spawnEnemies();
            }

            if (phase == 3)
            {
                phaseZeroShooting();
                phaseZeroMovement();
            }
        }
        //brakes
        if (Mathf.Abs(rB.velocity.x) > 0.5f)
        {
            rB.AddForce(new Vector3(Mathf.Sign(rB.velocity.x) * -1 * brakes, 0f, 0f));
        }
        else rB.velocity = new Vector3(0f, rB.velocity.y, 0f);
        if (Mathf.Abs(rB.velocity.y) > 0.5f)
        {

            rB.AddForce(new Vector3(0f, Mathf.Sign(rB.velocity.y) * -1 * brakes, 0f));

        }
        else rB.velocity = new Vector3(rB.velocity.x, 0f, 0f);
    }

    void spawnEnemies()
    {
        if (enemySpawnCounter >= enemySpawnCooldown)
        {
            GameObject tempEnemy;

            tempEnemy = (GameObject)Instantiate(enemy, enemySpawn1, transform.rotation);
            tempEnemy.GetComponent<EnemyEnabledScript>().engaged = true;

            tempEnemy = (GameObject)Instantiate(enemy, enemySpawn2, transform.rotation);
            tempEnemy.GetComponent<EnemyEnabledScript>().engaged = true;

            tempEnemy = (GameObject)Instantiate(enemy, enemySpawn3, transform.rotation);
            tempEnemy.GetComponent<EnemyEnabledScript>().engaged = true;

            tempEnemy = (GameObject)Instantiate(enemy, enemySpawn4, transform.rotation);
            tempEnemy.GetComponent<EnemyEnabledScript>().engaged = true;

            enemySpawnCounter = 0;
        }
        else
            enemySpawnCounter++;
    }

    void phaseOneShooting()
    {
        if (shootCounter >= shootCooldown)
        {
            if (shootTimeCounter < shootTime)
            {
                if (shootDelayCounter > shootDelayTimeOne)
                {
                    float tempAngle = 0f;
                    for (int i = 1; i < numShots; i++)
                    {
                        shoot(shootAngle + tempAngle);
                        tempAngle += (Mathf.PI / 32);
                    }
                    shootAngle += shootAngleIncrementOne;
                    shootDelayCounter = 0;
                }
                else
                {
                    shootDelayCounter++;
                }
                shootTimeCounter++;
            }
            else
            {
                shootTimeCounter = 0;
                shootCounter = 0;
            }
        }
        else
        {
            shootCounter++;
        }
    }

    void phaseZeroMovement()
    {
        if(moveCooldownCounter >= moveCooldown)
        {
            float randAngle = Random.Range(0, Mathf.PI * 2f);
            //move in random direction
            rB.velocity = new Vector3(dashSpeed * Mathf.Sin(randAngle), dashSpeed * Mathf.Cos(randAngle));
            moveCooldownCounter = 0;
        }
        else
        {
            moveCooldownCounter++;
        }
    }

    void pushBack()
    {
        float shootAngle = 0;
        while(shootAngle < 2 * Mathf.PI)
        {
            bulletStartX = transform.position.x + 1f * Mathf.Cos(shootAngle);
            bulletStartY = transform.position.y + 1f * Mathf.Sin(shootAngle);

            GameObject tempBullet = (GameObject)Instantiate(PushBullet, new Vector3(bulletStartX, bulletStartY), Bullet.transform.rotation);
            tempBullet.SetActive(true);

            tempBullet.GetComponent<MineBossPushBulletBehaviour>().bulletVelocity = new Vector3(bulletSpeed * 2f * Mathf.Cos(shootAngle),
                                                                            bulletSpeed * 2f * Mathf.Sin(shootAngle));

            shootAngle += (Mathf.PI / 16);
        }
    }

    void phaseZeroShooting()
    {
        if (shootCounter >= shootCooldown)
        {
            if (shootTimeCounter < shootTime)
            {
                if (shootDelayCounter > shootDelayTime)
                {
                    shoot(shootAngle);
                    shoot(shootAngle + (Mathf.PI));
                    shootAngle += shootAngleIncrement;
                    shootDelayCounter = 0;
                }
                else
                {
                    shootDelayCounter++;
                }
                shootTimeCounter++;
            }
            else
            {
                shootTimeCounter = 0;
                shootCounter = 0;
            }
        }
        else
        {
            shootCounter++;
        }
    }

    void shoot(float shootAngle)
    {
        AudioSource.PlayClipAtPoint(enemyShoot, transform.position);

        bulletStartX = transform.position.x + 2f * Mathf.Cos(shootAngle);
        bulletStartY = transform.position.y + 2f * Mathf.Sin(shootAngle);

        GameObject tempBullet = (GameObject)Instantiate(Bullet, new Vector3(bulletStartX, bulletStartY), Bullet.transform.rotation);
        tempBullet.SetActive(true);

        tempBullet.GetComponent<MineBossBulletBehaviour>().bulletVelocity = new Vector3(bulletSpeed * Mathf.Cos(shootAngle),
                                                                        bulletSpeed * Mathf.Sin(shootAngle));
    }
}
