using UnityEngine;
using System.Collections;

public class BossEntrance : MonoBehaviour {

    public GameObject boss1;
    public GameObject boss2;
    public GameObject boss3;

    private Vector3 tempPos;

    public GameObject mainCamera;

    public float rotateSpeed;

	private GameObject enemies;
	private Quaternion bossStart;
	private Quaternion bossEnd;
    private int currentLevel;
    private GameObject boss;

    public bool doneEntering = false;
    public bool endLevel = false;

	// Use this for initialization
	void Start () {

        mainCamera = GameObject.Find("Main Camera");
//        player = GameObject.Find("Player Ship");
        currentLevel = GameObject.Find("Level Controller").GetComponent<LevelController>().currentLevel;

        enemies = GameObject.Find ("Enemy Base").GetComponent<BaseController> ().enemies;
		Vector3 position = new Vector3(-5.5f, 0.0f, 3.0f);
		bossStart = Quaternion.Euler (0.0f, 0.0f, 260.0f);
		bossEnd = Quaternion.Euler (0.0f, 0.0f, 0.0f);
        boss = (GameObject) Instantiate ((currentLevel == 1) ? boss1 : (currentLevel == 2) ? boss2 : boss3, position, bossStart, enemies.transform);
    }
	
	// Update is called once per frame
	void Update () {
        if (!doneEntering)
        {
            GameObject.Find("Level Controller").GetComponent<LevelController>().boss = true;
            boss.transform.rotation = Quaternion.RotateTowards(boss.transform.rotation, bossEnd, rotateSpeed);

            if (boss.transform.rotation == bossEnd)
            {
                switch (currentLevel)
                {
                    case 1:
                        boss.GetComponent<BossOneAI>().enabled = true;
                        boss.GetComponent<GenericRotation>().enabled = true;
                        break;
                    case 2:
                        boss.GetComponent<BossTwoAI>().enabled = true;
                        break;
                    case 3:
                        boss.GetComponent<BossThreeAI>().enabled = true;
                        break;
                }
                GetComponent<BossArena>().enabled = true;
                doneEntering = true;
            }
        }
        else if (endLevel == false)
        {
            switch(currentLevel)
            {
                case 1:
                    if(boss.GetComponent<BossOneAI>().done == true)
                    {
                        GameObject.Find("Player Ship").GetComponent<PlayerController>().enabled = false;
                        GameObject.Find("Player Ship").GetComponent<PlayerShooter>().enabled = false;
                        GameObject.Find("Level Controller").SetActive(false);
                        endLevel = true;
                        tempPos = new Vector3(mainCamera.transform.position.x,
                                                mainCamera.transform.position.y,
                                                mainCamera.transform.position.z - 30f
                                        );
                    }
                    break;
                case 2:
                    if (boss.GetComponent<BossTwoAI>().done == true)
                    {
                        GameObject.Find("Player Ship").GetComponent<PlayerController>().enabled = false;
                        GameObject.Find("Player Ship").GetComponent<PlayerShooter>().enabled = false;
                        GameObject.Find("Level Controller").SetActive(false);
                        endLevel = true;
                        tempPos = new Vector3(mainCamera.transform.position.x,
                                                mainCamera.transform.position.y,
                                                mainCamera.transform.position.z - 30f
                                        );
                    }
                    break;
                case 3:
                    if (boss.GetComponent<BossThreeAI>().done == true)
                    {
                        GameObject.Find("Player Ship").GetComponent<PlayerController>().enabled = false;
                        GameObject.Find("Player Ship").GetComponent<PlayerShooter>().enabled = false;
                        GameObject.Find("Level Controller").SetActive(false);
                        endLevel = true;
                        tempPos = new Vector3(mainCamera.transform.position.x,
                                                mainCamera.transform.position.y,
                                                mainCamera.transform.position.z - 30f
                                        );
                    }
                    break;
            }
        }
        
        if(endLevel)
        {
            mainCamera.GetComponent<FadeOut>().fade = true;




            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, tempPos, 0.05f);
            if(mainCamera.transform.position == tempPos)
            {
                //restart scene
                
                print("LEVEL IS DONE");
            }
        }
    }
}
