using UnityEngine;
using System.Collections;

public class AttachShip : MonoBehaviour {

    public GameObject capsule;
    public GameObject enemyBase;
    public GameObject mainCamera;
    public GameObject cylinder;
    public GameObject playerShip;
    public GameObject destroyers;

    public GameObject DumbShip;

    private GameObject bossArena;
    private Vector3 variable;
    private Vector3 baseLocation;
    private GameObject player;


    private Vector3 cameraLocation;
    private Quaternion cameraAngle;
    private Vector3 playerPosition;

    private int stage = 0;

    // Use this for initialization
    void Start () {
        mainCamera.transform.position = new Vector3(25f, 0.0f, 8f);
        mainCamera.transform.rotation = Quaternion.Euler(0.0f, -110, 0.0f);
        cameraAngle = Quaternion.Euler(0.0f, -70.0f, 0f);
        cameraLocation = new Vector3(4.86f, 0.0f, -2f);
        baseLocation = new Vector3(enemyBase.transform.position.x, enemyBase.transform.position.y, enemyBase.transform.position.z + 20);


        Instantiate(cylinder, baseLocation, enemyBase.transform.rotation, enemyBase.transform);

        //spawn enemies for first cylinder
        GameObject tempShip;
        Instantiate(DumbShip, new Vector3(enemyBase.transform.position.x, enemyBase.transform.position.y, 50f), GameObject.Find("Enemies").transform.rotation, GameObject.Find("Enemies").transform);

        tempShip = (GameObject)Instantiate(DumbShip, new Vector3(enemyBase.transform.position.x, enemyBase.transform.position.y, 55f), GameObject.Find("Enemies").transform.rotation, GameObject.Find("Enemies").transform);
        tempShip.GetComponent<DumbShipAI>().level = 2;

        tempShip = (GameObject)Instantiate(DumbShip, new Vector3(enemyBase.transform.position.x, enemyBase.transform.position.y, 65f), GameObject.Find("Enemies").transform.rotation, GameObject.Find("Enemies").transform);
        tempShip.GetComponent<DumbShipAI>().level = 3;




        variable = new Vector3(enemyBase.transform.position.x, enemyBase.transform.position.y, enemyBase.transform.position.z - 40);

        bossArena = (GameObject)Instantiate(capsule, variable, enemyBase.transform.rotation, enemyBase.transform);
        //player = (GameObject)Instantiate(playerShip, variable, bossArena.transform.rotation, bossArena.transform);

        playerShip.transform.parent = bossArena.transform;
        playerShip.transform.localPosition = new Vector3(.52f, -0.85f, 0f);

        variable = new Vector3(
          enemyBase.transform.position.x,
          enemyBase.transform.position.y,
          enemyBase.transform.position.z + 5
        );
    }

    // Update is called once per frame
    void Update () {

        switch (stage)
        {
            case 0:
                bossArena.transform.position = Vector3.MoveTowards(bossArena.transform.position, variable, 0.25f);//     bossArena.transform.position = new Vector3 (bossArena.transform.position.x, bossArena.transform.position.y, bossArena.transform.position.z + 1.0f);
                if (bossArena.transform.position == variable) stage = 1;
                break;
            case 1:
                mainCamera.transform.rotation = Quaternion.RotateTowards(mainCamera.transform.rotation, cameraAngle, .2f);
                mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, cameraLocation, 0.3f);
                if (mainCamera.transform.rotation == cameraAngle &&
                    mainCamera.transform.position == cameraLocation)
                    stage = 2;
                break;
            case 2:
                stage = 3;
                break;
            case 3:
                //GameObject.Find("Enemy Base").GetComponent<BaseController>().enabled = true;
                //enemyBase.GetComponent<BaseController>().enabled = true;
                playerShip.transform.parent = null;// enemyBase.transform;
                playerShip.GetComponent<PlayerController>().enabled = true;
                playerShip.GetComponent<PlayerShooter>().enabled = true;
                GameObject.Find("Level Controller").GetComponent<LevelController>().enabled = true;
                //GameObject.Find("The Destroyers").GetComponent<GameObject>().SetActive(true);
                destroyers.SetActive(true);
                enabled = false;
                break;
        }
    }
}
