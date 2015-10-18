using UnityEngine;
using System.Collections;

public class CloudMaker : MonoBehaviour {

	GameObject[] clouds;
	WeatherManager wm;

	public Color normalCol;
	public Color rainCol;
	public Color snowCol;
	public Color fireCol;

	float timer = 10f;
	float ticker;

	float colorSpeed = 1f;

	float size = 0.002f;

	Color targetCol;

	// Use this for initialization
	void Start () {
		//instantiate clouds
		targetCol = normalCol;
		ticker = timer;
		wm = (WeatherManager)GameObject.Find ("WeatherManager").GetComponent<WeatherManager>();
	}
	
	// Update is called once per frame
	void Update () {
		//scale up and down
		ticker -= Time.deltaTime;

		if (ticker < 0) {
			size = -size;
			ticker = timer;
		}

		this.transform.localScale += new Vector3 (size, size, 0);

		switch (wm.currentWeather) {
			
		case WeatherManager.WeatherType.Sun:
			targetCol = normalCol;
			break;
		case WeatherManager.WeatherType.Rain:
			targetCol = rainCol;
			break;
		case WeatherManager.WeatherType.Snow:
			targetCol = snowCol;
			break;
		case WeatherManager.WeatherType.Volcano:
			targetCol = fireCol;
			break;
		
		}

		this.GetComponent<SpriteRenderer> ().color = Color.Lerp (this.GetComponent<SpriteRenderer> ().color, targetCol, colorSpeed);

		//lerp color
	}
}
