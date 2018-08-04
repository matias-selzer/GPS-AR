using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class DataManager : MonoBehaviour {

	public struct IconType {
		public string name;
		public Sprite sprite;
	}

	public struct POIData {
		public string name;
		public string info;
		public string iconType;
		public string lat;
		public string lon;
	}

	public string xmlText;

	private Dictionary<string,Sprite> iconTypes;
	private ArrayList POIs;

	public Image imageTest;

	// Use this for initialization
	void Start () {
		iconTypes = new Dictionary<string,Sprite> ();
		POIs = new ArrayList ();
		LoadData ();	
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
		Debug.Log (rootNode == null);
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
		}
	}

	void LoadIconTypes(XmlNode rootNode){
		foreach (XmlNode iconTypeNode in rootNode.SelectNodes("IconType")) {
			IconType newIconType = new IconType ();
			foreach (XmlNode nameNode in iconTypeNode.SelectNodes("Name")) {
				newIconType.name = nameNode.InnerText;
			}
			foreach (XmlNode imageURLNode in iconTypeNode.SelectNodes("ImageURL")) {

				StartCoroutine (ApplyTexture (newIconType, imageURLNode.InnerText));
			}

			iconTypes.Add (newIconType.name, newIconType.sprite);
		}
	}

	IEnumerator ApplyTexture(IconType icon,string URL)
	{
		Texture2D tex;
		tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
		using (WWW www = new WWW(URL))
		{
			yield return www;
			www.LoadImageIntoTexture(tex);
			icon.sprite=Sprite.Create(tex,new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
			imageTest.sprite = icon.sprite;
		}
	}
}
