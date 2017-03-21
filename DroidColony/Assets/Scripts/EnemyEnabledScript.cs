using UnityEngine;
using System.Collections;

public class EnemyEnabledScript : MonoBehaviour {

    public bool engaged;
    public GameObject alerter;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (engaged)
            alerter.GetComponent<AlerterScript>().active = true;
	
	}
}
