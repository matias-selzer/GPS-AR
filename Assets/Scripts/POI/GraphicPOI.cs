using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicPOI : MonoBehaviour {

	public Image icon;
	public Text distanceLabel;

	[HideInInspector]
	public PanelInfoManager panelInfo;

	[HideInInspector]
	public POI myPOI;


	void Start(){
		panelInfo = Resources.FindObjectsOfTypeAll<PanelInfoManager>()[0];
		transform.GetChild(0).GetComponent<Canvas> ().worldCamera = GameObject.Find ("User").GetComponent<Camera> ();
	}

	public void UpdateIcon(Texture2D tex){
		icon.sprite = Sprite.Create (tex, new Rect (0.0f, 0.0f, tex.width, tex.height), new Vector2 (0.5f, 0.5f));
	}

	public void UpdatePosition(Vector3 newPosition, double distance){
		transform.position = newPosition;
        distanceLabel.text = (int)(distance*1000) + " m";
	}

	public void showInformation(){
		panelInfo.info.text = myPOI.info;
		panelInfo.title.text = myPOI.name;

		foreach (Category cat in myPOI.categories) {
			Texture2D tex = IconsRepository.iconTypes [cat.iconType];
			panelInfo.InstantiateNewCategoryButton (tex);
		}

		panelInfo.gameObject.SetActive (true);
	}
}
