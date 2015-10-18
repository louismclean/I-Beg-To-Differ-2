using UnityEngine;
using System.Collections;

public class volcanoSeason : MonoBehaviour {

	public GameObject flashWhite;
	float flash = 0.1f;
	float timer;
	bool on = true;
	int numFlashes = 10;
	// Use this for initialization
	void Start () {
		timer = flash;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) {
			timer = flash;
			numFlashes--;
			flashWhite.SetActive(on);
			on = !on;

		}
		if (numFlashes < 0) {
			Destroy(flashWhite.gameObject);
		}
	}
}
