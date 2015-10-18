using UnityEngine;
using System.Collections;

public class HoboExposure : MonoBehaviour {

    public float exposure;
	public float max_exposure=1;

    public Vector2 pos = new Vector2(20, 40);
    public Vector2 size = new Vector2(60, 20);
    public Texture2D emptyTex;
    public Texture2D fullTex;
    public WeatherManager weatherManager;

    public float homeRestoreRate = 0.025f;

    public float SunnyExposureRestoreRate = 0.01f;
    public float RainyExposureRate = 0.02f;
    public float SnowyExposureRate = 0.04f;

    public Transform guiAnchor;
    public House myHouse;

    private bool _isExposed;

    private HoboController hControl;

    void OnGUI()
    {
        //Vector2 pos = Camera.main.WorldToViewportPoint(guiAnchor.position);
        //Debug.Log("gui position: " + pos.x + ", " + pos.y);
        //draw the background:
        //float posx = (Screen.width / 2) - (size.x / 2);
        //GUI.BeginGroup(new Rect(posx, pos.y, size.x, size.y));
        //GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex);

        //draw the filled-in part:
        //GUI.BeginGroup(new Rect(0, 0, size.x * exposure, size.y));
       // GUI.Box(new Rect(0, 0, posx, pos.y), fullTex);
       // GUI.EndGroup();
        //GUI.EndGroup();
    }

    // Use this for initialization
	void Start () {
        //guiAnchor = GameObject.Find("Exposure Progress Bar Anchor").transform;
        hControl = gameObject.GetComponent<HoboController>();
	}
	
	// Update is called once per frame
	void Update () {

        _isExposed = !hControl.m_isInHouse;

        //If not protected, take some exposure
	    if(!isProtected())
        {
            switch(weatherManager.currentWeather)
            {
                case WeatherManager.WeatherType.Rain:
                    exposure = Mathf.Min(exposure + Time.deltaTime * RainyExposureRate * weatherManager.weatherIntensity, 1f);
                    break;
                case WeatherManager.WeatherType.Snow:
                    exposure = Mathf.Min(exposure + Time.deltaTime * SnowyExposureRate * weatherManager.weatherIntensity, 1f);
                    break;
            }
        }

        //If not exposed, regain some health
        if(!_isExposed)
        {
            exposure = Mathf.Max(exposure - Time.deltaTime * homeRestoreRate * myHouse.frameLevel, 0f);
        }

        //If you're overexposed, die
        if (exposure >= max_exposure && !hControl.m_isDying)
        {
            Debug.Log("dying now kk");
            hControl.m_isDying = true;
			switch(weatherManager.currentWeather)
            {
                //switch scene based on current weather
                case WeatherManager.WeatherType.Sun:
                    StartCoroutine(WaitAndLoadLevel("GameOverSun"));
                    break;
                case WeatherManager.WeatherType.Rain:
                    StartCoroutine(WaitAndLoadLevel("GameOverRain"));
                    break;
                case WeatherManager.WeatherType.Snow:
                    StartCoroutine(WaitAndLoadLevel("GameOverSnow"));
                    break;
                case WeatherManager.WeatherType.Volcano:
                    StartCoroutine(WaitAndLoadLevel("GameOverVolcano"));
                    break;
            }	
		}
	}

    public bool isProtected()
    {
        if(_isExposed)
        {
            return false;
        }

        switch (weatherManager.currentWeather)
        {
            //switch scene based on current weather
            case WeatherManager.WeatherType.Sun:
                return true;
            case WeatherManager.WeatherType.Rain:
                return myHouse.frameLevel >= weatherManager.weatherIntensity;
            case WeatherManager.WeatherType.Snow:
                return myHouse.materialLevel >= weatherManager.weatherIntensity;
            case WeatherManager.WeatherType.Volcano:
                return myHouse.materialLevel >= 3;
        }

        return true;
    }

    IEnumerator WaitAndLoadLevel(string levelName)
    {
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel(levelName);
    }    
}
