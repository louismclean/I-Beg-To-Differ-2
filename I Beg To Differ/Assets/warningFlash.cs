using UnityEngine;
using System.Collections;

public class warningFlash : MonoBehaviour {

	HoboExposure hb;
	bool pulse = false;
	public float threshold = 0.85f;
	float pulseTimer;
	public float pulseDur = 1f;
	public Color c;
	public float colorspeed = 2f;
	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		pulseTimer = pulseDur;
		hb = GameObject.FindGameObjectWithTag ("Player").GetComponent<HoboExposure> ();
		sr = this.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (hb.exposure > threshold) {
			pulseTimer-= Time.deltaTime;
			if(pulseTimer <0){
				pulse = !pulse;
				pulseTimer = pulseDur;
			}

		}
		if (pulse) {
			sr.color = Color.Lerp(sr.color,c,colorspeed);
		}
		else{
			sr.color = Color.Lerp(sr.color, new Color(0,0,0,0),colorspeed);
		}
	}
}
