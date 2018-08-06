using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicPOI : MonoBehaviour {

	public Image icon;
	public Text label;

	[HideInInspector]
	public GameObject panelInfo;

	[HideInInspector]
	public string infoText;

	void Start(){
		panelInfo = GameObject.Find ("PanelInfo");
	}

	public void UpdateIcon(Texture2D tex){
		icon.sprite = Sprite.Create (tex, new Rect (0.0f, 0.0f, tex.width, tex.height), new Vector2 (0.5f, 0.5f));
	}

	public void UpdatePosition(Vector3 newPosition, double distance){
		transform.position = newPosition;
		label.text = (int)(distance*1000) + " m";
	}

	public void showInformation(){
		Debug.Log ("hola");
		panelInfo.transform.Find ("Text").GetComponent<Text> ().text = infoText;
		panelInfo.SetActive (true);
	}
}
