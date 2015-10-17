using UnityEngine;
using System.Collections;

public class SunMoonCircle : MonoBehaviour {

    public float rotationRate = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0f, 0f, Time.deltaTime * rotationRate);
	
	}
}
