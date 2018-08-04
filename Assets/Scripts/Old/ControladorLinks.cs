using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorLinks : MonoBehaviour {

	public Text linkActual;
	public string urlActual;
	public string[] linksNombre;
	public string[] linksURLs;
	private int contador=0;

	public void iniciar(){
		contador = 0;
		linkActual.text = "";
		if (linksNombre.Length > 0) {
			linkActual.text = linksNombre [contador];
			urlActual = linksURLs [contador];
		}
	}

	public void avanzar(){
		contador = (contador + 1) % linksNombre.Length;
		linkActual.text = linksNombre [contador];
		urlActual = linksURLs [contador];
	}

	public void retroceder(){
		if (contador == 0)
			contador = linksNombre.Length - 1;
		else
			contador = (contador - 1) % linksNombre.Length;
		linkActual.text = linksNombre [contador];
		urlActual = linksURLs [contador];
	}

	public void activarLinkActual(){
		Application.OpenURL (urlActual);
	}

}
