using UnityEngine;
using System.Collections;

public class LargeEnemyBehaviourScript : MonoBehaviour {

    float speed = -1f;
    float engageDistance = 10.0f;

    public bool engaged = false;

    public float attackDistance;
    
    private Rigidbody2D rB;

    private float bucketXDistance;
    private float bucketYDistance;
    private float playerXDistance;
    private float playerYDistance;

    private float bucketQuadrant;
    private float playerQuadrant;

    private float bucketAngle;
    private float playerAngle;

    public int spinType = 1;
    public float spinSpeed;
    public float spinMax;

    public float bucketRotateSpeed;
    public float bucketRotationTolerance;


    // Use this for initialization
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<EnemyEnabledScript>().engaged)
        {
            engaged = true;
        }

        var player = GameObject.Find("Player");
        var playerPosition = player.transform.position;

        

        float xDistance = 0.0f;
        float yDistance = 0.0f;
        float distance = 0.0f;
        
        xDistance = transform.position.x - playerPosition.x;
        yDistance = transform.position.y - playerPosition.y;

        distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));

        if (engaged)
        {
            
            rB.AddForce(new Vector3(xDistance * speed, yDistance * speed, 0.0f));
            

            rotate();
        }

    }

    void rotate()
    {
        if (spinType == 1)
        {
            rB.AddTorque(spinSpeed);
            if (rB.angularVelocity > spinMax)
            {
                spinType *= -1;
            }
        }
            
        else if (spinType == -1)
        {
            rB.AddTorque(-spinSpeed);
            if (rB.angularVelocity < -spinMax)
            {
                spinType *= -1;
            }
        }
            

        
    }

    void swingBuckets(GameObject bucket)
    {
        //get angle that bucket is at relative to player
        bucketXDistance = bucket.GetComponent<periph>().peripheral.transform.position.x - transform.position.x;
        bucketYDistance = bucket.GetComponent<periph>().peripheral.transform.position.y - transform.position.y;

        //have logic based on what quadrant its in, to keep angle accurate
        if (bucketXDistance >= 0 && bucketYDistance >= 0)
        {
            //first quadrant
            bucketAngle = Mathf.Atan(Mathf.Abs(bucketYDistance / bucketXDistance));
            bucketQuadrant = 1;
        }
        else if (bucketXDistance <= 0 && bucketYDistance >= 0)
        {
            //second quadrant
            bucketAngle = Mathf.Atan(Mathf.Abs(bucketXDistance / bucketYDistance)) + (Mathf.PI / 2f);
            bucketQuadrant = 2;
        }
        else if (bucketXDistance <= 0 && bucketYDistance <= 0)
        {
            //third quadrant
            bucketAngle = Mathf.Atan(Mathf.Abs(bucketYDistance / bucketXDistance)) + (Mathf.PI);
            bucketQuadrant = 3;
        }
        else if (bucketXDistance >= 0 && bucketYDistance <= 0)
        {
            //fourth quadrant
            bucketAngle = Mathf.Atan(Mathf.Abs(bucketXDistance / bucketYDistance)) + (3f * Mathf.PI / 2f);
            bucketQuadrant = 4;
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
        if (bucketQuadrant == 1 && playerQuadrant == 4)
        {
            //clockwise

            bucket.GetComponent<Rigidbody2D>().angularVelocity = -bucketRotateSpeed;
            //bucket.AddTorque(-bucketRotateSpeed, ForceMode2D.Force);
            //bucketRotator.transform.Rotate(new Vector3(0f, 0f, 1f), -bucketRotateSpeed);
        }
        else if (bucketQuadrant == 4 && playerQuadrant == 1)
        {
            //counter clockwise
            bucket.GetComponent<Rigidbody2D>().angularVelocity = bucketRotateSpeed;
            //bucket.AddTorque(bucketRotateSpeed, ForceMode2D.Force);
            //bucketRotator.transform.Rotate(new Vector3(0f, 0f, 1f), bucketRotateSpeed);
        }
        else if ((bucketQuadrant == 1 && playerQuadrant == 3) || (bucketQuadrant == 3 && playerQuadrant == 1))
        {
            if (Mathf.Abs(bucketAngle - playerAngle) > (2 * Mathf.PI - (bucketAngle) + playerAngle))
            {
                //counter clockwise
                bucket.GetComponent<Rigidbody2D>().angularVelocity = bucketRotateSpeed;
                //bucket.AddTorque(bucketRotateSpeed, ForceMode2D.Force);
            }
            else
            {
                bucket.GetComponent<Rigidbody2D>().angularVelocity = -bucketRotateSpeed;
                //bucket.AddTorque(-bucketRotateSpeed, ForceMode2D.Force);
            }
        }
        else if ((bucketQuadrant == 2 && playerQuadrant == 4) || (bucketQuadrant == 4 && playerQuadrant == 2))
        {
            if ((bucketAngle - playerAngle) < (2 * Mathf.PI + (bucketAngle) - playerAngle))
            {
                //counter clockwise
                bucket.GetComponent<Rigidbody2D>().angularVelocity = bucketRotateSpeed;
                //bucket.AddTorque(bucketRotateSpeed, ForceMode2D.Force);
            }
            else
            {
                bucket.GetComponent<Rigidbody2D>().angularVelocity = -bucketRotateSpeed;
                //bucket.AddTorque(-bucketRotateSpeed, ForceMode2D.Force);
            }
        }
        //normal case
        else
        {
            if (bucketAngle - playerAngle < -bucketRotationTolerance)
            {
                //counter clockwise
                bucket.GetComponent<Rigidbody2D>().angularVelocity = bucketRotateSpeed;
                //bucket.AddTorque(bucketRotateSpeed, ForceMode2D.Force);
                //bucketRotator.transform.Rotate(new Vector3(0f, 0f, 1f), bucketRotateSpeed);
            }
            else if (bucketAngle - playerAngle > bucketRotationTolerance)
            {
                //clockwise
                bucket.GetComponent<Rigidbody2D>().angularVelocity = -bucketRotateSpeed;
                //bucket.AddTorque(-bucketRotateSpeed, ForceMode2D.Force);
                //bucketRotator.transform.Rotate(new Vector3(0f, 0f, 1f), -bucketRotateSpeed);
            }
            else
            {
                //dead zone, stop rotation
                bucket.GetComponent<Rigidbody2D>().angularVelocity *= 0.5f;
            }
        }
    }
}
