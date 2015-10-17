using UnityEngine;
using System.Collections;

public class Moon : MonoBehaviour {

    public float moonScale = 0.5f;
    public WorldTime worldTime;
    public float maxHeight = 15f;

	// Use this for initialization
	void Start () {
        worldTime = GameObject.Find("WorldTime").GetComponent<WorldTime>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(1 + moonScale * Mathf.Abs(Mathf.Cos(Time.time)), 1 + moonScale * Mathf.Cos(Mathf.Sin(Time.time)), 0);
        //transform.rotation = Quaternion.identity;
        transform.position = new Vector3(transform.position.x, -10f + MoonHeight(worldTime.time) * maxHeight, transform.position.z);
	}

    float MoonHeight(float worldTime)
    {
        Debug.Log(worldTime);
        if (worldTime < 0.5f)
        {
            return (1f - worldTime);            
        }
        else
        {
            return (worldTime - 0.5f) * 2;
        }
    }
}
