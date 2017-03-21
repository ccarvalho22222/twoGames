using UnityEngine;
using System.Collections;

public class FireJetAI : MonoBehaviour {

    public int fireWaitTime;
    public int fireWaitTimeCounter = 0;

    public int fireStayTime;
    private int fireStayTimeCounter = 0;

    private bool shooting = false;

    public GameObject Fire;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (!shooting)
        {
            if (fireWaitTimeCounter++ > fireWaitTime)
            {
                fireWaitTimeCounter = 0;
                Fire.SetActive(true);
                shooting = true;
            }
        }
        else if(shooting)
        {
            if(fireStayTimeCounter++ > fireStayTime)
            {
                fireStayTimeCounter = 0;
                Fire.SetActive(false);
                shooting = false;
            }
        }
	}
}
