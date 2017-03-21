using UnityEngine;
using System.Collections;

public class EnemyBehaviourScript : MonoBehaviour {

    public float speed = -0.25f;
    public float terminalVelocity;
    public float engageDistance = 10.0f;

    public bool engaged = false;

    GameObject player;// = GameObject.Find("Player");
    Vector3 playerPosition;// = player.transform.position;

    Rigidbody2D rB;// = GetComponent<Rigidbody2D>();

    float xDistance;// = 0.0f;
    float yDistance;// = 0.0f;
    float distance;// = 0.0f;


    // Use this for initialization
    void Start () {

        rB = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

        xDistance = 0.0f;
        yDistance = 0.0f;
        distance = 0.0f;

    }
	
	// Update is called once per frame
	void Update () {
        
        //bool tempEngaged = GetComponent<EnemyEnabledScript>().engaged;

        //if (GetComponent<EnemyEnabledScript>().engaged)
        //{
        //    engaged = true;
        //}
        //else
        //{
        //    engaged = false;
        //}

        //var player = GameObject.Find("Player");
        //var playerPosition = player.transform.position;

        //var rB = GetComponent<Rigidbody2D>();

        //float xDistance = 0.0f;
        //float yDistance = 0.0f;
        //float distance = 0.0f;

        //xDistance = transform.position.x - playerPosition.x;
        //yDistance = transform.position.y - playerPosition.y;

        //distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));

        //if(distance < engageDistance)
        //{
        //    engaged = true;
        //}
        if(engaged)
        {
            
            playerPosition = player.transform.position;

            

            

            xDistance = transform.position.x - playerPosition.x;
            yDistance = transform.position.y - playerPosition.y;

            //add extra force if turning around toward player
            if (-Mathf.Sign(xDistance) != Mathf.Sign(rB.velocity.x))
            {
                rB.AddForce(new Vector3(xDistance * 2f * speed, 0f, 0.0f));
            }
            if (-Mathf.Sign(yDistance) != Mathf.Sign(rB.velocity.y))
            {
                rB.AddForce(new Vector3(0f, yDistance * 2f * speed, 0.0f));
            }
            rB.AddForce(new Vector3(xDistance * speed, yDistance * speed, 0.0f));
        }

        //brakes
        //apply extra slowdown if going diagonal and above terminal velocity
        if (Mathf.Sqrt(Mathf.Pow(rB.velocity.x, 2) + (Mathf.Pow(rB.velocity.y, 2))) > terminalVelocity)
        {
            rB.velocity = new Vector3(rB.velocity.x * 0.99f, rB.velocity.y * 0.99f, 0f);
        }

    }    
}
