using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInfoManager : MonoBehaviour {

	public Text info;
	public Text title;

	public GameObject categoryButtonPrefab;
	public GameObject categoriesContentPanel;

	public void InstantiateNewCategoryButton(Texture2D tex){
		GameObject newButton = Instantiate (categoryButtonPrefab) as GameObject;
		newButton.GetComponent<Image>().sprite = Sprite.Create (tex, new Rect (0.0f, 0.0f, tex.width, tex.height), new Vector2 (0.5f, 0.5f));
		newButton.transform.parent = categoriesContentPanel.transform;
	}
}
