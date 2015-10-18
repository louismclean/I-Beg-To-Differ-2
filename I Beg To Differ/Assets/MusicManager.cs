using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip sun;
	public AudioClip rain;
	public AudioClip snow;
	public AudioClip doom;

	string weath;

	AudioSource current;
	WeatherManager wm;

	// Use this for initialization
	void Start () {
		current = this.GetComponent<AudioSource> ();
		wm = this.GetComponent<WeatherManager> ();
		weath = wm.getWeather ();
		setSong (weath);
		current.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!current.isPlaying)
			current.Play ();
		if(wm.getWeather() != weath){
			current.Stop();
			weath = wm.getWeather();
			setSong(weath);
			current.Play ();
		}

	}

	public void setSong(string song){
		switch (song) {
		case "sun":
			current.clip = sun;
			break;
		case "rain":
			current.clip = rain;
			break;
		case "snow":
			current.clip = snow;
			break;
		case "volcano":
			current.clip = doom;
			break;

		}
	}
}
