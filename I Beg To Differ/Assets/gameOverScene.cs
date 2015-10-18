using UnityEngine;
using System.Collections;

public class gameOverScene : MonoBehaviour {


	float ttl = 5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		ttl -= Time.deltaTime;
		if (ttl < 0) {
			Application.LoadLevel("MainMenu");
		}
	}
}
