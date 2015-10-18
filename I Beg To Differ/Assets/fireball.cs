using UnityEngine;
using System.Collections;

public class fireball : MonoBehaviour {
	float m_force = 4f;
	float ttl = 2.5f;
	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody2D> ().AddForce(new Vector2 (-200f, -m_force));
	}
	
	// Update is called once per frame
	void Update () {
		ttl -= Time.deltaTime;
		if (ttl < 0) {

			Destroy (this.gameObject);
		}
	}

}
