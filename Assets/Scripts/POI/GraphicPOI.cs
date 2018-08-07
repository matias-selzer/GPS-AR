using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicPOI : MonoBehaviour {

	public Image icon;
	public Text label;

	[HideInInspector]
	public PanelInfoManager panelInfo;

	[HideInInspector]
	public string infoText;

	void Start(){
		panelInfo = Resources.FindObjectsOfTypeAll<PanelInfoManager>()[0];
		transform.GetChild(0).GetComponent<Canvas> ().worldCamera = GameObject.Find ("User").GetComponent<Camera> ();
	}

	public void UpdateIcon(Texture2D tex){
		icon.sprite = Sprite.Create (tex, new Rect (0.0f, 0.0f, tex.width, tex.height), new Vector2 (0.5f, 0.5f));
	}

	public void UpdatePosition(Vector3 newPosition, double distance){
		transform.position = newPosition;
        label.text = (int)(distance*1000) + " m";
	}

	public void showInformation(){
		panelInfo.info.text = infoText;
		panelInfo.gameObject.SetActive (true);
	}
}
