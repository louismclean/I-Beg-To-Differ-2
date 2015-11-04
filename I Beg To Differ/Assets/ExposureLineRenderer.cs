using UnityEngine;
using System.Collections;

public class ExposureLineRenderer : MonoBehaviour {

    public LineRenderer bglineRenderer;
    public LineRenderer filllineRenderer;

    public HoboExposure hExpose;

    Vector3 leftAnchor = new Vector3(-3f, 0f, 0f);
    Vector3 rightAnchor = new Vector3(3f, 0f, 0f);

	// Use this for initialization
	void Start () {
        //bglineRenderer.material = new Material (Shader.Find("Particles/Additive"));
        //bglineRenderer.material = new Material(Shader.Find("Sprites-Default"));
        bglineRenderer.GetComponent<Renderer>().sortingLayerName = "UI";
        bglineRenderer.GetComponent<Renderer>().sortingOrder = 0;
        //bglineRenderer.SetColors(Color.gray,Color.gray);

        //filllineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        //filllineRenderer.material = new Material(Shader.Find("Sprites-Default"));
        filllineRenderer.GetComponent<Renderer>().sortingLayerName = "UI";
        filllineRenderer.GetComponent<Renderer>().sortingOrder = 1;

        bglineRenderer.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(1, 1, 1, 1f));
        filllineRenderer.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(1, 1, 1, 1f));
        
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = Vector3.Lerp(leftAnchor, rightAnchor, hExpose.exposure);

        bglineRenderer.SetPosition(0, pos);
        bglineRenderer.SetPosition(1, rightAnchor);

        filllineRenderer.SetPosition(0, leftAnchor);
        filllineRenderer.SetPosition(1, pos);

        filllineRenderer.SetColors(Color.green, Color.blue);
	}

    void fixedUpdate()
    {
        /*
        Vector3 pos = Vector3.Lerp(leftAnchor.position, rightAnchor.position, hExpose.exposure);

        bglineRenderer.SetPosition(0, pos);
        bglineRenderer.SetPosition(1, rightAnchor.position);

        filllineRenderer.SetPosition(0, leftAnchor.position);
        filllineRenderer.SetPosition(1, pos);

        filllineRenderer.SetColors(Color.green, Color.blue);
         */
    }
}
