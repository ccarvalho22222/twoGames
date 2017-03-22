using UnityEngine;
using System.Collections;

public class DoorActivator : MonoBehaviour
{
    public GameObject doors;
    bool player = false;
    bool enemy = false;
    int enemyCount = 0;
    int playerCount;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemyCount--;
        playerCount--;
        if(enemyCount == 0)
        {
            enemy = false;
        }
        if (playerCount == 0)
        {
            player = false;
        }
        if (player == true && enemy == true)
        {
            doors.SetActive(true);
        }
        else
        {
            doors.SetActive(false);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DoorActivator"))
        {
            player = true;
            playerCount = 3;
        }
        if(other.gameObject.CompareTag("eDoor"))
        {
            enemy = true;
            enemyCount = 3;
        }
    }
}