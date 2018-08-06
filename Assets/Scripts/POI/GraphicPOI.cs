using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicPOI : MonoBehaviour {

	public Image icon;
	public Text label;

	public void UpdateIcon(Texture2D tex){
		icon.sprite = Sprite.Create (tex, new Rect (0.0f, 0.0f, tex.width, tex.height), new Vector2 (0.5f, 0.5f));
	}

	public void UpdatePosition(Vector3 newPosition){
		transform.position = newPosition;
	}
}
