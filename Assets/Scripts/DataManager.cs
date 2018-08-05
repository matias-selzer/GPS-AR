using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class DataManager : MonoBehaviour {

	public GameObject poiPrefab;

	private Dictionary<string,Texture2D> iconTypes;
	private List<POI> POIs;
	private DataLoader dataLoader;
	private POILocationManager poiLocationManager;

	// Use this for initialization
	void Start () {
		iconTypes = new Dictionary<string,Texture2D> ();
		POIs = new List<POI> ();
		dataLoader = GetComponent<DataLoader> ();
		poiLocationManager = new POILocationManager ();
		dataLoader.LoadData (iconTypes,POIs);	
		InstantiatePOIs ();
		InvokeRepeating ("UpdateIconTypes", 0, 0.1f);
		InvokeRepeating ("UpdateLocations", 0, 1f);
	}


	void UpdateIconTypes(){
		if (iconTypes.Count >= ((XMLEditorDataLoader)dataLoader).cantIconTypes) {
			for (int i = 0; i < POIs.Count; i++) {
				Texture2D tex = iconTypes [POIs [i].iconType];
				POIs [i].UpdateIcon (tex);
			}
			CancelInvoke ("UpdateIconTypes");
		}
	}


	void InstantiatePOIs(){
		for (int i = 0; i < POIs.Count; i++) {
			GameObject newPoi = Instantiate (poiPrefab) as GameObject;
			POIs[i].graphicPOI=newPoi.GetComponent<GraphicPOI>();
		}
	}


	public void UpdateLocations(){
		UserLocation.singleton.UpdateLocation ();
		Location userLocation = UserLocation.singleton;

		for (int i = 0; i < POIs.Count; i++) {
			Vector3 newPosition=poiLocationManager.UpdatePosition (userLocation, POIs [i].location);
			POIs [i].UpdatePosition (newPosition);
		}
	}


}
