using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonPrefab : MonoBehaviour {

	public int indice;

	public void activar(){
		GameObject.Find ("Menu").GetComponent<ControladorLista> ().seleccionar (indice);
	}
}
