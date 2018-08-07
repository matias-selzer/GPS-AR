using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInfoManager : MonoBehaviour {

	public Text info;
	public Text title;

	public GameObject categoryButtonPrefab;
	public GameObject categoriesContentPanel;

	public GameObject textContentPanelPrefab;
	public GameObject panelContent;
	public Scrollbar scrollbar;

	public void InstantiateNewCategoryButton(Texture2D tex){
		GameObject newButton = Instantiate (categoryButtonPrefab) as GameObject;
		newButton.GetComponent<Image>().sprite = Sprite.Create (tex, new Rect (0.0f, 0.0f, tex.width, tex.height), new Vector2 (0.5f, 0.5f));
		newButton.transform.parent = categoriesContentPanel.transform;
	}

	public void InstantiateTextContentPanel(string textContent){
		GameObject newTextContentPanel = Instantiate (textContentPanelPrefab)as GameObject;

		//newTextContentPanel.transform.GetChild (0).position = newTextContentPanel.transform.position;
		newTextContentPanel.transform.parent = panelContent.transform;
		newTextContentPanel.transform.GetChild (0).GetComponent<Text> ().text = textContent;
	}

	public void InstantiateContent(List<Content> contents){
		//prueba
		if (contents.Count > 0) {
			scrollbar.numberOfSteps = contents.Count;
			foreach (Content c in contents) {
				InstantiateTextContentPanel (c.rawContent);
			}
		}
	}
}
