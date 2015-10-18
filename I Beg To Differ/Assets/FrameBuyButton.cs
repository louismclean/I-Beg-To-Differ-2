using UnityEngine;
using System.Collections;

public class FrameBuyButton : MonoBehaviour {

    House myHouse;

	// Use this for initialization
	void Start () {
        myHouse = GameObject.Find("House").GetComponent<House>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        if (myHouse.frameLevel < House.MAXFRAMELEVEL)
        {
            myHouse.UpgradeHouseFrame();
        }        
    }
}
