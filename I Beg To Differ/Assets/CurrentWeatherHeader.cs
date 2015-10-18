using UnityEngine;
using System.Collections;

public class CurrentWeatherHeader : MonoBehaviour {

    WeatherManager weatherManager;
    TextMesh textMesh;

	// Use this for initialization
	void Start () {
        weatherManager = GameObject.Find("WeatherManager").GetComponent<WeatherManager>();
        textMesh = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        string weatherText = "Undefined Weather Type";
        switch (weatherManager.currentWeather)
        {
            //switch scene based on current weather
            case WeatherManager.WeatherType.Sun:
                weatherText = "Sunny and Warm";
                break;
            
            case WeatherManager.WeatherType.Rain:
                switch(weatherManager.weatherIntensity)
                {
                    case 1:
                        weatherText = "Light Drizzle";
                        break;
                    case 2:
                        weatherText = "Heavy Rain";
                        break;
                    case 3:
                        weatherText = "Torrential Downpour";
                        break;
                    case 4:
                        weatherText = "Cataclysmic Monsoon";
                        break;
                    default:
                        weatherText = "Undefined Weather Intensity";
                        break;
                }
                break;
            
            case WeatherManager.WeatherType.Snow:
                switch(weatherManager.weatherIntensity)
                {
                    case 1:
                        weatherText = "Snow Day";
                        break;
                    case 2:
                        weatherText = "Hyperborean Tempest";
                        break;
                    default:
                        weatherText = "Undefined Weather Intensity";
                        break;
                }
                break;
            
            case WeatherManager.WeatherType.Volcano:
                weatherText = "Volcano";
                break;
        }
        textMesh.text = "Current Weather: " + weatherText;
	}
}
