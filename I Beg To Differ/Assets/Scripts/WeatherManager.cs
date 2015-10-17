using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeatherManager : MonoBehaviour {

    public enum WeatherType {Sun, Rain, Snow, Volcano};
    public WeatherType currentWeather;
    public float WeatherDuration = 5f;
    public float currentWeatherTimeRemaining;
    public Queue<WeatherType> WeatherForecast;
    public Sprite SunnySprite;
    public Sprite RainySprite;
    public Sprite SnowySprite;
    public Sprite VolcanoSprite;

    public WeatherForecastIcon[] weatherForecastIcons;

    void Awake()
    {
        currentWeather = WeatherType.Sun;
        WeatherForecast = new Queue<WeatherType>();
        for (int i = 0; i < 4; i++)
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
            currentWeatherTimeRemaining = WeatherDuration;
        }
	}

    void ChangeWeather()
    {
        currentWeather = WeatherForecast.Dequeue();
        WeatherForecast.Enqueue(GetNextWeather());
        
        Sprite newSprite = SunnySprite;
        WeatherType[] weatherArray = WeatherForecast.ToArray();

        for (int i = 0; i<4; i++)
        {
            switch (weatherArray[i])
            {
                case WeatherType.Sun:
                    newSprite = SunnySprite;
                    break;
                case WeatherType.Rain:
                    newSprite = RainySprite;
                    break;
                case WeatherType.Snow:
                    newSprite = SnowySprite;
                    break;
                case WeatherType.Volcano:
                    newSprite = VolcanoSprite;
                    break;
            }
            weatherForecastIcons[i].SetSprite(newSprite);
        }
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
