using UnityEngine;
using System.Collections;

public class SunnyWeather : MonoBehaviour {

    public float sunScale = 0.5f;
    public WorldTime worldTime;
    public float maxHeight = 15f;

	// Use this for initialization
	void Start () {
        worldTime = GameObject.Find("WorldTime").GetComponent<WorldTime>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3 (1 + sunScale * Mathf.Abs(Mathf.Sin(Time.time)), 1 + sunScale * Mathf.Abs(Mathf.Sin(Time.time)), 0);
       // transform.rotation = Quaternion.identity;
        transform.position = new Vector3(transform.position.x, -10f + sunHeight(worldTime.time) * maxHeight, transform.position.z);
	}

    float sunHeight(float worldTime)
    {
        Debug.Log(worldTime);
        if(worldTime < 0.5f)
        {
            return worldTime * 2f;
        }
        else
        {
            return (1f - worldTime) * 2f;
        }
    }
}
