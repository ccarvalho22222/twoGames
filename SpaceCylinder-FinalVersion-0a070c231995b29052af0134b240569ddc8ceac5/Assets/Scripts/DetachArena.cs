using UnityEngine;
using System.Collections;

public class DetachArena : MonoBehaviour {

	private Transform cylinder;

	private Transform mainCamera;
	private Transform player;

	public float startCameraRotate;
	public float cameraRotateSpeed;
	private bool pointed = false;

	public float startCameraReposition;
	public float cameraRepositionSpeed;
	private bool placed = false;

	public float startPlayerReposition;
	public float playerRepositionSpeed;
	private bool repositioned = false;

	private Vector3 cameraLocation;
	private Quaternion cameraAngle;
	private Vector3 playerPosition;


	private bool detached = false;
	private bool playerDisabled = false;

	void Start () {
		mainCamera = GameObject.Find ("Main Camera").GetComponent<Transform> ().transform;
		player = GameObject.Find ("Level Controller").GetComponent<LevelController>().player.transform;
		cylinder = GetComponent<Transform> ();
		cameraLocation = new Vector3(7.0f, 0.0f, 0.0f);
		cameraAngle = Quaternion.Euler (0.0f, -90.0f, 0.0f);
	}
	
	void Update () {

		// Rotate the camera to 90 degrees
		if (cylinder.position.z < startCameraRotate) {
			mainCamera.rotation = Quaternion.RotateTowards(mainCamera.rotation, cameraAngle, cameraRotateSpeed* Time.deltaTime);
			if (mainCamera.rotation == cameraAngle)
				pointed = true;
		}

		// Move camera position to center capsule
		if (cylinder.position.z < startCameraReposition) {
			mainCamera.position = Vector3.MoveTowards (mainCamera.position, cameraLocation, cameraRepositionSpeed * Time.deltaTime);
			if (mainCamera.position == cameraLocation)
				placed = true;
		}

		// Disable player control and move ship to center
		if (cylinder.position.z < startPlayerReposition) {
			if (!playerDisabled) {
				GameObject.Find ("Player Ship").GetComponent<PlayerController> ().enabled = false;
				GameObject.Find ("Player Ship").GetComponent<PlayerShooter> ().enabled = false;
				GameObject.Find ("Level Controller").GetComponent<LevelController> ().enabled = false;
                GameObject.Find("Player Ship").GetComponent<Rigidbody>().velocity = Vector3.zero;
				playerDisabled = true;
			}
			playerPosition = new Vector3 (player.position.x, 0.0f, cylinder.position.z - 3.0f);
			player.position = Vector3.MoveTowards (player.position, playerPosition, playerRepositionSpeed * Time.deltaTime);
            
			if (player.position == playerPosition)
				repositioned = true;
		}

		if (cylinder.position.z <= 0.0f) {
			cylinder.parent = null;
			detached = true;
		}

		if (detached && pointed && placed && repositioned) {
			GetComponent<BossEntrance> ().enabled = true;
			enabled = false;
		}
}
}
