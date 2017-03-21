using UnityEngine;
using System.Collections;

public class SprayBotBulletBehaviour : MonoBehaviour {

    public Vector3 bulletVelocity;
    Rigidbody2D rB;

    public int lifeTime;
    private int counter = 0;

    // Use this for initialization
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        rB.velocity = bulletVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if(counter++ > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.collider.CompareTag("Bullet") && !col.collider.CompareTag("Alerter"))
        {
            Destroy(gameObject);
        }

        //if (col.collider.gameObject.CompareTag("Player"))
        //{
        //    col.gameObject.GetComponent<PlayerMovement>().hp--;
        //}
    }
}
