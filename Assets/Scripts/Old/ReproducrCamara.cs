using UnityEngine;
using System.Collections;

public class ReproducrCamara : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		WebCamTexture wct= new WebCamTexture();
		Renderer renderer = GetComponent<Renderer> ();
		renderer.material.mainTexture = wct;
		wct.Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
