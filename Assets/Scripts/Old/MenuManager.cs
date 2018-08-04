using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public GameObject panelBreveDescripcion, menuInteracciones, menuInformacion,menuMultimedia,menuJuego,panelMapa;


	public void activarPanelBreveDescripcion(){
		panelBreveDescripcion.SetActive (true);
	}

	public void desactivarPanelBreveDescripcion(){
		panelBreveDescripcion.SetActive (false);
	}

	public void activarMenuInteracciones(){
		menuInteracciones.SetActive (true);
	}

	public void desactivarMenuInteracciones(){
		menuInteracciones.SetActive (false);
		//Debug.Log ("pepe");
	}

	public void activarMenuInformacion(){
		menuInformacion.SetActive (true);
	}

	public void desactivarMenuInformacion(){
		menuInformacion.SetActive (false);
	}

	public void activarMenuMultimedia(){
		menuMultimedia.SetActive (true);
	}

	public void desactivarMenuMultimedia(){
		menuMultimedia.SetActive (false);
	}

	public void activarMenuJuego(){
		menuJuego.SetActive (true);
	}

	public void desactivarMenuJuego(){
		menuJuego.SetActive (false);
	}

	public void activarPanelMapa(){
		panelMapa.SetActive (true);
	}

	public void desactivarMenues(){
		desactivarMenuInformacion ();
		desactivarMenuMultimedia ();
		desactivarMenuJuego ();
		//Debug.Log ("hola");
	}



	public void setearDatos(InformacionDelObjeto info){
		GetComponent<ReferenciasGraficas> ().matchear (info);
		GetComponent<ReferenciasGraficas> ().checkearNulos (info);
	}

	public void setearDescripcionNivel2(string d){
		panelBreveDescripcion.transform.Find ("Desc").GetComponent<Text> ().text = d;
	}

	public void setearNombreNivel2(string n){
		panelBreveDescripcion.transform.Find ("Nombre").GetComponent<Text> ().text = n;
	}

	public void cerrarAplicacíon() {
		Application.Quit ();

	}
}
