using UnityEngine;
using System.Collections;

public class House : MonoBehaviour {

    public SpriteRenderer houseFront;
    public SpriteRenderer houseBack;
   
    private float fadeRate = 2.0f;
    private float disableFadeTimer = 0f;

    private bool m_isInHouse;
    private float m_alphaTarget;

    public GUIStyle MenuGUIStyle;

    public Sprite[] houseFrontSprite;
    public Sprite[] houseBackSprite;

    public int frameLevel = 0;
    public int materialLevel = 0;

    public static int MAXFRAMELEVEL = 4;
    public static int MAXMATERIALLEVEL = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(disableFadeTimer > 0f)
        {
            disableFadeTimer -= Time.deltaTime;
            Debug.Log(disableFadeTimer);            
        }
        else
        {
            houseFront.color = new Color(1f, 1f, 1f, Mathf.MoveTowards(houseFront.color.a, m_alphaTarget, Time.deltaTime * fadeRate));
        }
	}

    void OnGUI()
    {
        if (m_isInHouse && frameLevel == 0)
        {
            // Make a background box
            GUI.Box(new Rect(200, 100, 250, 110), "Investment Advisor");

            if (GUI.Button(new Rect(220, 140, 200, 20), "Build a shack out of blankets"))
            {
                UpgradeHouseFrame();
            }
        }

        else if (m_isInHouse)
        {
            // Make a background box
            GUI.Box(new Rect(200, 100, 250, 110), "Investment Advisor");


            if (GUI.Button(new Rect(220, 140, 200, 20), "Upgrade House Frame"))
            {
                UpgradeHouseFrame();
            }


            if (GUI.Button(new Rect(220, 170, 200, 20), "Upgrade House Materials"))
            {
                UpgradeHouseMaterials();
            }
        }    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.SendMessage("SetIsInHouse", true);
            m_isInHouse = true;
            m_alphaTarget = 0.0f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.SendMessage("SetIsInHouse", false);
            m_isInHouse = false;
            m_alphaTarget = 1f;
        }
    }

    void UpgradeHouseFrame()
    {
        frameLevel++;
        materialLevel = 1;

        houseBack.sprite = houseBackSprite[frameLevel-1];
        Debug.Log("array length: " + houseFrontSprite.Length);
        Debug.Log("frame level:" + frameLevel);
        Debug.Log("material level:" + materialLevel);
        Debug.Log("getting element: " + (((frameLevel - 1) * 3) + materialLevel-1));

        houseFront.sprite = houseFrontSprite[((frameLevel - 1) * 3) + materialLevel-1];

        //houseFront.color = new Color(1f, 1f, 1f, 1f);
        //disableFadeTimer = 2f;
    }

    void UpgradeHouseMaterials()
    {
        materialLevel++;
        
        houseFront.sprite = houseFrontSprite[((frameLevel - 1) * 3)+materialLevel-1];
        
        //houseFront.color = new Color(1f, 1f, 1f, 1f);
        //disableFadeTimer = 2f;
    }
}
