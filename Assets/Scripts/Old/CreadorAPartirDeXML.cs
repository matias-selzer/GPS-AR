using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class CreadorAPartirDeXML : MonoBehaviour {

	public GameObject objetoPrefab;
	public GameObject persona,menu,manager;
	public Sprite imagenPlanta, imagenCangrejo,imagenDelfin,imagenAve,imagenHombre,imagenPaisaje;
	public string xml;

	// Use this for initialization
	void Start () {
		cargarDatos ();
	}


	void cargarDatos(){



		XmlDocument doc = new XmlDocument ();
		doc.LoadXml (xml);
		//mostrar (doc.SelectSingleNode("/Datos"));
		XmlNode inicial = doc.SelectSingleNode("/Datos");

		ArrayList objetos = new ArrayList ();
		//GameObject[] objetos = new GameObject[ inicial.ChildNodes.Count];
		int o = 0;

	//	Debug.Log (inicial.ChildNodes.Count);

		//por cada punto de interes
		foreach (XmlNode puntoDeInteres in inicial.ChildNodes) {
			
			if (puntoDeInteres.Name != "#text") {
				GameObject nuevoPunto = Instantiate (objetoPrefab) as GameObject;
				objetos.Add( nuevoPunto);
				o++;
				nuevoPunto.GetComponent<SeguirCamara> ().target = persona;
				nuevoPunto.GetComponent<ControladorObjetoEnElMundo> ().menu = menu;
				nuevoPunto.GetComponent<ControladorObjetoEnElMundo> ().panelInfo = menu.transform.Find ("PanelBreveDescripcion").gameObject;
				//asigno el nombre
				if (puntoDeInteres.SelectSingleNode ("Nombre") != null) {
					nuevoPunto.transform.name = puntoDeInteres.SelectSingleNode ("Nombre").InnerText;
					nuevoPunto.GetComponent<InformacionDelObjeto> ().nombre = nuevoPunto.transform.name;
				}
				//asigno la descripcion
				if (puntoDeInteres.SelectSingleNode ("Descripcion") != null)
					nuevoPunto.GetComponent<InformacionDelObjeto> ().descipcion = puntoDeInteres.SelectSingleNode ("Descripcion").InnerText;
				//asigno como soy
				if (puntoDeInteres.SelectSingleNode ("ComoSoy") != null)
					nuevoPunto.GetComponent<InformacionDelObjeto> ().infoComoSoy = puntoDeInteres.SelectSingleNode ("ComoSoy").InnerText;
				//asigmo como me adapto
				if (puntoDeInteres.SelectSingleNode ("ComoMeAdapto") != null)
					nuevoPunto.GetComponent<InformacionDelObjeto> ().infoComoMeAdapto = puntoDeInteres.SelectSingleNode ("ComoMeAdapto").InnerText;
				//asigno como me relaciono
				if (puntoDeInteres.SelectSingleNode ("ComoMeRelaciono") != null)
					nuevoPunto.GetComponent<InformacionDelObjeto> ().infoComoMeRelaciono = puntoDeInteres.SelectSingleNode ("ComoMeRelaciono").InnerText;
				//asigno sabias que
				if (puntoDeInteres.SelectSingleNode ("SabiasQue") != null)
					nuevoPunto.GetComponent<InformacionDelObjeto> ().infoSabiasQue = puntoDeInteres.SelectSingleNode ("SabiasQue").InnerText;

				if (puntoDeInteres.SelectSingleNode ("Juego") != null) {
					//asigno juego pregunta
					nuevoPunto.GetComponent<InformacionDelObjeto> ().juegoPregunta = puntoDeInteres.SelectSingleNode ("Juego").SelectSingleNode ("Pregunta").InnerText;
					//asigno juego respuesta
					nuevoPunto.GetComponent<InformacionDelObjeto> ().juegoRespuesta = puntoDeInteres.SelectSingleNode ("Juego").SelectSingleNode ("Respuesta").InnerText;
					//asigno la foto del juego
					if (puntoDeInteres.SelectSingleNode ("Juego").SelectSingleNode ("Foto") != null)
						nuevoPunto.GetComponent<InformacionDelObjeto> ().juegoFoto = puntoDeInteres.SelectSingleNode ("Juego").SelectSingleNode ("Foto").InnerText;
					//asigno las opciones de la pregunta
					int cantidadDeOpciones = puntoDeInteres.SelectSingleNode ("Juego").SelectSingleNode ("Opciones").ChildNodes.Count;
					string[] opciones = new string[cantidadDeOpciones];
					int pos = 0;
					foreach (XmlNode op in puntoDeInteres.SelectSingleNode("Juego").SelectSingleNode("Opciones").ChildNodes) {
						opciones [pos] = op.InnerText;
						pos++;
					}
					nuevoPunto.GetComponent<InformacionDelObjeto> ().juegoOpciones = opciones;
				}

				//asigno fotos
				if (puntoDeInteres.SelectSingleNode ("Fotos") != null) {
					int cantidadFotos = puntoDeInteres.SelectSingleNode ("Fotos").ChildNodes.Count;
					string[] opcionesFotos = new string[cantidadFotos];
					int posF = 0;
					foreach (XmlNode op in puntoDeInteres.SelectSingleNode("Fotos").ChildNodes) {
						opcionesFotos [posF] = op.InnerText;
						posF++;
					}
					nuevoPunto.GetComponent<InformacionDelObjeto> ().fotos = opcionesFotos;
				}

				//asigno videos
				if (puntoDeInteres.SelectSingleNode ("Videos") != null) {
					int cantidadVideos = puntoDeInteres.SelectSingleNode ("Videos").ChildNodes.Count;
					string[] opcionesVideos = new string[cantidadVideos];
					int posV = 0;
					foreach (XmlNode op in puntoDeInteres.SelectSingleNode("Videos").ChildNodes) {
						opcionesVideos [posV] = op.InnerText;
						posV++;
					}
					nuevoPunto.GetComponent<InformacionDelObjeto> ().videos = opcionesVideos;
				}

				//asigno links
				if (puntoDeInteres.SelectSingleNode ("Links") != null) {
					int cantidadLinks = puntoDeInteres.SelectSingleNode ("Links").ChildNodes.Count;
					string[] opcionesLinksNombres = new string[cantidadLinks];
					string[] opcionesLinksURLs = new string[cantidadLinks];
					int posL = 0;
					foreach (XmlNode link in puntoDeInteres.SelectSingleNode("Links").ChildNodes) {
						opcionesLinksNombres [posL] = link.SelectSingleNode ("Nombre").InnerText;
						opcionesLinksURLs [posL] = link.SelectSingleNode ("Url").InnerText;
						posL++;
					}
					nuevoPunto.GetComponent<InformacionDelObjeto> ().linksNombres = opcionesLinksNombres;
					nuevoPunto.GetComponent<InformacionDelObjeto> ().linksURLs = opcionesLinksURLs;
				}

				//asigno coordanadas GPS
				if (puntoDeInteres.SelectSingleNode ("CoordenadaGPS") != null) {
					nuevoPunto.GetComponent<PosicionGPS> ().lat = double.Parse (puntoDeInteres.SelectSingleNode ("CoordenadaGPS").SelectSingleNode ("Latitud").InnerText);
					nuevoPunto.GetComponent<PosicionGPS> ().lon = double.Parse (puntoDeInteres.SelectSingleNode ("CoordenadaGPS").SelectSingleNode ("Longitud").InnerText);
				}

				//asigno iconos de categoria
				if (puntoDeInteres.SelectSingleNode ("Categoria") != null) {
					string categoria = puntoDeInteres.SelectSingleNode ("Categoria").InnerText;
					if (categoria.Equals ("Hombre")) {
						nuevoPunto.transform.Find ("Nivel2").Find ("Button").GetComponent<Image> ().sprite = imagenHombre;
					} 
					else if (categoria.Equals ("Cangrejo")) {
						nuevoPunto.transform.Find ("Nivel2").Find ("Button").GetComponent<Image> ().sprite = imagenCangrejo;
					} 
					else if (categoria.Equals ("Delfin")) {
						nuevoPunto.transform.Find ("Nivel2").Find ("Button").GetComponent<Image> ().sprite = imagenDelfin;
					} 
					else if (categoria.Equals ("Ave")) {
						nuevoPunto.transform.Find ("Nivel2").Find ("Button").GetComponent<Image> ().sprite = imagenAve;
					}
					else if (categoria.Equals ("Paisaje")) {
						nuevoPunto.transform.Find ("Nivel2").Find ("Button").GetComponent<Image> ().sprite = imagenPaisaje;
					}
					else {
						//nuevoPunto.transform.Find ("Nivel2").Find ("Button").GetComponent<Image> ().sprite = imagenPlanta;
						StartCoroutine (CheckTexture (nuevoPunto));
					}
				}




			}

			//agrego los objetos al manager

			manager.GetComponent<Manager> ().setearObjetosYComenzar (((GameObject[])(  objetos.ToArray(typeof(GameObject)))));
		}
	}

	IEnumerator CheckTexture(GameObject np)
	{
		string url = "https://vignette.wikia.nocookie.net/es.pokemon/images/5/5d/Pikachu_%28anime_DP%29.png/revision/latest?cb=20120906045146";
		Texture2D tex;
		tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
		using (WWW www = new WWW(url))
		{
			yield return www;
			www.LoadImageIntoTexture(tex);
			np.transform.Find ("Nivel2").Find ("Button").GetComponent<Image> ().sprite=Sprite.Create(tex,new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
		}
	}

	void mostrar(XmlNode n){
		foreach (XmlNode n2 in n.ChildNodes) {
			Debug.Log (n2.Name);
			mostrar (n2);
		}
	}
}
