using UnityEngine;
using System.Collections;

public class RoomActivator : MonoBehaviour {

    public GameObject Room;
    GameObject tempRoom;
    bool contact = false;
    int timer;
    int cap = 0;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.timeScale == 1)
        {
            timer--;
        }

        if(timer == 0)
        {
            Destroy(tempRoom);
            cap = 0;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RoomActivator"))
        {
            timer = 15;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RoomActivator"))
        {
            if(cap == 0)
            { 
            cap += 1;
            tempRoom = (GameObject)Instantiate(Room, transform.parent, transform.parent);
            //tempRoom.transform.position = new Vector3(tempRoom.transform.position.x, tempRoom.transform.position.y, -8.54f);
            Room.gameObject.SetActive(true);
            }
        }
    }
}
