using UnityEngine;
using System.Collections;

public class PlayerBulletDestroyer : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player Bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
