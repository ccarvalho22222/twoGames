using UnityEngine;
using System.Collections;

public class TelegrapherAI : MonoBehaviour {

    public AudioClip bombSound;
    public AudioClip telegraphSound;

    public GameObject explosion1;
    public GameObject explosion2;
    public GameObject explosion3;
    public GameObject explosion4;

    public GameObject Spike;
    public GameObject SpikeTelegraph;

    public float spikeRotateSpeed;
    public float spikeRotationTolerance;

    private float spikeAngle;
    private float mouseAngle;

    public float attackCooldownTime;
    private float attackCooldownCounter = 0;
    public float attackSpeed;

    public int attackDamage;
    private bool attacking = false;
    public float attackTime;
    private float attackTimeCounter;
    public int attackWaitTime;
    private int attackWaitTimeCounter;
    public int executeAttackWaitTime;
    private int executeAttackWaitTimeCounter;

    private float spikeXDistance;
    private float spikeYDistance;
    private float mouseXDistance;
    private float mouseYDistance;

    private float spikeQuadrant;
    private float mouseQuadrant;

    public bool spikeDestroyed = false;

    public float speed = -0.25f;
    public float terminalVelocity;
    public float engageDistance = 10.0f;

    public bool engaged = false;

    GameObject player;// = GameObject.Find("Player");
    Vector3 playerPosition;// = player.transform.position;

    private Rigidbody2D rB;// = GetComponent<Rigidbody2D>();

    float xDistance;// = 0.0f;
    float yDistance;// = 0.0f;
    float distance;// = 0.0f;

    bool attackStart = false;
    bool telegraphing = false;
    bool executeAttack = false;

    public int attackRange;


    private float playerXDistance;
    private float playerYDistance;

    private float playerQuadrant;
    private float playerAngle;

    public float explosionRange;

    // Use this for initialization
    void Start () {
        rB = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

        xDistance = 0.0f;
        yDistance = 0.0f;
        distance = 0.0f;
        attackCooldownCounter = attackCooldownTime;
        Random.InitState((int)Time.time);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<EnemyEnabledScript>().engaged)
        {
            if(!attacking)
            {
                move();
                distance = Mathf.Sqrt(xDistance * xDistance + yDistance * yDistance);
                rotatePeripheral();

                if (distance < attackRange)
                {
                    if(attackCooldownCounter >= attackCooldownTime) 
                    {
                        if (Mathf.Abs(spikeAngle - mouseAngle) < Mathf.PI / 4)
                        {
                            attackStart = true;
                            attacking = true;
                            rB.angularVelocity *= 0.25f;
                            rB.velocity *= 0.25f;
                            attackCooldownCounter = 0;
                        }
                    }   
                }
                attackCooldownCounter++;
            }
            else if(attacking)
            {
                if (attackStart)
                {
                    SpikeTelegraph.SetActive(true);
                    attackStart = false;
                    telegraphing = true;
                }
                if(telegraphing)
                {
                    if(attackWaitTimeCounter++ > attackWaitTime)
                    {
                        //make explosions
                        Spike.SetActive(true);
                        SpikeTelegraph.SetActive(false);
                        executeAttack = true;
                        telegraphing = false;
                        attackWaitTimeCounter = 0;
                    }
                    else if(attackWaitTimeCounter % 15 == 0)
                    {
                        if (SpikeTelegraph.activeSelf)
                            SpikeTelegraph.SetActive(false);
                        else
                        {
                            SpikeTelegraph.SetActive(true);
                            AudioSource.PlayClipAtPoint(telegraphSound, SpikeTelegraph.transform.position);
                        }
                    }
                    
                }
                if(executeAttack)
                {
                    if(executeAttackWaitTimeCounter++ > executeAttackWaitTime)
                    {
                        
                        Spike.SetActive(false);
                        SpikeTelegraph.SetActive(false);
                        executeAttack = false;
                        attacking = false;
                        executeAttackWaitTimeCounter = 0;
                    }
                    else
                    {
                        if (executeAttackWaitTimeCounter % 1 == 0)
                        {
                            int rand = Random.Range(1, 5);
                            float randX = Random.Range(-explosionRange, explosionRange);
                            float randY = Random.Range(-explosionRange, explosionRange);
                            Vector3 explosionPosition = new Vector3(SpikeTelegraph.transform.position.x + randX,
                                                                   SpikeTelegraph.transform.position.y + randY);
                            if (rand == 1)
                                Instantiate(explosion1, explosionPosition, SpikeTelegraph.transform.rotation);
                            if (rand == 2)
                                Instantiate(explosion2, explosionPosition, SpikeTelegraph.transform.rotation);
                            if (rand == 3)
                                Instantiate(explosion3, explosionPosition, SpikeTelegraph.transform.rotation);
                            if (rand == 4)
                                Instantiate(explosion4, explosionPosition, SpikeTelegraph.transform.rotation);
                            AudioSource.PlayClipAtPoint(bombSound, explosionPosition);
                        }

                    }
                }
            }
        }
    }

    void move()
    {
        //playerPosition = player.transform.position;

        //xDistance = transform.position.x - playerPosition.x;
        //yDistance = transform.position.y - playerPosition.y;

        ////add extra force if turning around toward player
        //if (-Mathf.Sign(xDistance) != Mathf.Sign(rB.velocity.x))
        //{
        //    rB.AddForce(new Vector3(xDistance * 2f * speed, 0f, 0.0f));
        //}
        //if (-Mathf.Sign(yDistance) != Mathf.Sign(rB.velocity.y))
        //{
        //    rB.AddForce(new Vector3(0f, yDistance * 2f * speed, 0.0f));
        //}
        //if (rB.velocity.x < terminalVelocity)
        //    rB.AddForce(new Vector3(xDistance * speed, 0f, 0.0f));
        //if (rB.velocity.y < terminalVelocity)
        //    rB.AddForce(new Vector3(0f, yDistance * speed, 0.0f));

        playerXDistance = GameObject.Find("Player").transform.position.x - transform.position.x;
        playerYDistance = GameObject.Find("Player").transform.position.y - transform.position.y;

        //have logic based on what quadrant its in, to keep angle accurate
        if (playerXDistance >= 0 && playerYDistance >= 0)
        {
            //first quadrant
            playerAngle = Mathf.Atan(Mathf.Abs(playerYDistance / playerXDistance));
            playerQuadrant = 1;
        }
        else if (playerXDistance <= 0 && playerYDistance >= 0)
        {
            //second quadrant
            playerAngle = Mathf.Atan(Mathf.Abs(playerXDistance / playerYDistance)) + (Mathf.PI / 2f);
            playerQuadrant = 2;
        }
        else if (playerXDistance <= 0 && playerYDistance <= 0)
        {
            //third quadrant
            playerAngle = Mathf.Atan(Mathf.Abs(playerYDistance / playerXDistance)) + (Mathf.PI);
            playerQuadrant = 3;
        }
        else if (playerXDistance >= 0 && playerYDistance <= 0)
        {
            //fourth quadrant
            playerAngle = Mathf.Atan(Mathf.Abs(playerXDistance / playerYDistance)) + (3f * Mathf.PI / 2f);
            playerQuadrant = 4;
        }

        rB.AddForce(new Vector3(Mathf.Cos(playerAngle) * speed, Mathf.Sin(playerAngle) * speed, 0f));
    }

    //rotate spike toward mouse pointer
    void rotatePeripheral()
    {
        //get angle that spike is at relative to player
        spikeXDistance = Spike.transform.position.x - transform.position.x;
        spikeYDistance = Spike.transform.position.y - transform.position.y;

        //have logic based on what quadrant its in, to keep angle accurate
        if (spikeXDistance >= 0 && spikeYDistance >= 0)
        {
            //first quadrant
            spikeAngle = Mathf.Atan(Mathf.Abs(spikeYDistance / spikeXDistance));
            spikeQuadrant = 1;
        }
        else if (spikeXDistance <= 0 && spikeYDistance >= 0)
        {
            //second quadrant
            spikeAngle = Mathf.Atan(Mathf.Abs(spikeXDistance / spikeYDistance)) + (Mathf.PI / 2f);
            spikeQuadrant = 2;
        }
        else if (spikeXDistance <= 0 && spikeYDistance <= 0)
        {
            //third quadrant
            spikeAngle = Mathf.Atan(Mathf.Abs(spikeYDistance / spikeXDistance)) + (Mathf.PI);
            spikeQuadrant = 3;
        }
        else if (spikeXDistance >= 0 && spikeYDistance <= 0)
        {
            //fourth quadrant
            spikeAngle = Mathf.Atan(Mathf.Abs(spikeXDistance / spikeYDistance)) + (3f * Mathf.PI / 2f);
            spikeQuadrant = 4;
        }

        mouseXDistance = GameObject.Find("Player").transform.position.x - transform.position.x;
        mouseYDistance = GameObject.Find("Player").transform.position.y - transform.position.y;
        
        //have logic based on what quadrant its in, to keep angle accurate
        if (mouseXDistance >= 0 && mouseYDistance >= 0)
        {
            //first quadrant
            mouseAngle = Mathf.Atan(Mathf.Abs(mouseYDistance / mouseXDistance));
            mouseQuadrant = 1;
        }
        else if (mouseXDistance <= 0 && mouseYDistance >= 0)
        {
            //second quadrant
            mouseAngle = Mathf.Atan(Mathf.Abs(mouseXDistance / mouseYDistance)) + (Mathf.PI / 2f);
            mouseQuadrant = 2;
        }
        else if (mouseXDistance <= 0 && mouseYDistance <= 0)
        {
            //third quadrant
            mouseAngle = Mathf.Atan(Mathf.Abs(mouseYDistance / mouseXDistance)) + (Mathf.PI);
            mouseQuadrant = 3;
        }
        else if (mouseXDistance >= 0 && mouseYDistance <= 0)
        {
            //fourth quadrant
            mouseAngle = Mathf.Atan(Mathf.Abs(mouseXDistance / mouseYDistance)) + (3f * Mathf.PI / 2f);
            mouseQuadrant = 4;
        }


        //different cases that need to be handled differently
        if (spikeQuadrant == 1 && mouseQuadrant == 4)
        {
            //clockwise

            rB.angularVelocity = -spikeRotateSpeed;
            //rB.AddTorque(-spikeRotateSpeed, ForceMode2D.Force);
            //SpikeRotator.transform.Rotate(new Vector3(0f, 0f, 1f), -spikeRotateSpeed);
        }
        else if (spikeQuadrant == 4 && mouseQuadrant == 1)
        {
            //counter clockwise
            rB.angularVelocity = spikeRotateSpeed;
            //rB.AddTorque(spikeRotateSpeed, ForceMode2D.Force);
            //SpikeRotator.transform.Rotate(new Vector3(0f, 0f, 1f), spikeRotateSpeed);
        }
        else if ((spikeQuadrant == 1 && mouseQuadrant == 3) || (spikeQuadrant == 3 && mouseQuadrant == 1))
        {
            if (Mathf.Abs(spikeAngle - mouseAngle) > (2 * Mathf.PI - (spikeAngle) + mouseAngle))
            {
                //counter clockwise
                rB.angularVelocity = spikeRotateSpeed;
                //rB.AddTorque(spikeRotateSpeed, ForceMode2D.Force);
            }
            else
            {
                rB.angularVelocity = -spikeRotateSpeed;
                //rB.AddTorque(-spikeRotateSpeed, ForceMode2D.Force);
            }
        }
        else if ((spikeQuadrant == 2 && mouseQuadrant == 4) || (spikeQuadrant == 4 && mouseQuadrant == 2))
        {
            if ((spikeAngle - mouseAngle) < (2 * Mathf.PI + (spikeAngle) - mouseAngle))
            {
                //counter clockwise
                rB.angularVelocity = spikeRotateSpeed;
                //rB.AddTorque(spikeRotateSpeed, ForceMode2D.Force);
            }
            else
            {
                rB.angularVelocity = -spikeRotateSpeed;
                //rB.AddTorque(-spikeRotateSpeed, ForceMode2D.Force);
            }
        }
        //normal case
        else
        {
            if (spikeAngle - mouseAngle < -spikeRotationTolerance)
            {
                //counter clockwise
                rB.angularVelocity = spikeRotateSpeed;
                //rB.AddTorque(spikeRotateSpeed, ForceMode2D.Force);
                //SpikeRotator.transform.Rotate(new Vector3(0f, 0f, 1f), spikeRotateSpeed);
            }
            else if (spikeAngle - mouseAngle > spikeRotationTolerance)
            {
                //clockwise
                rB.angularVelocity = -spikeRotateSpeed;
                //rB.AddTorque(-spikeRotateSpeed, ForceMode2D.Force);
                //SpikeRotator.transform.Rotate(new Vector3(0f, 0f, 1f), -spikeRotateSpeed);
            }
            else
            {
                //dead zone, stop rotation
                rB.angularVelocity *= 0.5f;
            }
        }
    }
}
