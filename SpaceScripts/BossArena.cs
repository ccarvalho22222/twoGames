using UnityEngine;
using System.Collections;

public class BossArena : MonoBehaviour {

	private GameObject enemies;
	private GameObject levelController;


	void OnEnable() {
		levelController = GameObject.Find ("Level Controller");
		enemies = levelController.GetComponent<LevelController> ().enemies;
		GameObject.Find ("Player Ship").GetComponent<PlayerController>().enabled = true;
		GameObject.Find ("Player Ship").GetComponent<PlayerShooter> ().enabled = true;
		levelController.GetComponent<LevelController> ().scrollSpeed = 0.0f;
		levelController.GetComponent<LevelController> ().enemyBase = GetComponent<Rigidbody> ();
        levelController.GetComponent<LevelController>().enabled = true;
    }

	// Update is called once per frame
	void Update () {

	}
}
