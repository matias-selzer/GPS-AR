using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorFotos : MonoBehaviour {

	public Image imagen;
	public string[] fotos;
	private int contador=0;
	public GameObject catalogoFotos;

	public void iniciar(){
		contador = 0;
		//Debug.Log (fotos [contador]);
		imagen.sprite = catalogoFotos.transform.Find (fotos [contador]).GetComponent<Image> ().sprite;
	}

	public void avanzar(){
		contador = (contador + 1) % fotos.Length;
		imagen.sprite = catalogoFotos.transform.Find (fotos [contador]).GetComponent<Image> ().sprite;
	}

	public void retroceder(){
		if (contador == 0)
			contador = fotos.Length - 1;
		else
			contador = (contador - 1) % fotos.Length;
		imagen.sprite = catalogoFotos.transform.Find (fotos [contador]).GetComponent<Image> ().sprite;
	}

}
