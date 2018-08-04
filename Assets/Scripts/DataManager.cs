using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class DataManager : MonoBehaviour {

	public struct IconType {
		public string name;
		public Texture2D sprite;
	}

	public struct POIData {
		public string name;
		public string info;
		public string iconType;
		public string lat;
		public string lon;
	}

	public string xmlText;
	public GameObject poiPrefab;

	private Dictionary<string,Texture2D> iconTypes;
	private ArrayList POIs;
	private ArrayList poiObjects;
	private int cantIconTypes;

	// Use this for initialization
	void Start () {
		iconTypes = new Dictionary<string,Texture2D> ();
		POIs = new ArrayList ();
		poiObjects = new ArrayList ();
		LoadData ();	
		InstantiatePOIs ();
		InvokeRepeating ("UpdateIconTypes", 0, 1);
	}

	void UpdateIconTypes(){
		if (iconTypes.Count >= cantIconTypes) {
			for (int i = 0; i < poiObjects.Count; i++) {
				Texture2D tex = iconTypes [((POIData)(POIs [i])).iconType];
				((GameObject)poiObjects[i]).transform.GetChild (0).GetChild (1).GetComponent<Image> ().sprite = Sprite.Create (tex, new Rect (0.0f, 0.0f, tex.width, tex.height), new Vector2 (0.5f, 0.5f));
			}
			CancelInvoke ("UpdateIconTypes");
		}
	}

	void InstantiatePOIs(){
		for (int i = 0; i < POIs.Count; i++) {
			Vector3 poiFinalPosition = CalculatePoiPosition (double.Parse (((POIData)(POIs [i])).lat), double.Parse (((POIData)(POIs [i])).lon));
			GameObject newPoi = Instantiate (poiPrefab) as GameObject;
			newPoi.transform.position = poiFinalPosition;
			//newPoi.transform.GetChild (0).GetChild (1).GetComponent<Image> ().sprite = iconTypes [((POIData)(POIs [i])).iconType];
			Debug.Log("hola + "+((POIData)(POIs [i])).iconType);
			poiObjects.Add (newPoi);

		}
	}

	Vector3 CalculatePoiPosition(double lat,double lon){
		Vector3 newPos=new Vector3(0,0,0);

		PosicionGPS posicionTarget = new PosicionGPS (lat, lon);
		PosicionGPS posicionBase = new PosicionGPS (UserLocation.lat, UserLocation.lon);

		// Creo una posicion auxiliar para luego calcular la distancia en x e y
		PosicionGPS nuevaY = new PosicionGPS (posicionTarget.lat, posicionBase.lon);
		PosicionGPS nuevaX = new PosicionGPS (posicionBase.lat, posicionTarget.lon);
		double posX = PosicionGPS.calcularDistancia (posicionBase, nuevaX);
		double posY = PosicionGPS.calcularDistancia (posicionBase, nuevaY);

		if (posicionTarget.lat < posicionBase.lat)
			posY *= -1.0;

		if (posicionTarget.lon < posicionBase.lon)
			posX *= -1.0;

		newPos.x = (float) (posX*1000);
		newPos.z = (float) (posY*1000);

		return newPos;
	}
	

	void LoadData(){
		XmlDocument doc = new XmlDocument ();
		doc.LoadXml (xmlText);
		XmlNode rootNode = doc.SelectSingleNode("/Documment/IconTypes");
		LoadIconTypes (rootNode);
		rootNode = doc.SelectSingleNode ("/Documment/Data");
		LoadPOIs (rootNode);
	}

	void LoadPOIs(XmlNode rootNode){
		foreach (XmlNode poiNode in rootNode.SelectNodes("POI")) {
			POIData newPoi = new POIData ();
			foreach (XmlNode nameNode in poiNode.SelectNodes("Name")) {
				newPoi.name = nameNode.InnerText;
			}
			foreach (XmlNode infoNode in poiNode.SelectNodes("Info")) {
				newPoi.info = infoNode.InnerText;
			}
			foreach (XmlNode iconTypeNode in poiNode.SelectNodes("IconType")) {
				newPoi.iconType = iconTypeNode.InnerText;
			}
			foreach (XmlNode locationNode in poiNode.SelectNodes("Location")) {
				foreach (XmlNode latNode in locationNode.SelectNodes("Lat")) {
					newPoi.lat = latNode.InnerText;
				}
				foreach (XmlNode lonNode in locationNode.SelectNodes("Lon")) {
					newPoi.lon = lonNode.InnerText;
				}
			}
			POIs.Add (newPoi);
		}
	}

	void LoadIconTypes(XmlNode rootNode){
		cantIconTypes = rootNode.SelectNodes ("IconType").Count;
		foreach (XmlNode iconTypeNode in rootNode.SelectNodes("IconType")) {
			IconType newIconType = new IconType ();
			foreach (XmlNode nameNode in iconTypeNode.SelectNodes("Name")) {
				newIconType.name = nameNode.InnerText;
			}
			foreach (XmlNode imageURLNode in iconTypeNode.SelectNodes("ImageURL")) {

				StartCoroutine (DownloadTexture (iconTypes,newIconType.name, imageURLNode.InnerText));
			}

			//iconTypes.Add (newIconType.name, newIconType.sprite);
		}
	}

	IEnumerator DownloadTexture(Dictionary<string,Texture2D> dic, string name,string URL)
	{
		Texture2D tex;
		tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
		using (WWW www = new WWW(URL))
		{
			yield return www;
			www.LoadImageIntoTexture(tex);
			dic.Add (name, tex);
			Debug.Log ("se agrego " + name);
			//icon.sprite=Sprite.Create(tex,new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
			//test = icon.sprite;
		}
	}
}
