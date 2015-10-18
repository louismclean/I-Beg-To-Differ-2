using UnityEngine;
using System.Collections;

public class resourceManager : MonoBehaviour {

	public Resource coins;
	public Resource wood;
	public Resource blankets;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	public void getCoin(int amnt){
		coins.add (amnt);
	}

	public void getWood(int amnt){
		wood.add (amnt);
	}


	public void getBlanket(int amnt){
		blankets.add (amnt);
	}
		
	public bool spend(int coinamt, int woodamt, int blanketamt){
		if (coins.canSpend (coinamt) && wood.canSpend (woodamt) && blankets.canSpend (blanketamt)) {
			coins.spend(coinamt);
			wood.spend(woodamt);
			blankets.spend(blanketamt);
			return true;
		}
		return false;
	}
}


