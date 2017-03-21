using UnityEngine;
using System.Collections;

public class BaseController : MonoBehaviour
{

    public GameObject cylinder;
    public GameObject bossArena;

    private Transform self;

    public Vector3 variable;

    public int cylinderCount = 1;

    [System.NonSerialized]
    public bool createNew = false;

    private bool bossFight = false;

    private bool createEnemies = false;

    public GameObject gunnerTower;
    public GameObject dumbShip;
    public GameObject rotatorShip;
    public GameObject smartShip;

    public GameObject enemies;

    private GameObject tempGunner;
    private GameObject tempDumb;
    private GameObject tempRotator;
    private GameObject tempSmart;
    private GameObject tempCylinder;
    private GameObject tempBoss;

    private int baseLength;

    // Use this for initialization
    void Start()
    {
 
        //seed RNG
        Random.InitState((int)(System.DateTime.Now.Ticks));

        baseLength = GameObject.Find("Level Controller").GetComponent<LevelController>().baseLength;

        self = GetComponent<Transform>();

        variable = new Vector3(
            self.position.x,
            self.position.y,
            self.position.z
        );

        cylinderCount +=2;

        createEnemies = true;
    }

    // Update is called once per frame
    void Update()
    {

        //when new cylinder is created
        if (createNew && !bossFight)
        {
            if (cylinderCount < baseLength)
            {
                createCylinder();
            }
            else
            {
                // Transition to Boss Fight
                float zLocation = self.position.z + (cylinderCount - 1) * cylinder.transform.localScale.y * self.localScale.z * 2;
                zLocation += 15f;//(bossTemplate.transform.localScale.y * self.localScale.z * 2);
                cylinderCount++;

                variable = new Vector3(
                    self.position.x,
                    self.position.y,
                    zLocation
                );
                tempBoss = (GameObject)Instantiate(bossArena, variable, self.rotation, self.transform);
                tempBoss.transform.position = variable;
                tempBoss.GetComponent<DetachArena>().enabled = true;
                tempBoss.SetActive(true);

                createEnemies = false;
                createNew = false;
                bossFight = true;
            }
        }
    }

    void createCylinder()
    {

        //create cylinder
        float zLocation = self.position.z + cylinderCount++ * cylinder.transform.localScale.y * self.localScale.z * 2;
        variable = new Vector3(
                self.position.x,
                self.position.y,
                zLocation
            );
        tempCylinder = (GameObject)Instantiate(cylinder, variable, self.rotation, self.transform);

        if (createEnemies)
        {
            spawnEnemies(zLocation);
        }
        createNew = false;
    }

    public void spawnEnemies(float location)
    {
        //Spawn Enemies
        float randomZ = 0f;
        float randomRotation = 0f;

        float minZ = location - self.localScale.z * cylinder.transform.localScale.y;
        float maxZ = location + self.localScale.z * cylinder.transform.localScale.y;

        float zRange = maxZ - minZ;

        int currentLevel = GameObject.Find("Level Controller").GetComponent<LevelController>().currentLevel;
        float minRotationRange = 0;
        float maxRotationRange = 0;
        float minZRange = 0;
        float maxZRange = 0;

        float rotationTotal = 0;
        float zTotal = 0;

        //spawn 2-5 gunner towers
        if (currentLevel == 1)
        {
            minRotationRange = 30f;
            maxRotationRange = 60f;
        }
        if (currentLevel == 2)
        {
            minRotationRange = 60f;
            maxRotationRange = 90f;
        }
        if (currentLevel == 3)
        {
            minRotationRange = 120f;
            maxRotationRange = 160f;
        }
        while (rotationTotal < 360)
        {
            randomZ = Random.Range(minZ, maxZ);
            randomRotation = Random.Range(minRotationRange, maxRotationRange);

            rotationTotal += randomRotation;

            Vector3 position = new Vector3(-5.5f, 0.0f, randomZ);
            tempGunner = (GameObject)Instantiate(gunnerTower, position, enemies.transform.rotation, enemies.transform);
            tempGunner.transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), rotationTotal);

            if (currentLevel == 1)
            {
                tempGunner.GetComponent<StationaryGunnerAI>().level = Random.Range(1, 3);
            }
            if (currentLevel == 2)
            {
                tempGunner.GetComponent<StationaryGunnerAI>().level = Random.Range(2, 4);
            }
            if (currentLevel == 3)
            {
                tempGunner.GetComponent<StationaryGunnerAI>().level = Random.Range(4, 6);
            }
        }


        //Spawn 2-5 dumb ships
        for (int i = Random.Range(10, 20); i > 0; i--)
        {
            randomZ = Random.Range(minZ, maxZ);
            randomRotation = Random.Range(0f, 360f);
            Vector3 position = new Vector3(-5.5f, 0.0f, randomZ);
            tempDumb = (GameObject)Instantiate(dumbShip, position, enemies.transform.rotation, enemies.transform);
            tempDumb.transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), randomRotation);

            if (currentLevel == 1)
            {
                tempDumb.GetComponent<DumbShipAI>().level = Random.Range(1, 3);
            }
            if (currentLevel == 2)
            {
                tempDumb.GetComponent<DumbShipAI>().level = Random.Range(2, 4);
            }
            if (currentLevel == 3)
            {
                tempDumb.GetComponent<DumbShipAI>().level = Random.Range(3, 5);
            }
        }


        //Spawn 1 smart ships
        for (int i = 0; i < 1; i++)
        {

            randomRotation = Random.Range(0f, 360f);
            Vector3 position = new Vector3(-5.5f, 0.0f, randomZ);
            tempSmart = (GameObject)Instantiate(smartShip, position, enemies.transform.rotation, enemies.transform);
            tempSmart.transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), randomRotation);

            if (currentLevel == 1)
            {
                tempSmart.GetComponent<SmartEnemyMovement>().level = Random.Range(1, 3);
            }
            if (currentLevel == 2)
            {
                tempSmart.GetComponent<SmartEnemyMovement>().level = Random.Range(2, 4);
            }
            if (currentLevel == 3)
            {
                tempSmart.GetComponent<SmartEnemyMovement>().level = Random.Range(3, 5);
            }
        }

        //spawn ships a certain distance away from eachother
        zRange = maxZ - minZ;
        minZRange = minZ + (zRange / 4f);
        maxZRange = minZ + (zRange / 2f);

        zTotal = 0;



        while (zTotal < (zRange - (zRange / 8f)))
        {
            randomZ = Random.Range(minZRange, maxZRange);
            zTotal += randomZ - minZ;
            minZRange += zTotal;

            if (zTotal > (zRange - (zRange / 8f)))
            {
                break;
            }
            Vector3 position = new Vector3(-5.5f, 0.0f, randomZ);

            randomRotation = Random.Range(0f, 360f);

            tempRotator = (GameObject)Instantiate(rotatorShip, position, enemies.transform.rotation, enemies.transform);
            tempRotator.transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), randomRotation);
        }
    }
}
