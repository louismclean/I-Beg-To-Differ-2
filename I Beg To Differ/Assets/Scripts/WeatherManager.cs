using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeatherManager : MonoBehaviour {

    public enum WeatherType {Sun, Rain, Snow, Volcano};
    public WeatherType currentWeather;
    public Queue<WeatherType> WeatherForecast;
    public Sprite SunnySprite;
    public Sprite RainySprite;
    public Sprite SnowySprite;
    public Sprite VolcanoSprite;
    public WeatherForecastIcon WeatherForecastIconPrefab;
	public Camera mainCam;

	public Color sunnyCol_day = new Color(30,117,255);
	public Color rainCol_day = new Color(154,176,210);
	public Color snowCol_day = new Color(202,236,232);
	public Color doomCol_day = new Color(203,63,12);	
	public Color sunnyCol_night = new Color(58,69,86);
	public Color rainCol_night = new Color(84,96,114);
	public Color snowCol_night = new Color(173,148,208);
	public Color doomCol_night = new Color(109,57,38);
	public Color targetCol;
	bool isNight;
	WorldTime timer;
	public float colorRate = 2f;


    public static int ForecastDays = 4;

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
		timer = GameObject.FindGameObjectWithTag ("WorldTime").GetComponent<WorldTime>();
		targetCol = sunnyCol_day;
        rainParticleSystem.SetActive(false);
        snowParticleSystem.SetActive(false);
	}
	
	void Update () 
    {
		mainCam.backgroundColor = Color.Lerp (mainCam.backgroundColor, targetCol, Time.deltaTime*colorRate);

		switch (currentWeather)
		{
		case WeatherType.Sun:
			sunnySystem.SetActive(true);
			if(timer.isDay())
				targetCol = sunnyCol_day;
			else
				targetCol = sunnyCol_night;
			break;
		case WeatherType.Rain:
			rainParticleSystem.SetActive(true);
			if(timer.isDay())
				targetCol = rainCol_day;
			else
				targetCol = rainCol_night;
			break;
		case WeatherType.Snow:
			snowParticleSystem.SetActive(true);
			if(timer.isDay())
				targetCol = snowCol_day;
			else
				targetCol = snowCol_night;
			break;
            case WeatherType.Volcano:
                if(timer.isDay())
                    targetCol = doomCol_day;
                else
                    targetCol = doomCol_night;
                break;
        }
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
				if(timer.isDay())
					targetCol = sunnyCol_day;
				else
					targetCol = sunnyCol_night;
                break;
            case WeatherType.Rain:
				rainParticleSystem.SetActive(true);
				if(timer.isDay())
					targetCol = rainCol_day;
				else
					targetCol = rainCol_night;
                break;
            case WeatherType.Snow:
				snowParticleSystem.SetActive(true);
				if(timer.isDay())
					targetCol = snowCol_day;
				else
					targetCol = snowCol_night;
                break;
			case WeatherType.Volcano:
				if(timer.isDay())
					targetCol = doomCol_day;
				else
                    targetCol = doomCol_night;
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
