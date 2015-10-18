using UnityEngine;
using System.Collections;

public class menuaudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject[] x = GameObject.FindGameObjectsWithTag ("Music");
		if (x.Length > 1) {
			Destroy (this.gameObject);
		}
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName ==  "Home") {
			Destroy (this.gameObject);
		}
	}
}
