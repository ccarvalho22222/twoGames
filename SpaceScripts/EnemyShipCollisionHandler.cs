using UnityEngine;
using System.Collections;

public class EnemyShipCollisionHandler : MonoBehaviour {

    public int health;

    public GameObject explosion;
    public GameObject redOutline;
    public GameObject whiteOutline;
    public AudioClip Hit;
    public AudioClip Explosion;
    private int totalTime = 4;
    private int counter = 0;
    private bool isHit = false;

    public GameObject enemies;

    private int randChance;     
    private int randNum;

    private GameObject powerup1;
    private GameObject powerup2;
    private GameObject powerup3;
    private GameObject powerup4;

	// Use this for initialization
	void Start () {

        //seed random
        Random.InitState((int)(System.DateTime.Now.Ticks));

        enemies = GameObject.Find("Enemies");

        powerup1 = GameObject.Find("Powerup1");
        powerup2 = GameObject.Find("Powerup2");
        powerup3 = GameObject.Find("Powerup3");
        powerup4 = GameObject.Find("Powerup4");

	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isHit == true)
        {
            if (counter < totalTime)
            {
                counter++;
            }
            else if(counter >= totalTime)
            {
                whiteOutline.SetActive(false);
                redOutline.SetActive(true);
                isHit = false;
            }
        }
        if (health <= 0)
        {

            //create explosion
            AudioSource.PlayClipAtPoint(Explosion, transform.position);
            Vector3 position = new Vector3(enemies.transform.position.x, enemies.transform.position.y, transform.position.z);
            //Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Instantiate(explosion, position, enemies.transform.rotation, enemies.transform);

            //random chance to drop powerup
            randChance = Random.Range(1, 5);

            //1 in 4 chance to drop
            if(randChance == 4)
            {
                //chance for different guns to drop
                randNum = Random.Range(0, 9);
                if(randNum == 1 || randNum == 2)
                    Instantiate(powerup1, position, enemies.transform.rotation, enemies.transform);
                if(randNum == 3 || randNum == 4)
                    Instantiate(powerup2, position, enemies.transform.rotation, enemies.transform);
                if(randNum == 5 || randNum == 6)
                    Instantiate(powerup3, position, enemies.transform.rotation, enemies.transform);
                if(randNum == 7 || randNum == 8)
                    Instantiate(powerup4, position, enemies.transform.rotation, enemies.transform);
            }

            //Give points
            GameObject.Find("Level Controller").GetComponent<LevelController>().points += transform.parent.GetComponent<Points>().points;

            //kill enemy
            Destroy(transform.parent.gameObject);
            Destroy(gameObject);
            this.enabled = false;
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player Bullet"))
        {
            isHit = true;
            AudioSource.PlayClipAtPoint(Hit, transform.position);
            redOutline.SetActive(false);
            whiteOutline.SetActive(true);
            counter = 0;
            //destroy bullet so it doesnt penetrate
            Destroy(col.gameObject);

            //get bullet damage from parent
            int damage = col.transform.parent.gameObject.GetComponent<Projectile>().damage;
            
            //subtract bullet damage from enemy
            health -= damage;
        }
    }
    void OnDestroy()
    {
        Transform parentTransform = GetComponent<Transform>().parent;
        GameObject test = parentTransform.gameObject;
        Destroy(test);
    }
}
