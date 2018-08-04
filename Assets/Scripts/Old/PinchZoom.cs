using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinchZoom : MonoBehaviour {

	public GameObject canvas; // The canvas
	public float zoomSpeed = 0.5f;        // The rate of change of the canvas scale factor

	void Update()
	{


	}

	public void aumentar(){
		canvas.transform.localScale*=1.5f;;
	}

	public void disminuir(){
		if(canvas.transform.localScale.z>1)
			canvas.transform.localScale/=1.5f;

	}
}
