using UnityEngine;
using System.Collections;

public class HoboExposure : MonoBehaviour {

    public float exposure;

    public Vector2 pos = new Vector2(20, 40);
    public Vector2 size = new Vector2(60, 20);
    public Texture2D emptyTex;
    public Texture2D fullTex;
    public WeatherManager weatherManager;

    public float SunnyExposureRestoreRate = 0.01f;
    public float RainyExposureRate = 0.02f;
    public float SnowyExposureRate = 0.04f;

    private bool _isExposed;

    void OnGUI()
    {
        //draw the background:
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex);

        //draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * exposure, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), fullTex);
        GUI.EndGroup();
        GUI.EndGroup();
    }

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        _isExposed = true;

	    if(_isExposed)
        {
            switch(weatherManager.currentWeather)
            {
                case WeatherManager.WeatherType.Sun:
                    exposure = Mathf.Max(exposure - Time.deltaTime * SunnyExposureRestoreRate, 0f);
                    break;
                case WeatherManager.WeatherType.Rain:
                    exposure = Mathf.Min(exposure + Time.deltaTime * RainyExposureRate, 1f);
                    break;
                case WeatherManager.WeatherType.Snow:
                    exposure = Mathf.Min(exposure + Time.deltaTime * SnowyExposureRate, 1f);
                    break;
                case WeatherManager.WeatherType.Volcano:
                    break;
            }
        }
	}
}
