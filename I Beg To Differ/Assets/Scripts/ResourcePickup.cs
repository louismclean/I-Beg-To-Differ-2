using UnityEngine;
using System.Collections;

public class ResourcePickup : MonoBehaviour {
	public float distance= 10f;
	public float rate = 0.1f;
	public int value = 5;
	public string type;
	public GameObject getText;
	private GameObject resourceman;
	int day;
	private bool goingUp = true;
	private bool getable = true;
	private float ctr;// = distance;
	private float ttl = 2f;

	// Use this for initialization
	void Start () {
		ctr = distance;
		resourceman = GameObject.FindGameObjectWithTag ("ResourceManager");

	}
	
	// Update is called once per frame
	void Update () {
		day = WorldTime.day;
		value = 5 + day / 5;
		move ();
		if (!getable) {
			ttl-=Time.deltaTime;
			if(ttl<0)
				Destroy(this.gameObject);
		}
	}

	void move(){
		if (goingUp)
			this.transform.position = new Vector3 (this.transform.position.x,
		                                       this.transform.position.y + rate,
		                                      this.transform.position.z);
		else
			this.transform.position = new Vector3 (this.transform.position.x,
			                                       this.transform.position.y - rate,
			                                       this.transform.position.z);

		ctr -= Time.deltaTime;
		if (ctr < 0) {
			ctr = distance;
			goingUp = !goingUp;
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player"&&getable) {
			this.gameObject.GetComponent<SpriteRenderer>().enabled=false;
			this.gameObject.GetComponent<ParticleSystem>().Play();
			this.gameObject.GetComponent<AudioSource>().Play();
			GameObject x = (GameObject)Instantiate(getText,this.transform.position-new Vector3(1,0,0),Quaternion.identity);
			x.GetComponent<TextMesh>().text = value+" "+type;
			getable=false;
			consume();
		}
	}

	void consume(){
		if(type=="wood")
			resourceman.SendMessage("getWood",value);
		if(type=="coin")
			resourceman.SendMessage("getCoin",value);
		if(type=="blanket")
			resourceman.SendMessage("getBlanket",value);
	}
}
