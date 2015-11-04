using UnityEngine;
using System.Collections;

public class Beggable : MonoBehaviour {

    GameObject hobo;
    float m_hobodistance = 0;
    float m_begradius = 2f;
    
    float begCooldown = 10f;
    float begTimer = 0f;

    public ResourcePickup coinPrefab;

    private GameObject begSign;

	// Use this for initialization
	void Start () {
        hobo = GameObject.Find("hobo_player");
        begSign = GameObject.Find("Beg");
	}
	
	// Update is called once per frame
	void Update () {
        //update conditions
        if(begTimer > 0f)
        {
            begTimer -= Time.deltaTime;
        }
        m_hobodistance = Vector2.Distance(this.transform.position, hobo.transform.position);

        if(CanBeg() && Input.GetKeyDown(KeyCode.W))
        {
            Beg();
        }
	}

    void LateUpdate()
    {
        if(CanBeg())
        {
            begSign.SetActive(true);
        }
    }

    void Beg()
    {
        Instantiate(coinPrefab, hobo.transform.position + new Vector3(-2f, 0, 0), Quaternion.identity);
        begTimer = begCooldown;
    }

    bool CanBeg()
    {
        return ((m_hobodistance <= m_begradius) && (begTimer <= 0f));
    }
}
