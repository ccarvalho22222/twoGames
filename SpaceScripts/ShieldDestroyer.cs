using UnityEngine;
using System.Collections;

public class ShieldDestroyer : MonoBehaviour {

    public AudioClip shieldDeflect;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy Bullet"))
        {
            AudioSource.PlayClipAtPoint(shieldDeflect, transform.position);
            Destroy(col.gameObject);
        }
    }
}
