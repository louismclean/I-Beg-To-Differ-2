using UnityEngine;
using System.Collections;

public class WeatherForecastIcon : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    public float scrollRate = 0.5f;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-scrollRate, 0f);
	}
	
	// Update is called once per frame
	void Update () {
     
	}

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
