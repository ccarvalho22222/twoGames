using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelController : MonoBehaviour {

	public float scrollSpeed;
	public GameObject player;
	public Rigidbody enemyBase;
	public GameObject skyBoxCamera;
	public GameObject enemies;
	public GameObject projectiles;
	public Transform cylinder;
	public int baseLength;
    public int currentLevel;

	private float moveHorizontal;
	private float moveVertical;
	public float shipSpeed;
    public float playerSpeed;
	public Rigidbody playerBody;

//	private Vector3 variable;
	private Vector3 midPosition;

    public bool boss = false;

    //[System.NonSerialized]
    public bool keysEnabled = true;

    public Texture2D healthTexture;
    public Texture2D overheatTexture;

    public int points;
    public Text pointsText;

    //public GUIStyle style;

    

    void Start () {
		playerBody = player.GetComponent<Rigidbody>();
        playerSpeed = player.GetComponent<PlayerController>().speed * -1.0f;
        shipSpeed = playerSpeed * 0.35f;

        pointsText.text = "Points: " + points;
        
	}

	void Update()
	{
        pointsText.text = "Points: " + points;

        if (keysEnabled) {
			moveHorizontal = Input.GetAxis ("Horizontal");

            float playerZSpeed = 0;
            if (!boss)
            {
                if (moveHorizontal > 0)
                {
                    playerZSpeed = moveHorizontal * -1f;
                }
                if (moveHorizontal < 0)
                {
                    playerZSpeed = moveHorizontal * -2f;
                }
            }
            else
            {
                playerZSpeed = moveHorizontal * -1f;
            }

            moveVertical = Input.GetAxis ("Vertical");

			Vector3 playerMovement = new Vector3 (0.0f, /*moveVertical* -1.0f*/0.0f, playerZSpeed);
			Vector3 movement = new Vector3 (0.0f, 0.0f, scrollSpeed);

			enemyBase.velocity = movement;
			playerBody.velocity = playerMovement * playerSpeed;

			//if (player.transform.position.y > 0.5f || player.transform.position.y < -0.5f) {
				enemyBase.transform.Rotate (Vector3.up * shipSpeed * moveVertical); 
				skyBoxCamera.transform.Rotate (Vector3.left * shipSpeed * moveVertical);
		
				foreach (Transform child  in enemies.transform) {
					child.Rotate (Vector3.forward * shipSpeed * moveVertical);
				}
				foreach (Transform child  in projectiles.transform) {
					child.Rotate (Vector3.forward * shipSpeed * moveVertical);
				}
			//}
		}
        
        
    }

    void OnGUI()
    {
        //Rect rect = new Rect(0, 0, Screen.width, Screen.height);
        //GUI.Label(rect, "Health: " + player.GetComponent<PlayerController>().health
        //               + "\nInvicible? " + player.GetComponent<PlayerController>().invicible
        //               + "\nInvicible Time: " + player.GetComponent<PlayerController>().invicibleCounter
        //               + "\n\nWeapon Heat: " + player.GetComponent<PlayerShooter>().weaponHeat
        //               + "\nOverheat Threshold: " + player.GetComponent<PlayerShooter>().maxOverheat
        //               + "\nIs OverHeated??: " + player.GetComponent<PlayerShooter>().overheated);

        //GUIContent pointText = new GUIContent("Points: " + points);

        //style.Draw(new Rect((Screen.width / 2), 5, 100, 50), pointText, 0, true);

        for (int i = 0; i < player.GetComponent<PlayerController>().health; i++)
        {

            GUI.Label(new Rect(10 + (healthTexture.width * i), 5, healthTexture.width, healthTexture.height), healthTexture);
        }
    }
}
