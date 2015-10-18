using UnityEngine;
using System.Collections;

public class ExposureLineRenderer : MonoBehaviour {

    public Transform leftAnchor;
    public Transform rightAnchor;

    public LineRenderer bglineRenderer;
    public LineRenderer filllineRenderer;

    public HoboExposure hExpose;

	// Use this for initialization
	void Start () {
        bglineRenderer.material = new Material (Shader.Find("Particles/Additive"));
        bglineRenderer.GetComponent<Renderer>().sortingLayerName = "UI";
        bglineRenderer.SetColors(Color.gray,Color.gray);

        filllineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        filllineRenderer.GetComponent<Renderer>().sortingLayerName = "UI";
        
	}
	
	// Update is called once per frame
	void Update () {
        bglineRenderer.SetPosition(0, leftAnchor.position);
        bglineRenderer.SetPosition(1, rightAnchor.position);

        filllineRenderer.SetPosition(0, leftAnchor.position);
        Vector3 pos = Vector3.Lerp(leftAnchor.position, rightAnchor.position, hExpose.exposure);
        filllineRenderer.SetPosition(1, pos);

        filllineRenderer.SetColors(Color.green, Color.blue);
	}
}
