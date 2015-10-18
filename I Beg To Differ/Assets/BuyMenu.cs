using UnityEngine;
using System.Collections;

public class BuyMenu : MonoBehaviour {

    public TextMesh FrameWoodQty;
    public TextMesh FrameBlanketQty;
    public TextMesh FrameCoinQty;

    public TextMesh MaterialWoodQty;
    public TextMesh MaterialBlanketQty;
    public TextMesh MaterialCoinQty;

    House myHouse;

	// Use this for initialization
	void Start () {
        myHouse = GameObject.Find("House").GetComponent<House>();
	}
	
	// Update is called once per frame
	void Update () {
        FrameWoodQty.text = "x " + myHouse.getFrameUpgradeCost(myHouse.frameLevel).woodCost;
        FrameBlanketQty.text = "x " + myHouse.getFrameUpgradeCost(myHouse.frameLevel).blanketCost;
        FrameCoinQty.text = "x " + myHouse.getFrameUpgradeCost(myHouse.frameLevel).coinCost;
        
        MaterialWoodQty.text = "x " + myHouse.getMaterialUpgradeCost(myHouse.frameLevel, myHouse.materialLevel).woodCost;
        MaterialBlanketQty.text = "x " + myHouse.getMaterialUpgradeCost(myHouse.frameLevel, myHouse.materialLevel).blanketCost;
        MaterialCoinQty.text = "x " + myHouse.getMaterialUpgradeCost(myHouse.frameLevel, myHouse.materialLevel).coinCost;
	}
}
