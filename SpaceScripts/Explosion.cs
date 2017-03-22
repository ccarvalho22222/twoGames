using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    public int explosionTime;
    private int counter = 0;

    public float speed;

	// Use this for initialization
	void Start () {
        explosionTime = explosionTime * 60;
	}
	
	// Update is called once per frame
	void Update () {

        transform.localScale = new Vector3(transform.localScale.x - speed, transform.localScale.y - speed, transform.localScale.z - speed);
        //GetComponent<ExplosionMat>()._alpha -= 0.01f;
        if(counter++ > explosionTime)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }

        GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, -2f);
	}
}
