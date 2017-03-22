using UnityEngine;
using System.Collections;

public class MineBossPushBulletBehaviour : MonoBehaviour {

    public Vector3 bulletVelocity;
    Rigidbody2D rB;

    public int lifeTime = 120;
    private int lifeCounter = 0;

    // Use this for initialization
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        rB.velocity = bulletVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeCounter++ > lifeTime)
            Destroy(gameObject);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.collider.CompareTag("Bullet") && !col.collider.CompareTag("Enemy") && !col.collider.CompareTag("PlayerPeripheral") && !col.collider.CompareTag("Alerter") && !col.collider.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        //if (col.collider.gameObject.CompareTag("Player"))
        //{
        //    col.gameObject.GetComponent<PlayerMovement>().hp--;
        //}
    }
}
