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

    public float SunnyExposureRestoreRate = 0.01f;
    public float RainyExposureRate = 0.02f;
    public float SnowyExposureRate = 0.04f;

    public Transform guiAnchor;

    private bool _isExposed;

    void OnGUI()
    {
        //Vector2 pos = Camera.main.WorldToViewportPoint(guiAnchor.position);
        //Debug.Log("gui position: " + pos.x + ", " + pos.y);
        //draw the background:
        float posx = (Screen.width / 2) - (size.x / 2);
        GUI.BeginGroup(new Rect(posx, pos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex);

        //draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * exposure, size.y));
        GUI.Box(new Rect(0, 0, posx, pos.y), fullTex);
        GUI.EndGroup();
        GUI.EndGroup();
    }

    // Use this for initialization
	void Start () {
        guiAnchor = GameObject.Find("Exposure Progress Bar Anchor").transform;
	}
	
	// Update is called once per frame
	void Update () {

        _isExposed = true;

	    if(_isExposed)
        {
            switch(weatherManager.currentWeather)
            {
                case WeatherManager.WeatherType.Sun:
                    //exposure = Mathf.Max(exposure - Time.deltaTime * SunnyExposureRestoreRate, 0f);
                    break;
                case WeatherManager.WeatherType.Rain:
                    exposure = Mathf.Min(exposure + Time.deltaTime * RainyExposureRate * weatherManager.weatherIntensity, 1f);
                    break;
                case WeatherManager.WeatherType.Snow:
                    exposure = Mathf.Min(exposure + Time.deltaTime * SnowyExposureRate * weatherManager.weatherIntensity, 1f);
                    break;
                case WeatherManager.WeatherType.Volcano:
                    break;
            }
        }

		if (exposure > max_exposure) {
			Application.LoadLevel("GameOver");
			//switch scene based on current weather
		}
	}
}
