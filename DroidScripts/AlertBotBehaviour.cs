using UnityEngine;
using System.Collections;

public class AlertBotBehaviour : MonoBehaviour {

    public bool engaged = false;

    GameObject player;// = GameObject.Find("Player");
    Vector3 playerPosition;// = player.transform.position;

    private Rigidbody2D rB;// = GetComponent<Rigidbody2D>();

    float xDistance;// = 0.0f;
    float yDistance;// = 0.0f;
    float distance;// = 0.0f;

    public float speed = -0.25f;
    public float terminalVelocity;

    
    public Vector3 patrolSpotOne;
    public Vector3 patrolSpotTwo;
    public Vector3 runSpot;
    public int patrolDest;

    public float patrolSpeed;

    public float xDist;
    public float yDist;

    private float patrolAngle;
    private int patrolQuadrant;

    // Use this for initialization
    void Start () {
        rB = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

        patrolDest = 1;
        xDist = transform.position.x - patrolSpotOne.x;
        yDist = transform.position.y - patrolSpotOne.y;
    }
	
	// Update is called once per frame
	void Update () {
        //if not engaged patrol instead
        if(!GetComponent<EnemyEnabledScript>().engaged)
        {
            if(patrolDest == 1)
            {
                setVelocity();
                
                ////if it gets to its spot, switch spots
                if ((Mathf.Abs(patrolSpotOne.x - transform.position.x) <= (0.5f))
                    && (Mathf.Abs(patrolSpotOne.y - transform.position.y) <= (0.5f)))
                {
                    xDist = transform.position.x - patrolSpotTwo.x;
                    yDist = transform.position.y - patrolSpotTwo.y;
                    patrolDest = 2;
                }
            }
            if(patrolDest == 2)
            {
                setVelocity();

                //if it gets to its spot, switch spots
                if ((Mathf.Abs(patrolSpotTwo.x - transform.position.x) <= (1f))
                    && (Mathf.Abs(patrolSpotTwo.y - transform.position.y) <= (1f)))
                {
                    xDist = transform.position.x - patrolSpotOne.x;
                    yDist = transform.position.y - patrolSpotOne.y;
                    patrolDest = 1;
                }
            }
        }
        else if (GetComponent<EnemyEnabledScript>().engaged)//once engaged, run away from player trying to alert enemies
        {
            xDist = transform.position.x - runSpot.x;
            yDist = transform.position.y - runSpot.y;
            setVelocity();
        }
	
	}

    void setVelocity()
    {
        //have logic based on what quadrant its in, to keep angle accurate
        if (xDist >= 0 && yDist >= 0)
        {
            //first quadrant
            patrolAngle = Mathf.Atan(Mathf.Abs(yDist / xDist));
            patrolQuadrant = 1;

        }
        else if (xDist <= 0 && yDist >= 0)
        {
            //second quadrant
            patrolAngle = Mathf.Atan(Mathf.Abs(xDist / yDist)) + (Mathf.PI / 2f);
            patrolQuadrant = 2;
        }
        else if (xDist <= 0 && yDist <= 0)
        {
            //third quadrant
            patrolAngle = Mathf.Atan(Mathf.Abs(yDist / xDist)) + (Mathf.PI);
            patrolQuadrant = 3;
        }
        else if (xDist >= 0 && yDist <= 0)
        {
            //fourth quadrant
            patrolAngle = Mathf.Atan(Mathf.Abs(xDist / yDist)) + (3f * Mathf.PI / 2f);
            patrolQuadrant = 4;
        }

        rB.velocity = new Vector3(Mathf.Cos(patrolAngle) * -patrolSpeed, Mathf.Sin(patrolAngle) * -patrolSpeed, 0f);
    }
}
