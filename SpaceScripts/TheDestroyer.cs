using UnityEngine;
using System.Collections;

public class TheDestroyer : MonoBehaviour {

	public GameObject cylinder;
	public GameObject enemyBase;

	private Vector3 wallPosition;

	void Start() {
		wallPosition = new Vector3 (
			               0.0f,
			               0.0f,
			               cylinder.transform.localScale.y * enemyBase.transform.localScale.y * -1.0f
		               );
		GameObject.Find ("Forward Destroyer").transform.position = wallPosition;
	}
}
