using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeatherManager : MonoBehaviour {

    public enum WeatherType {Sun, Rain, Snow, Volcano};
    public WeatherType currentWeather;
    public int weatherIntensity = 1;
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
	public GameObject lavaSystem;

    public Transform forecastLeftAnchor;
    public Transform forecastRightAnchor;

    private float rainEmissionRateLevel1 = 20f;
    private float rainEmissionRateLevel2 = 60f;
    private float rainEmissionRateLevel3 = 150f;

    private float snowEmissionRateLevel1 = 20f;
    private float snowEmissionRateLevel2 = 60f;
    private float snowEmissionRateLevel3 = 150f;

    private int midGame = 10;
    private int lateGame = 30;
    private int endGame = 60;


    void Awake()
    {
        weatherIntensity = 1;
        currentWeather = WeatherType.Sun;
        WeatherForecast = new Queue<WeatherType>();

        WeatherForecastIcon newIcon;

        //First day is nice        
        newIcon = Instantiate(WeatherForecastIconPrefab, transform.position, transform.rotation) as WeatherForecastIcon;
        newIcon.transform.parent = transform.parent;
        newIcon.lifeTime = WorldTime.dayDuration * 3;
        newIcon.SetSprite(SunnySprite);

        //Second day is rainy
        WeatherForecast.Enqueue(WeatherType.Rain);
        newIcon = Instantiate(WeatherForecastIconPrefab, transform.position, transform.rotation) as WeatherForecastIcon;
        newIcon.transform.parent = transform.parent;
        newIcon.lifeTime = WorldTime.dayDuration * 2;
        newIcon.SetSprite(RainySprite);        

        //Third day is nice
        WeatherForecast.Enqueue(WeatherType.Sun);
        newIcon = Instantiate(WeatherForecastIconPrefab, transform.position, transform.rotation) as WeatherForecastIcon;
        newIcon.transform.parent = transform.parent;
        newIcon.lifeTime = WorldTime.dayDuration * 1;
        newIcon.SetSprite(SunnySprite);

        //Fourth day is snowy
        WeatherForecast.Enqueue(WeatherType.Snow);
        newIcon = Instantiate(WeatherForecastIconPrefab, transform.position, transform.rotation) as WeatherForecastIcon;
        newIcon.transform.parent = transform.parent;
        newIcon.lifeTime = 0;
        newIcon.SetSprite(SnowySprite);
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
            switch(weatherIntensity)
            {
                case 1:
                    rainParticleSystem.GetComponent<ParticleSystem>().emissionRate = rainEmissionRateLevel1;
                    break;
                case 2:
                    rainParticleSystem.GetComponent<ParticleSystem>().emissionRate = rainEmissionRateLevel2;
                    break;
                case 3:
                    rainParticleSystem.GetComponent<ParticleSystem>().emissionRate = rainEmissionRateLevel3;
                    break;
                default:
                    rainParticleSystem.GetComponent<ParticleSystem>().emissionRate = rainEmissionRateLevel1;
                    break;
            }
             
			if(timer.isDay())
				targetCol = rainCol_day;
			else
				targetCol = rainCol_night;
			break;


		case WeatherType.Snow:
			snowParticleSystem.SetActive(true);
            switch (weatherIntensity)
            {
                case 1:
                    snowParticleSystem.GetComponent<ParticleSystem>().emissionRate = snowEmissionRateLevel1;
                    break;
                case 2:
                    snowParticleSystem.GetComponent<ParticleSystem>().emissionRate = snowEmissionRateLevel2;
                    break;
                case 3:
                    snowParticleSystem.GetComponent<ParticleSystem>().emissionRate = snowEmissionRateLevel3;
                    break;
                default:
                    snowParticleSystem.GetComponent<ParticleSystem>().emissionRate = snowEmissionRateLevel1;
                    break;
            }
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

		if (currentWeather == WeatherType.Volcano) {
			lavaSystem.SetActive (true);
		} else {
			lavaSystem.SetActive (false);
		}

	}

    public void ChangeWeather()
    {
        WeatherType nextWeather = GetNextWeather();
        WeatherForecast.Enqueue(nextWeather);
        currentWeather = WeatherForecast.Dequeue();

        weatherIntensity = GetNewWeatherIntensity();

        WeatherForecastIcon newIcon = Instantiate(WeatherForecastIconPrefab, transform.position, transform.rotation) as WeatherForecastIcon;
        newIcon.transform.parent = transform.parent;
        
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

        //day 10 volcano
        if(WorldTime.day == 7)
        {
            return WeatherType.Volcano;
        }

        float r = Random.Range(0f,1f);

        if(WorldTime.day > endGame)
        {
            if(r > 0.5f)
            {
                return WeatherType.Volcano;
            }
            else if (r > 0.25f)
            {
                return WeatherType.Rain;
            }
            else
            {
                return WeatherType.Snow;
            }
        }
        else if(WorldTime.day > lateGame)
        {
            if (r > 0.80f)
            {
                return WeatherType.Volcano;
            }
            else if (r > 0.50f)
            {
                return WeatherType.Rain;
            }
            else if (r > 0.20f)
            {
                return WeatherType.Snow;
            }
            else
            {
                return WeatherType.Sun;
            }
        }
        else if(WorldTime.day > midGame)
        {
            if (r > 0.90f)
            {
                return WeatherType.Volcano;
            }
            else if (r > 0.60f)
            {
                return WeatherType.Rain;
            }
            else if (r > 0.30f)
            {
                return WeatherType.Snow;
            }
            else
            {
                return WeatherType.Sun;
            }
        }
        else
        {
            if (r > 0.95f)
            {
                return WeatherType.Volcano;
            }
            else if (r > 0.70f)
            {
                return WeatherType.Rain;
            }
            else if (r > 0.50f)
            {
                return WeatherType.Snow;
            }
            else
            {
                return WeatherType.Sun;
            }
        }

        return WeatherType.Sun;   
    }

	public string getWeather(){
		switch (currentWeather)
		{
		case WeatherType.Sun:
			return "sun";
		case WeatherType.Rain:
			return "rain";
		case WeatherType.Snow:
			return "snow";
		case WeatherType.Volcano:
			return "volcano";
		}
		return "";
	}

    int GetNewWeatherIntensity()
    {
        float r = Random.Range(0f, 1f);
        switch (currentWeather)
        {
            //Sun intensity is 1
            case WeatherType.Sun:
                return 1;
            
            //Rain intensity
            case WeatherType.Rain:
                if (WorldTime.day > endGame)
                {
                    return 4;
                }
                else if (WorldTime.day > lateGame)
                {
                    if (r > 0.5)
                    {
                        return 3;
                    }
                    else return 2;
                    
                }
                else if (WorldTime.day > midGame)
                {
                    if (r > 0.5)
                    {
                        return 2;
                    }
                    else return 1;
                }
                else
                {
                    return 1;
                }
            
            //Snow intensity
            case WeatherType.Snow:
                if (WorldTime.day > endGame)
                {
                    return 2;
                }
                else if (WorldTime.day > lateGame)
                {
                    if (r > 0.25)
                    {
                        return 2;
                    }
                    else return 1;

                }
                else if (WorldTime.day > midGame)
                {
                    if (r > 0.5)
                    {
                        return 2;
                    }
                    else return 1;
                }
                else
                {
                    return 1;
                }

            case WeatherType.Volcano:
                return 3;
        }
        return 1;
    }
}
