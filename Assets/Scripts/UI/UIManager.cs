using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIManager : MonoBehaviour {

	public Text title;
	public GameObject categoryButtonPrefab;
	public GameObject categoriesContainer;
	public GameObject textContentPrefab,imageContentPrefab;
	public GameObject contentContainerPrefab;

	private List<Transform> listOfContentContainers;


	public void ShowPOIAllUIInformation(List<Category> categories){
		listOfContentContainers = new List<Transform> ();
		EmptyCategoriesContentPanel ();

		foreach (Category cat in categories) {
			GameObject newContentContainer = CreateNewContentContainer ();
			CreateContentForCategory (cat, newContentContainer);
			CreateButtonForCategory (cat, newContentContainer);
		}
		ShowFirstContentContainer ();
	}

	public void ShowFirstContentContainer(){
		ShowContent (listOfContentContainers [0].gameObject);
	}

	public void CreateButtonForCategory(Category cat,GameObject newContentContainer){
		Texture2D tex = IconsRepository.iconTypes [cat.iconType];
		GameObject newButton = Instantiate (categoryButtonPrefab) as GameObject;
		newButton.GetComponent<Image>().sprite = Sprite.Create (tex, new Rect (0.0f, 0.0f, tex.width, tex.height), new Vector2 (0.5f, 0.5f));
		newButton.transform.parent = categoriesContainer.transform;

		newButton.GetComponent<Button>().onClick.AddListener(delegate {ShowContent(newContentContainer); });
	}

	public void CreateContentForCategory(Category cat, GameObject newContentContainer){
		//ver scrollbar
		foreach (Content con in cat.contents) {
			if (con is ImageContent) {
				InstantiateImageContentPanel (((ImageContent)con).url, ((ImageContent)con).caption, newContentContainer.transform.Find ("Viewport").Find ("Content"));
			} else {
				InstantiateTextContentPanel (con.rawContent, newContentContainer.transform.Find ("Viewport").Find ("Content"));
			}
		}
	}

	public GameObject CreateNewContentContainer(){
		GameObject newContentContainer = Instantiate (contentContainerPrefab, contentContainerPrefab.transform.position, contentContainerPrefab.transform.rotation) as GameObject;
		newContentContainer.transform.SetParent (transform, false);
		listOfContentContainers.Add (newContentContainer.transform);
		return newContentContainer;
	}

	public void InstantiateImageContentPanel(string url, string caption, Transform parent){
		GameObject newImageContentPanel = Instantiate (imageContentPrefab)as GameObject;
		newImageContentPanel.transform.parent = parent;
		newImageContentPanel.transform.Find("Caption").GetComponent<Text> ().text = caption;
		//StartCoroutine(DownloadTexture(newImageContentPanel.transform.Find("RawImage").GetComponent<RawImage>(),url));
		Texture2D tex= new Texture2D(4, 4, TextureFormat.DXT1, false);
		newImageContentPanel.transform.Find ("RawImage").GetComponent<RawImage> ().texture = tex;
		DataRepository.singleton.LoadImage ( tex, url);
	}






	public void InstantiateTextContentPanel(string textContent, Transform parent){
		GameObject newTextContentPanel = Instantiate (textContentPrefab)as GameObject;
		newTextContentPanel.transform.parent = parent;
		newTextContentPanel.transform.GetChild (0).GetComponent<Text> ().text = textContent;
	}


	public void EmptyCategoriesContentPanel(){
		foreach (Transform t in categoriesContainer.transform.Cast<Transform>().ToList()) {
			Destroy (t.gameObject);
		}
	}





	public void ShowContent(GameObject contentScrollView){
		foreach (Transform t in listOfContentContainers) {
			t.gameObject.SetActive (false);
		}
		contentScrollView.SetActive (true);
	}




	public void CloseUIPanel(){
		foreach (Transform t in listOfContentContainers) {
			Destroy (t.gameObject);
		}
		gameObject.SetActive (false);
	}



	IEnumerator DownloadTexture(RawImage target,string URL)
	{
		Texture2D tex;
		tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
		using (WWW www = new WWW(URL))
		{
			yield return www;
			www.LoadImageIntoTexture(tex);
			target.texture = tex;
		}
	}
}
