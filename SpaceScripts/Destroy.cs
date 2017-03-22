using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {
	
	public int location;
    private const int
        forward = 1,
        mid = 2,
        rear = 3;

	void OnTriggerEnter(Collider other) {
        switch (location)
        {
            case forward:
                break;
            case mid:
                if (!other.gameObject.CompareTag("Cylinder") &&
                    !other.gameObject.CompareTag("BossArena") &&
                    !other.gameObject.CompareTag("Player") &&
                    !other.gameObject.CompareTag("Player Bullet"))
                    Destroy(other.gameObject);
                break;
            case rear:
                if (other.gameObject.CompareTag("Player Bullet")) { 
                    Destroy(other.transform.parent.gameObject);
                    Destroy(other.gameObject);
                 }
                break;
		}
	}

	void OnTriggerExit(Collider other) {
        switch (location) {
            case forward:
                if (other.gameObject.CompareTag("Cylinder"))
                    GameObject.Find("Enemy Base").GetComponent<BaseController>().createNew = true;
                Destroy(other.gameObject);
                if (other.gameObject.CompareTag("Cylinder"))
                    Destroy(other.gameObject);
                break;
            case mid:
                if (!other.gameObject.CompareTag("Cylinder") &&
                    !other.gameObject.CompareTag("BossArena") &&
                    !other.gameObject.CompareTag("Player") &&
                    !other.gameObject.CompareTag("Player Bullet"))
                    Destroy(other.gameObject);
                break;
            case rear:
                if (other.gameObject.CompareTag("Player Bullet")) {
                    Destroy(other.transform.parent.gameObject);
                    Destroy(other.gameObject);
                }
            break;
        }
	}
}

