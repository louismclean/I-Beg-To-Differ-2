using UnityEngine;
using System.Collections;

public class stroller : MonoBehaviour {

	
	private bool m_FacingRight = true; 
	float m_Range = 0.1f;
	float speed = 1.5f;
	float ticker;
	Vector3 target;



	float pauseLength = 3f;
	float pauseTimer;
	bool paused = false;

	// Use this for initialization
	void Start () {
		ticker = speed;
		pauseTimer = pauseLength;
	}
	
	// Update is called once per frame
	void Update () {
		ticker -= Time.deltaTime;
		pauseTimer -= Time.deltaTime;

		if (!paused) {
			if (m_FacingRight) {
				this.transform.position += new Vector3 (m_Range, 0, 0);
			} else {
				this.transform.position -= new Vector3 (m_Range, 0, 0);
			}

			if (ticker < 0) {
				this.transform.localScale = new Vector3 (-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
				ticker = speed;
				m_FacingRight = !m_FacingRight;
			}
		}

		if (pauseTimer < 0) {
			pauseTimer = Random.Range(0,pauseLength);
			paused = !paused;
		}
	
	}
}
