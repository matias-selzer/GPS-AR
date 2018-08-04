using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NombreIgualAFoto : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.name = GetComponent<Image> ().sprite.name;
	}
	

}
