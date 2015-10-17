﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerManager : MonoBehaviour {

    public enum ResourceType {Wood, Blanket, Coin};

    public int maxLooseCoins = 3;
    public int maxLooseBlankets = 7;
    public int maxLooseWoods = 10;
    List<ItemSpawner> ChangeSpawners;
    List<ItemSpawner> BlanketSpawners;
    List<ItemSpawner> WoodsSpawners; 
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
         
	}

    public bool CanSpawn(ResourceType a_resourcetype)
    {
        //put logic here
        return true;
    }
}
