using UnityEngine;
using System.Collections;

public class PickupBehavior : MonoBehaviour {

    public GameObject player;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update ()    {
        player = GameObject.Find("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerMovement>().upgradePoints += 1;
            Destroy(gameObject);
        }
    }
}
