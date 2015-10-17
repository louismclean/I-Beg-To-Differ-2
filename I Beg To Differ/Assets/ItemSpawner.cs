using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

    
    public SpawnerManager.ResourceType resourcetype;

    public ResourcePickup resourcePrefab;

    private float spawnTimer;
    private float minSpawnTime = 20f;
    private float maxSpawnTime = 60f;

    private float m_PlayerSpawnBlockProximity = 15f;

    SpawnerManager myManager;

	// Use this for initialization
	void Start () {
        myManager = GameObject.FindGameObjectWithTag("SpawnerManager").GetComponent<SpawnerManager>();
        spawnTimer = Random.Range(1f, 5f);
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer < 0f)
        {
            spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
            if(CanSpawn())
            {
                Spawn();
            }
        }
	}

    void Spawn()
    {
        GameObject.Instantiate(resourcePrefab, this.transform.position, this.transform.rotation);
        Debug.Log("Spawned a " + resourcetype.ToString());
    }

    bool CanSpawn()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, 0.5f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "ResourcePickup")
            {
                return false;
            }
        }

        if(Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, this.transform.position) < m_PlayerSpawnBlockProximity)
        {
            return false;
        }

        if(!myManager.CanSpawn(resourcetype))
        {
            return false;
        }

        return true;
    }
}
