﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class XMLEditorDataLoader : DataLoader {

	public string xmlText;
	[HideInInspector]
	public int cantIconTypes;

	public override void LoadData(Dictionary<string,Texture2D> iconTypes,List<POI> pois){
		XmlDocument doc = new XmlDocument ();
		doc.LoadXml (xmlText);
		XmlNode rootNode = doc.SelectSingleNode("/Documment/IconTypes");
		LoadIconTypes (iconTypes,rootNode);
		rootNode = doc.SelectSingleNode ("/Documment/Data");
		LoadPOIs (pois,rootNode);
	}

	void LoadPOIs(List<POI> pois,XmlNode rootNode){
		foreach (XmlNode poiNode in rootNode.SelectNodes("POI")) {
			POI newPoi = new POI ();
			foreach (XmlNode nameNode in poiNode.SelectNodes("Name")) {
				newPoi.name = nameNode.InnerText;
			}
			/*foreach (XmlNode infoNode in poiNode.SelectNodes("Info")) {
				newPoi.info = infoNode.InnerText;
			}*/
			foreach (XmlNode iconTypeNode in poiNode.SelectNodes("IconType")) {
				newPoi.iconType = iconTypeNode.InnerText;
			}
			foreach (XmlNode locationNode in poiNode.SelectNodes("Location")) {
				newPoi.location = new POILocation ();
				foreach (XmlNode latNode in locationNode.SelectNodes("Lat")) {
					double lat = 0;
					double.TryParse (latNode.InnerText,out lat);
					newPoi.location.lat =lat;
				}
				foreach (XmlNode lonNode in locationNode.SelectNodes("Lon")) {
					double lon = 0;
					double.TryParse (lonNode.InnerText, out lon);
					newPoi.location.lon = lon;
				}
			}
			List<Category> categories = new List<Category> ();
			foreach (XmlNode categoriesNode in poiNode.SelectNodes("Categories")) {
				foreach (XmlNode categoryNode in categoriesNode.SelectNodes("Category")) {
					Category newCategory = new Category ();
					foreach (XmlNode iconTypeNode in categoryNode.SelectNodes("IconType")) {
						newCategory.iconType = iconTypeNode.InnerText;
					}
					foreach (XmlNode contentNode in categoryNode.SelectNodes("Content")) {
						foreach (XmlNode textContentNode in contentNode.SelectNodes("TextContent")) {
							newCategory.contents.Add (new TextContent (textContentNode.InnerText));
						}
					}
					categories.Add (newCategory);
				}
			}
			newPoi.categories = categories;

			pois.Add (newPoi);
		}
	}




	void LoadIconTypes(Dictionary<string,Texture2D> iconTypes,XmlNode rootNode){
		cantIconTypes = rootNode.SelectNodes ("IconType").Count;
		foreach (XmlNode iconTypeNode in rootNode.SelectNodes("IconType")) {
			string name = "";
			foreach (XmlNode nameNode in iconTypeNode.SelectNodes("Name")) {
				name = nameNode.InnerText;
			}
			foreach (XmlNode imageURLNode in iconTypeNode.SelectNodes("ImageURL")) {
				StartCoroutine (DownloadTexture (iconTypes,name, imageURLNode.InnerText));
			}
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
		}
	}
}
