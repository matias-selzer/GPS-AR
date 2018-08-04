using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControladorObjetoEnElMundo : MonoBehaviour {

	public static int LIMITE1=200;
	public static int LIMITE2=30;
//	public static int LIMITE3=15;


	public GameObject nivel1,nivel2,nivel3,panelInfo;
	public double distancia;
	public ControladorLista contLista;

	public Text labelNivel1,labelNivel2;



	public GameObject menu;
	public bool prueba;
	public string nombrePrueba;

	private bool panelBreveDescripcionActivado,menuBotonesActivado,datosSeteados;




	public void setearDistObs(double m){
		if (!(prueba && transform.name.Equals (nombrePrueba))) {
			distancia = m;
			labelNivel1.text = ((int)(m)) + "m";
			labelNivel2.text = ((int)(m)) + "m";
		}
			actualizarNivelActual ();
		
	}

	public void actualizarNivelActual(){
		nivel1.SetActive (false);
		nivel2.SetActive (false);
		nivel3.SetActive (false);

		//prueba
		if(prueba && transform.name.Equals(nombrePrueba))distancia=5;

		if (distancia > LIMITE1) { //nivel 1 : esta muy lejos
			nivel1.SetActive (true);
			datosSeteados = false;
			menu.GetComponent<ControladorLista>().sacar(GetComponent<InformacionDelObjeto>());
			//desactivarMenuInteracciones ();
		} else if (distancia > LIMITE2) { //nivel 2: nos estamos acercando
			nivel2.SetActive (true);
			datosSeteados = false;
			menu.GetComponent<ControladorLista>().sacar(GetComponent<InformacionDelObjeto>());

			//desactivarMenuInteracciones ();
		} else { //nivel 3: estamos en el objeto
			nivel2.SetActive (true);
			//nivel3.SetActive (true);
			//activarMenuInteracciones ();
			if (!datosSeteados) {
				//menu.GetComponent<MenuManager> ().setearDatos (GetComponent<InformacionDelObjeto> ());
				menu.GetComponent<ControladorLista>().agregar(GetComponent<InformacionDelObjeto>());
				datosSeteados = true;
			}
			//menu.GetComponent<MenuManager> ().setearDatos ( GetComponent<InformacionDelObjeto> ().infoComoSoy, GetComponent<InformacionDelObjeto> ().juegoPregunta, GetComponent<InformacionDelObjeto> ().foto,GetComponent<InformacionDelObjeto> ().descipcion);
		}
	}

	//mensaje de descripcción nivel 2
	public void togglearPanelBreveDescripcion(){
		menu.GetComponent<MenuManager> ().setearNombreNivel2 (GetComponent<InformacionDelObjeto> ().nombre);
		menu.GetComponent<MenuManager> ().setearDescripcionNivel2 (GetComponent<InformacionDelObjeto> ().descipcion);
		//if(panelBreveDescripcionActivado) menu.GetComponent<MenuManager> ().desactivarPanelBreveDescripcion ();
		//else menu.GetComponent<MenuManager> ().activarPanelBreveDescripcion ();
		menu.GetComponent<MenuManager> ().activarPanelBreveDescripcion ();
		//panelBreveDescripcionActivado = !panelBreveDescripcionActivado;
	}

	//botones centrales nivel 2
	public void activarMenuInteracciones(){
		//if(menuBotonesActivado) menu.GetComponent<MenuManager> ().desactivarMenuInteracciones ();
		menu.GetComponent<MenuManager> ().activarMenuInteracciones ();
		//menuBotonesActivado = !menuBotonesActivado;
	}

	public void desactivarMenuInteracciones(){
		menu.GetComponent<MenuManager> ().desactivarMenuInteracciones ();
	}


}
