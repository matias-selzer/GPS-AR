using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReferenciasGraficas : MonoBehaviour {

	public Text titulo,descipcion, infoComoSoy,infoComoMeRelaciono,infoComoMeAdapto,infoSabiasQue, juegoPregunta,juegoRespuesta;
	public Text juegoOpciones;
	public Image juegoFoto;
	//public string [] fotos;
	//public string [] videos;
	//public string [] links;
	public GameObject imagenes;

	public GameObject btnJuego, btnImagenes, btnVideos, btnLinks, btnComoSoy, btnComoMeRelaciono, btnComoMeAdapto, btnSabiasQue;

	public void matchear(InformacionDelObjeto info){
		titulo.text = info.transform.name;
		//descipcion.text = info.descipcion;
		infoComoSoy.text = info.infoComoSoy;
		infoComoMeAdapto.text = info.infoComoMeAdapto;
		infoComoMeRelaciono.text = info.infoComoMeRelaciono;
		infoSabiasQue.text = info.infoSabiasQue;
		juegoPregunta.text = info.juegoPregunta;
		juegoRespuesta.text = info.juegoRespuesta;
		string op = "";
		for (int i = 0; i < info.juegoOpciones.Length; i++) {
			op +="("+(i+1)+") "+ info.juegoOpciones [i] + "\n";
		}
		juegoOpciones.text = op;
		juegoFoto.sprite = imagenes.transform.Find (info.juegoFoto).GetComponent<Image> ().sprite;
		GetComponent<ControladorFotos> ().fotos = info.fotos;
		GetComponent<ControladorFotos> ().iniciar ();
		//lo mismo para videos y links
		GetComponent<ControladorLinks>().linksNombre=info.linksNombres;
		GetComponent<ControladorLinks>().linksURLs=info.linksURLs;
		GetComponent<ControladorLinks> ().iniciar ();
	}

	public void checkearNulos(InformacionDelObjeto info){
		btnJuego.SetActive (true);
		btnImagenes.SetActive (true);
		//btnVideos.SetActive (true);
		btnLinks.SetActive (true);
		btnComoSoy.SetActive (true);
		btnComoMeRelaciono.SetActive (true);
		btnComoMeAdapto.SetActive (true);
		btnSabiasQue.SetActive (true);

		//Debug.Log ("hola");

		if (info.infoComoSoy == null)
			btnComoSoy.SetActive (false);
		if (info.infoComoMeRelaciono == null)
			btnComoMeRelaciono.SetActive (false);
		if (info.infoComoMeAdapto == null)
			btnComoMeAdapto.SetActive (false);
		if (info.infoSabiasQue == null)
			btnSabiasQue.SetActive (false);
		if (info.juegoPregunta == null)
			btnJuego.SetActive (false);
		if (info.fotos == null)
			btnImagenes.SetActive (false);
		if (info.videos== null)
			btnVideos.SetActive (false);
		if (info.linksNombres== null)
			btnLinks.SetActive (false);

	}
}
