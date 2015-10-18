using UnityEngine;
using System.Collections;

public class fireball : MonoBehaviour {
	float m_force = 4f;
	float ttl = 2.5f;
	public float damage = 0.25f;
	HoboExposure exp;
	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody2D> ().AddForce(new Vector2 (-200f, -m_force));
		exp = GameObject.FindGameObjectWithTag ("Player").GetComponent<HoboExposure> ();
	}
	
	// Update is called once per frame
	void Update () {
		ttl -= Time.deltaTime;
		if (ttl < 0) {

			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			exp.exposure+= damage;
			Destroy(this.gameObject);

		}

	}

}
