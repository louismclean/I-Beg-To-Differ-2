using UnityEngine;
using System.Collections;

public struct Cost
{
    public int coinCost;
    public int woodCost;
    public int blanketCost;

    public Cost(int c, int w, int b)
    {
        coinCost = c;
        woodCost = w;
        blanketCost = b;
    }

    public string toString()
    {
        return coinCost + " coins, " + woodCost + " wood, and " + blanketCost + " blankets";
    }
}

public class House : MonoBehaviour {

    private resourceManager m_resourceManager;

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

    public Texture2D cost;

    public int[] FrameBlanketCost;
    public int[] FrameCoinCost;
    public int[] FrameWoodCost;

    public int[] MaterialBlanketCost;
    public int[] MaterialCoinCost;
    public int[] MaterialWoodCost;

    // Use this for initialization
	void Start () {

        m_resourceManager = GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<resourceManager>();
        for(int i=0; i<MAXFRAMELEVEL; i++)
        {
            Debug.Log("At frame level " + i + " a frame upgrade costs " + getFrameUpgradeCost(i).toString());
        }

        for(int frameLevel = 1; frameLevel < MAXFRAMELEVEL; frameLevel++)
        {
            for(int materialLevel = 1; materialLevel < MAXMATERIALLEVEL; materialLevel++)
            {
                Debug.Log("At frame level " + frameLevel + " and material level " + materialLevel + " a material upgrade costs " + getMaterialUpgradeCost(frameLevel, materialLevel).toString());
            }
        }
        

	}
	
	// Update is called once per frame
	void Update () {
        if(disableFadeTimer > 0f)
        {
            disableFadeTimer -= Time.deltaTime;
            houseFront.color = new Color(1f, 1f, 1f, Mathf.MoveTowards(houseFront.color.a, 1.0f, Time.deltaTime * fadeRate));            
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
            GUI.Label(new Rect(220, 170, 150, 50), cost);
        }

        else if (m_isInHouse)
        {
            // Make a background box
            GUI.Box(new Rect(200, 100, 250, 110), "Investment Advisor");

            if (frameLevel < MAXFRAMELEVEL)
            {
                if (GUI.Button(new Rect(220, 140, 200, 20), "Upgrade House Frame"))
                {
                    UpgradeHouseFrame();
                }
            }
            else
            {
                GUI.Label(new Rect(220, 140, 200, 20), "Max Frame Achieved!");
            }
            

            if(materialLevel < MAXMATERIALLEVEL)
            {
                if (GUI.Button(new Rect(220, 170, 200, 20), "Upgrade House Materials"))
                {
                    UpgradeHouseMaterials();
                }
            }
            else
            {
                GUI.Label(new Rect(220, 170, 200, 20), "Max Material Achieved!");
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
        Cost upgradeCost = getFrameUpgradeCost(frameLevel);
        if(m_resourceManager.spend(upgradeCost.coinCost, upgradeCost.woodCost, upgradeCost.blanketCost))
        {
            frameLevel++;
            materialLevel = 1;

            houseBack.sprite = houseBackSprite[frameLevel - 1];
            Debug.Log("array length: " + houseFrontSprite.Length);
            Debug.Log("frame level:" + frameLevel);
            Debug.Log("material level:" + materialLevel);
            Debug.Log("getting element: " + (((frameLevel - 1) * 3) + materialLevel - 1));

            houseFront.sprite = houseFrontSprite[((frameLevel - 1) * 3) + materialLevel - 1];

            //houseFront.color = new Color(1f, 1f, 1f, 1f);
            disableFadeTimer = 2f;
        }        
    }

    void UpgradeHouseMaterials()
    {
        Cost upgradeCost = getFrameUpgradeCost(frameLevel);
        if (m_resourceManager.spend(upgradeCost.coinCost, upgradeCost.woodCost, upgradeCost.blanketCost))
        {
            materialLevel++;

            houseFront.sprite = houseFrontSprite[((frameLevel - 1) * 3) + materialLevel - 1];

            //houseFront.color = new Color(1f, 1f, 1f, 1f);
            disableFadeTimer = 2f;
        }        
    }

    Cost getFrameUpgradeCost(int frameLevel)
    {
        int blanketCost = FrameBlanketCost[frameLevel];
        int coinCost = FrameCoinCost[frameLevel];
        int woodCost = FrameWoodCost[frameLevel];

        return new Cost(blanketCost, coinCost, woodCost);
    }

    Cost getMaterialUpgradeCost(int frameLevel, int materialLevel)
    {
        int blanketCost = MaterialBlanketCost[((frameLevel - 1) * 2) + materialLevel-1];
        int coinCost = MaterialCoinCost[((frameLevel - 1) * 2) + materialLevel-1];
        int woodCost = MaterialWoodCost[((frameLevel - 1) * 2) + materialLevel-1];

        return new Cost(blanketCost, coinCost, woodCost);
    }
}
