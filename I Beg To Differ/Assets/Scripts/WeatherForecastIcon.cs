using UnityEngine;
using System.Collections;

public class WeatherForecastIcon : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    public float scrollRate = 0.5f;
    public float lifeTime = 0f;
    public Transform leftAnchor;
    public Transform rightAnchor;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-scrollRate, 0f);
        leftAnchor = GameObject.Find("Forecast Left Anchor").transform;
        rightAnchor = GameObject.Find("Forecast Right Anchor").transform;
	}
	
	// Update is called once per frame
	void Update () {
        lifeTime += Time.deltaTime;
        transform.position = Vector3.Lerp(rightAnchor.position, leftAnchor.position, lifeTime / (WorldTime.dayDuration * 4));
     
	}

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
