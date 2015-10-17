using UnityEngine;
using System.Collections;

public class WorldTime : MonoBehaviour {

    public float time = 0.5f;
    public static float dayDuration = 30f;
    private WeatherManager weatherManager;

	// Use this for initialization
	void Start () {
        weatherManager = GameObject.Find("WeatherManager").GetComponent<WeatherManager>();
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime/dayDuration;
        if(time > 1f)
        {
            time = 0f;
            weatherManager.ChangeWeather();
        }
	}

    public bool isDay()
    {
        return (time > 0.25f && time < 0.75f);
    }
}
