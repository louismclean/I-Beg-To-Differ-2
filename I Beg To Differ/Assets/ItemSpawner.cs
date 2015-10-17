using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

    public enum ResourceType {Wood, Blanket, Change};
    public ResourceType resourcetype;

    public ResourcePickup resourcePrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Spawn()
    {
        GameObject.Instantiate(resourcePrefab, this.transform.position, this.transform.rotation);
    }

    bool canSpawn()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, 0.5f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }
        }
    }
}
