using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorLista : MonoBehaviour {

	public ArrayList puntos;
	public GameObject prefab,contenedor;
	ArrayList botones;

	// Use this for initialization
	void Start () {
		puntos = new ArrayList ();
		botones = new ArrayList ();
	}
	
	// Update is called once per frame
	void Update () {
		//contenedor.transform.se
	}


	public void agregar(InformacionDelObjeto info){
		if (!puntos.Contains (info)) {
			puntos.Add (info);
			GameObject nuevo =  Instantiate (prefab) as GameObject;
			botones.Add (nuevo);
			nuevo.transform.parent = contenedor.transform;
			nuevo.transform.name = info.name;
			string n = info.nombre;
			if (n.Length > 25) {
				n = n.Substring (0, 25) + "...";
			}
			nuevo.transform.GetChild (1).GetComponent<Text> ().text = n;
			nuevo.GetComponent<BotonPrefab> ().indice = puntos.Count - 1;
			nuevo.transform.Find ("Image").GetComponent<Image> ().sprite = GameObject.Find (info.nombre).transform.Find ("Nivel2").Find ("Button").GetComponent<Image> ().sprite;
		}
		if (puntos.Count == 1) {
			seleccionar (0);
			contenedor.SetActive (false);
		} else {
			contenedor.SetActive (true);
		}


	}

	public void sacar(InformacionDelObjeto info){
		if (puntos.Contains (info)) {
			puntos.Remove (info);
			GameObject removido = contenedor.transform.Find (info.name).gameObject;
			botones.Remove (removido);
			Destroy(removido);
			if (puntos.Count < 1) {
				GetComponent<MenuManager> ().desactivarMenuInteracciones ();
			}
			if (puntos.Count == 1) {
				seleccionar (0);
				contenedor.SetActive (false);
			}else {
				contenedor.SetActive (true);
			}
		}

	}


	public void seleccionar(int indice){
		for (int i = 0; i < puntos.Count; i++) {
			((GameObject)botones [i]).GetComponent<Button> ().interactable = true;
		}
		((GameObject)botones [indice]).GetComponent<Button> ().interactable = false;
		GetComponent<MenuManager> ().activarMenuInteracciones ();
		GetComponent<MenuManager> ().setearDatos ((InformacionDelObjeto)puntos[indice]);
	}
}
