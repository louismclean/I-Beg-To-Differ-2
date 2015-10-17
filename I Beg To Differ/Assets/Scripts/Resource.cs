using UnityEngine;
using System.Collections;

public class Resource : MonoBehaviour {

	public int quantity = 0;
	private TextMesh text;

	// Use this for initialization
	void Start () {
		text = this.GetComponent<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = quantity.ToString ();
	}

	public void add(int amnt){
		quantity += amnt;
	}

	public bool canSpend(int amnt){
		if(quantity>amnt){
			return true;
		}
		return false;
	}

	public void spend(int amnt){
		quantity -= amnt;
	}
}
