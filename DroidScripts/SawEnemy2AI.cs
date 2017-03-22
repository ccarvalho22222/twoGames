using UnityEngine;
using System.Collections;

public class SawEnemy2AI : MonoBehaviour {
    public AudioClip enemyDash;

    public GameObject Spike;

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

    public int spinType = 1;
    public float spinSpeed;
    public float spinMax;

    private float playerXDistance;
    private float playerYDistance;

    private float playerQuadrant;
    private float playerAngle;

    // Use this for initialization
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

        xDistance = 0.0f;
        yDistance = 0.0f;
        distance = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (GetComponent<EnemyEnabledScript>().engaged)
        {

            if (!spikeDestroyed)
                rotate();

            move();
        }
        else
            brakes();
    }

    void brakes()
    {
        //brakes
        //apply extra slowdown if going diagonal and above terminal velocity
        if (Mathf.Sqrt(Mathf.Pow(rB.velocity.x, 2) + (Mathf.Pow(rB.velocity.y, 2))) > terminalVelocity)
        {
            rB.velocity = new Vector3(rB.velocity.x * 0.99f, rB.velocity.y * 0.99f, 0f);
        }
    }

    void move()
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
    
    void rotate()
    {
        if (rB.angularVelocity < spinMax)
        {
            rB.AddTorque(spinSpeed);
        }
    }
}
