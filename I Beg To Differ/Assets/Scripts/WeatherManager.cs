using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeatherManager : MonoBehaviour {

    public enum WeatherType {Sun, Rain, Snow, Volcano};
    public WeatherType currentWeather;
    public float WeatherDuration = 90f;
    public float currentWeatherTimeRemaining;
    public Queue<WeatherType> WeatherForecast;
    public Sprite SunnySprite;
    public Sprite RainySprite;
    public Sprite SnowySprite;
    public Sprite VolcanoSprite;

    public WeatherForecastIcon today;
    public WeatherForecastIcon tomorrow;
    public WeatherForecastIcon twodays;
    public WeatherForecastIcon threedays;

    void Awake()
    {
        currentWeather = WeatherType.Sun;
        WeatherForecast = new Queue<WeatherType>();
        for (int i = 0; i < 3; i++)
        {
            WeatherForecast.Enqueue(GetNextWeather());
        }
    }

	void Start () 
    {

	}
	
	void Update () 
    {
        currentWeatherTimeRemaining -= Time.deltaTime;
        if(currentWeatherTimeRemaining < 0f)
        {
            ChangeWeather();    
        }
	}

    void ChangeWeather()
    {
        currentWeather = WeatherForecast.Dequeue();
        WeatherForecast.Enqueue(GetNextWeather());
    }

    WeatherType GetNextWeather()
    {
        float r = Random.Range(0f,1f);
        if(r > 0.95f)
        {
            return WeatherType.Volcano;
        }
        else if(r > 0.75)
        {
            return WeatherType.Snow;
        }
        else if(r > 0.5)
        {
            return WeatherType.Rain;
        }
        else
        {
            return WeatherType.Sun;
        }        
    }
}
