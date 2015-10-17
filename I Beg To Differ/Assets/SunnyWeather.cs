using UnityEngine;
using System.Collections;

public class SunnyWeather : MonoBehaviour {

    public float sunScale = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3 (1 + sunScale * Mathf.Abs(Mathf.Sin(Time.time)), 1 + sunScale * Mathf.Abs(Mathf.Sin(Time.time)), 0);
        transform.rotation = Quaternion.identity;
	}
}
