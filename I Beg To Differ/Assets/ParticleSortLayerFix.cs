using UnityEngine;
using System.Collections;

public class ParticleSortLayerFix : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Foreground";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
