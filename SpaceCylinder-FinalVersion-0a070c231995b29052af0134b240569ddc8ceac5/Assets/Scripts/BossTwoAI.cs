using UnityEngine;
using System.Collections;

public class BossTwoAI : MonoBehaviour
{

    public int health;

    public GameObject explosion;

    public bool done = false;

    public float fireSpeed;
    public float fireDelay;
    private float fireDelayCount;

    public float defendTime;
    private float defendTimeCount;

    private int circle = 0;
    public int rotates;

    public float fastSpeed;
    public float slowSpeed;
    private float moveSpeed;

    private int goingAround;
    public GameObject shot;

    private int fightStage;

    private Transform boss;
    private Quaternion tracking0;
    private Quaternion tracking1;
    private Quaternion tracking2;
    private Quaternion tracking3;
    private const int attack = 1;
    private const int defend = 2;
    private Quaternion avoid;


    void Start()
    {
        boss = GetComponent<Transform>();
        tracking0 = Quaternion.Euler(0.0f, 0.0f, 175.0f);
        tracking1 = Quaternion.Euler(0.0f, 0.0f, 185.0f);
        tracking2 = Quaternion.Euler(0.0f, 0.0f, 195.0f);
        tracking3 = Quaternion.Euler(0.0f, 0.0f, 10.0f);
        moveSpeed = fastSpeed;
        avoid = Quaternion.Euler(0.0f, 0.0f, 30.0f);
        fightStage = 1;
    }

    void Update()
    {
        if (health <= 0)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Instantiate(explosion, position, transform.rotation, transform.parent.transform);
            done = true;
            GetComponentInChildren<MeshRenderer>().enabled = false;
            enabled = false;
        }
        switch (fightStage)
        {
            case attack:
                moveSpeed = 5f;
                // Boss will fly around the cylinder 'rotates' times fast
                switch (goingAround)
                {
                    case 0:
                        boss.rotation = Quaternion.RotateTowards(boss.rotation, tracking0, moveSpeed);
                        if (boss.rotation.eulerAngles == tracking0.eulerAngles) goingAround++;
                        break;
                    case 1:
                        boss.rotation = Quaternion.RotateTowards(boss.rotation, tracking1, moveSpeed);
                        if (boss.rotation.eulerAngles == tracking1.eulerAngles) goingAround++;
                        break;
                    case 2:
                        boss.rotation = Quaternion.RotateTowards(boss.rotation, tracking2, moveSpeed);
                        if (boss.rotation.eulerAngles == tracking2.eulerAngles) goingAround++;
                        break;
                    case 3:
                        boss.rotation = Quaternion.RotateTowards(boss.rotation, tracking3, moveSpeed);
                        if (boss.rotation.eulerAngles == tracking3.eulerAngles)
                        {
                            goingAround = 0;
                            circle++;
                        }
                        break;
                }

                if (Quaternion.Angle(boss.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f)) < 40.0f)
                {
                    if (Time.time > fireDelayCount)
                    {
                        GameObject tempShot = (GameObject)Instantiate(shot, boss.position, boss.rotation);
                        tempShot.GetComponent<Projectile>().speed = fireSpeed * -1.0f;
                        fireDelayCount = Time.time + fireDelay;
                    }
                    if (circle == rotates)
                    {
                        fightStage = defend;
                        defendTimeCount = defendTime + Time.time;
                    }
                }

                break;
            case defend:
                if (Time.time < defendTimeCount)
                {
                    if (boss.rotation.eulerAngles.z > 180)
                    {
                        avoid = Quaternion.Euler(0.0f, 0.0f, -179.0f);
                    }
                    else
                    {
                        avoid = Quaternion.Euler(0.0f, 0.0f, 179.0f);
                    }
                    boss.rotation = Quaternion.RotateTowards(boss.rotation, avoid, slowSpeed);
                }
                else
                {
                    print(boss.rotation.eulerAngles.z);
                    fightStage = attack;
                    circle = 0;
                    if (boss.rotation.eulerAngles.z > 200.0f)
                    {
                        goingAround = 3;
                        circle = -1;
                    }
                }
                break;
        }
    }
}
