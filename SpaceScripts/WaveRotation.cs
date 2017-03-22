using UnityEngine;
using System.Collections;

public class WaveRotation : MonoBehaviour {

    public float waveSpeed;
    public float waveAmplitude;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        var parentScript = GetComponentInParent<GenericRotation>();

        parentScript.speed = parentScript.baseSpeed + waveAmplitude * Mathf.Sin(waveSpeed * transform.position.z);

	}
}
