using UnityEngine;
using System.Collections;

public class fireSpawner : MonoBehaviour {

	public GameObject fireball;

	float range = 12f;

	float spawnTime = .5f;
	float ticker;

	// Use this for initialization
	void Start () {
		ticker = spawnTime;
	}
	
	// Update is called once per frame
	void Update () {
		ticker -= Time.deltaTime;
		if (ticker < 0) {
			ticker = spawnTime;

			Instantiate(fireball,this.transform.position +new Vector3(Random.Range(-range,2.5f*range),0,0),Quaternion.identity);


		}
	}
}
