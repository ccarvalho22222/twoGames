using UnityEngine;
using System.Collections;

public class BossCollisionHandler3a : MonoBehaviour {

    GameObject parent;
    public GameObject redOutline;
    public GameObject whiteOutline;
    public AudioClip Hit;
    private int totalTime = 4;
    private int counter = 0;
    private bool isHit = false;

    // Use this for initialization
    void Start () {
        //parent of parent;
        parent = transform.parent.gameObject.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isHit == true)
        {
            if (counter < totalTime)
            {
                counter++;
            }
            else if (counter >= totalTime)
            {
                whiteOutline.SetActive(false);
                redOutline.SetActive(true);
                isHit = false;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player Bullet"))
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
            parent.GetComponent<BossThreeAI>().hp1 -= damage;
        }
    }
}
