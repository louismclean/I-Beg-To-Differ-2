using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeatherManager : MonoBehaviour {

    public enum WeatherType {Sun, Rain, Snow, Volcano};
    public WeatherType currentWeather;
    public float WeatherDuration = 15f;
    public float currentWeatherTimeRemaining;
    public Queue<WeatherType> WeatherForecast;
    public Sprite SunnySprite;
    public Sprite RainySprite;
    public Sprite SnowySprite;
    public Sprite VolcanoSprite;
    public WeatherForecastIcon WeatherForecastIconPrefab;

    public List<WeatherForecastIcon> weatherForecastIcons;

    public GameObject rainParticleSystem;
    public GameObject snowParticleSystem;
    public GameObject sunnySystem;

    public Transform forecastLeftAnchor;
    public Transform forecastRightAnchor;

    void Awake()
    {
        currentWeather = WeatherType.Sun;
        WeatherForecast = new Queue<WeatherType>();

        //First few days are nice
        for (int i = 0; i < 3; i++)
        {
            WeatherForecast.Enqueue(WeatherType.Sun);
        }
        //Fourth day is rainy
        WeatherForecast.Enqueue(WeatherType.Rain);
    }

	void Start () 
    {
        rainParticleSystem.SetActive(false);
        snowParticleSystem.SetActive(false);
	}
	
	void Update () 
    {
        //currentWeatherTimeRemaining -= Time.deltaTime;
       // if(currentWeatherTimeRemaining < 0f)
       // {
       //     ChangeWeather();
       //     currentWeatherTimeRemaining = WeatherDuration;
       // }

        //foreach (WeatherForecastIcon icon in weatherForecastIcons)
        //{
       //     icon.transform.position = (icon.transform.position - new Vector3(Time.deltaTime * forecastIconScrollRate, 0f, 0f));
       // }
	}

    public void ChangeWeather()
    {
        currentWeather = WeatherForecast.Dequeue();
        WeatherType nextWeather = GetNextWeather();
        WeatherForecast.Enqueue(nextWeather);

        WeatherForecastIcon newIcon = Instantiate(WeatherForecastIconPrefab, transform.position, transform.rotation) as WeatherForecastIcon;
        newIcon.transform.parent = transform.parent;
        weatherForecastIcons.Add(newIcon);
        weatherForecastIcons.RemoveAt(0);
        
        Sprite newSprite = SunnySprite;
       
        switch (nextWeather)
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
        newIcon.SetSprite(newSprite);

        rainParticleSystem.SetActive(false);
        snowParticleSystem.SetActive(false);

        switch (currentWeather)
        {
            case WeatherType.Sun:
                sunnySystem.SetActive(true);
                break;
            case WeatherType.Rain:
                rainParticleSystem.SetActive(true);
                break;
            case WeatherType.Snow:
                snowParticleSystem.SetActive(true);
                break;
            case WeatherType.Volcano:
                break;
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
