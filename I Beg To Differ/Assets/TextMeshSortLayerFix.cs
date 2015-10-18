using UnityEngine;
using System.Collections;

public class TextMeshSortLayerFix : MonoBehaviour {

    public int sortOrder = 1;

	// Use this for initialization
	void Start () {
        GetComponent<TextMesh>().GetComponent<Renderer>().sortingLayerName = "UI";
        GetComponent<TextMesh>().GetComponent<Renderer>().sortingOrder = sortOrder;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
