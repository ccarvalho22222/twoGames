using UnityEngine;
using System.Collections;

public class WallerBotBehaviour : MonoBehaviour
{

    public GameObject Gun;
    

    public float GunRotateSpeed;
    public float GunRotationTolerance;

    private float GunAngle;
    private float playerAngle;

    public GameObject Bullet;   //Bullet for Gun Peripheral
    public float bulletSpeed;

    private float shootStallTime = 10;
    private float shootStallCounter = 0;

    public float shotCooldownTime;
    private float shotCooldownCounter = 0;

    private float GunXDistance;
    private float GunYDistance;
    private float playerXDistance;
    private float playerYDistance;

    private float GunQuadrant;
    private float playerQuadrant;

    public bool GunDestroyed = false;

    private Rigidbody2D rB;

    public float speed;
    public float terminalVelocity;

    public bool engaged;
    public float engageDistance;

    GameObject player;// = GameObject.Find("Player");
    Vector3 playerPosition;// = player.transform.position;


    float xDistance;// = 0.0f;
    float yDistance;// = 0.0f;
    float distance;// = 0.0f;

    // Use this for initialization
    void Start()
    {

        rB = GetComponent<Rigidbody2D>();
        engaged = false;

        player = GameObject.Find("Player");
        
        


    }

    // Update is called once per frame
    void Update()
    {

        if (GetComponent<EnemyEnabledScript>().engaged)
        {
            engaged = true;
        }
        else
        {
            engaged = false;
        }

        //if (!engaged)
        //{
        //    xDistance = transform.position.x - playerPosition.x;
        //    yDistance = transform.position.y - playerPosition.y;

        //    distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));

        //    playerPosition = player.transform.position;

        //    if (distance < engageDistance)
        //    {
        //        engaged = true;
        //    }
        //}
        if (engaged)
        {
            rotatePeripheral();

            xDistance = transform.position.x - playerPosition.x;
            yDistance = transform.position.y - playerPosition.y;

            distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));

            if (distance < engageDistance)
            {
                avoidPlayer();
            }
            else
            {
                followPlayer();
            }



            shoot();
        }
    }


    void shoot()
    {
        //get angle that Gun is at relative to player
        GunXDistance = Gun.transform.position.x - transform.position.x;
        GunYDistance = Gun.transform.position.y - transform.position.y;

        //have logic based on what quadrant its in, to keep angle accurate
        if (GunXDistance >= 0 && GunYDistance >= 0)
        {
            //first quadrant
            GunAngle = Mathf.Atan(Mathf.Abs(GunYDistance / GunXDistance));
            GunQuadrant = 1;
        }
        else if (GunXDistance <= 0 && GunYDistance >= 0)
        {
            //second quadrant
            GunAngle = Mathf.Atan(Mathf.Abs(GunXDistance / GunYDistance)) + (Mathf.PI / 2f);
            GunQuadrant = 2;
        }
        else if (GunXDistance <= 0 && GunYDistance <= 0)
        {
            //third quadrant
            GunAngle = Mathf.Atan(Mathf.Abs(GunYDistance / GunXDistance)) + (Mathf.PI);
            GunQuadrant = 3;
        }
        else if (GunXDistance >= 0 && GunYDistance <= 0)
        {
            //fourth quadrant
            GunAngle = Mathf.Atan(Mathf.Abs(GunXDistance / GunYDistance)) + (3f * Mathf.PI / 2f);
            GunQuadrant = 4;
        }

        float bulletStartX;
        float bulletStartY;

        bulletStartX = Gun.transform.position.x + 0.5f * Mathf.Cos(GunAngle);
        bulletStartY = Gun.transform.position.y + 0.5f * Mathf.Sin(GunAngle);

        GameObject tempBullet = (GameObject)Instantiate(Bullet, new Vector3(bulletStartX, bulletStartY), Bullet.transform.rotation);
        tempBullet.SetActive(true);

        //tempBullet.GetComponent<WallerBotBulletBehavior>().velocity = new Vector3(bulletSpeed * Mathf.Cos(GunAngle) + rB.velocity.x,
        //                                                                bulletSpeed * Mathf.Sin(GunAngle) + rB.velocity.y);
    }


    void followPlayer()
    {
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
    void avoidPlayer()
    {
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

        rB.AddForce(new Vector3(Mathf.Cos(playerAngle) * -speed, Mathf.Sin(playerAngle) * -speed, 0f));

    }
    //rotate Gun toward player pointer
    void rotatePeripheral()
    {
        //get angle that Gun is at relative to player
        GunXDistance = Gun.transform.position.x - transform.position.x;
        GunYDistance = Gun.transform.position.y - transform.position.y;

        //have logic based on what quadrant its in, to keep angle accurate
        if (GunXDistance >= 0 && GunYDistance >= 0)
        {
            //first quadrant
            GunAngle = Mathf.Atan(Mathf.Abs(GunYDistance / GunXDistance));
            GunQuadrant = 1;
        }
        else if (GunXDistance <= 0 && GunYDistance >= 0)
        {
            //second quadrant
            GunAngle = Mathf.Atan(Mathf.Abs(GunXDistance / GunYDistance)) + (Mathf.PI / 2f);
            GunQuadrant = 2;
        }
        else if (GunXDistance <= 0 && GunYDistance <= 0)
        {
            //third quadrant
            GunAngle = Mathf.Atan(Mathf.Abs(GunYDistance / GunXDistance)) + (Mathf.PI);
            GunQuadrant = 3;
        }
        else if (GunXDistance >= 0 && GunYDistance <= 0)
        {
            //fourth quadrant
            GunAngle = Mathf.Atan(Mathf.Abs(GunXDistance / GunYDistance)) + (3f * Mathf.PI / 2f);
            GunQuadrant = 4;
        }





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


        //different cases that need to be handled differently
        if (GunQuadrant == 1 && playerQuadrant == 4)
        {
            //clockwise

            rB.angularVelocity = -GunRotateSpeed;
            //rB.AddTorque(-GunRotateSpeed, ForceMode2D.Force);
            //GunRotator.transform.Rotate(new Vector3(0f, 0f, 1f), -GunRotateSpeed);
        }
        else if (GunQuadrant == 4 && playerQuadrant == 1)
        {
            //counter clockwise
            rB.angularVelocity = GunRotateSpeed;
            //rB.AddTorque(GunRotateSpeed, ForceMode2D.Force);
            //GunRotator.transform.Rotate(new Vector3(0f, 0f, 1f), GunRotateSpeed);
        }
        else if ((GunQuadrant == 1 && playerQuadrant == 3) || (GunQuadrant == 3 && playerQuadrant == 1))
        {
            if (Mathf.Abs(GunAngle - playerAngle) > (2 * Mathf.PI - (GunAngle) + playerAngle))
            {
                //counter clockwise
                rB.angularVelocity = GunRotateSpeed;
                //rB.AddTorque(GunRotateSpeed, ForceMode2D.Force);
            }
            else
            {
                rB.angularVelocity = -GunRotateSpeed;
                //rB.AddTorque(-GunRotateSpeed, ForceMode2D.Force);
            }
        }
        else if ((GunQuadrant == 2 && playerQuadrant == 4) || (GunQuadrant == 4 && playerQuadrant == 2))
        {
            if ((GunAngle - playerAngle) < (2 * Mathf.PI + (GunAngle) - playerAngle))
            {
                //counter clockwise
                rB.angularVelocity = GunRotateSpeed;
                //rB.AddTorque(GunRotateSpeed, ForceMode2D.Force);
            }
            else
            {
                rB.angularVelocity = -GunRotateSpeed;
                //rB.AddTorque(-GunRotateSpeed, ForceMode2D.Force);
            }
        }
        //normal case
        else
        {
            if (GunAngle - playerAngle < -GunRotationTolerance)
            {
                //counter clockwise
                rB.angularVelocity = GunRotateSpeed;
                //rB.AddTorque(GunRotateSpeed, ForceMode2D.Force);
                //GunRotator.transform.Rotate(new Vector3(0f, 0f, 1f), GunRotateSpeed);
            }
            else if (GunAngle - playerAngle > GunRotationTolerance)
            {
                //clockwise
                rB.angularVelocity = -GunRotateSpeed;
                //rB.AddTorque(-GunRotateSpeed, ForceMode2D.Force);
                //GunRotator.transform.Rotate(new Vector3(0f, 0f, 1f), -GunRotateSpeed);
            }
            else
            {
                //dead zone, stop rotation
                rB.angularVelocity *= 0.5f;
            }
        }
    }
}
