using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerManager : MonoBehaviour {

    public enum ResourceType {Wood, Blanket, Coin};

    public int maxLooseCoins = 3;
    public int maxLooseBlankets = 7;
    public int maxLooseWoods = 10;
    List<ItemSpawner> CoinSpawners = new List<ItemSpawner>();
    List<ItemSpawner> BlanketSpawners = new List<ItemSpawner>();
    List<ItemSpawner> WoodSpawners = new List<ItemSpawner>(); 
    

	// Use this for initialization
	void Start () {
       
        GameObject[] objectList = GameObject.FindGameObjectsWithTag("CoinSpawner");
        foreach(GameObject go in objectList)
        {
            CoinSpawners.Add(go.GetComponent<ItemSpawner>());
        }

        objectList = GameObject.FindGameObjectsWithTag("BlanketSpawner");
        foreach (GameObject go in objectList)
        {
            BlanketSpawners.Add(go.GetComponent<ItemSpawner>());
        }

        objectList = GameObject.FindGameObjectsWithTag("WoodSpawner");
        foreach (GameObject go in objectList)
        {
            WoodSpawners.Add(go.GetComponent<ItemSpawner>());
        }
	}
	
	// Update is called once per frame
	void Update () {
         
	}

    public bool CanSpawn(ResourceType a_resourcetype)
    {
        int count = 0;
        List<ItemSpawner> itemSpawners = CoinSpawners;
        int max = 0;
        
        switch(a_resourcetype)
        {
            case ResourceType.Coin:
                itemSpawners = CoinSpawners;
                max = maxLooseCoins;
                break;
            case ResourceType.Blanket:
                itemSpawners = BlanketSpawners;
                max = maxLooseBlankets;
                break;
            case ResourceType.Wood:
                itemSpawners = WoodSpawners;
                max = maxLooseWoods;
                break;
        }

        foreach (ItemSpawner spawner in itemSpawners)
        {
            if(spawner.SpawnedItemIsWaiting())
            {
                count++;
            }
        }
        
        if (count > max)
        {
            return false;
        }

        return true;
    }
}
