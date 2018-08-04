using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSwipe : MonoBehaviour {

	private Vector3 mousePosition;
	public ControladorFotos controladorFotos;

	public void beginDrag(){
		mousePosition = Input.mousePosition;
	}

	public void endDrag(){
		if (Input.mousePosition.x < mousePosition.x)
			controladorFotos.retroceder ();
		else
			controladorFotos.avanzar ();
	}
}
