using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class DataRepository : MonoBehaviour {

	public static DataRepository singleton;

	// Use this for initialization
	void Start () {
		if (singleton == null)
			singleton = this;

		if (!PlayerPrefs.HasKey ("ImagesNumber")) {
			PlayerPrefs.SetInt ("ImagesNumber", 0);
		}
		//Debug.Log (Application.persistentDataPath);
	}


	public void LoadImage(Texture2D tex, string url){
		if (PlayerPrefs.HasKey (url)) {
			Debug.Log ("loading image from local storage");
			LoadImageFromMobile (tex, PlayerPrefs.GetString (url));
		} else {
			Debug.Log ("loading image from internet");
			StartCoroutine (DownloadTexture (tex, url));
		}
	}


	void LoadImageFromMobile(Texture2D tex, string fileName)
	{
		string filePath=Application.persistentDataPath + "/" + fileName;
		byte[] fileData; 

		if (File.Exists(filePath))     {
			fileData = File.ReadAllBytes(filePath);
			tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
		}



	}

	void SaveImageToMobile(Texture2D tex, string fileName)
	{

		Texture2D newTexture = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
		newTexture.SetPixels(0,0, tex.width, tex.height, tex.GetPixels());
		newTexture.Apply();
		byte[] byteArray = newTexture.EncodeToPNG ();
		File.WriteAllBytes (Application.persistentDataPath + "/" + fileName, byteArray);
	}





	IEnumerator DownloadTexture(Texture2D tex,string URL)
	{
		using (WWW www = new WWW(URL))
		{
			yield return www;
			www.LoadImageIntoTexture(tex);
			int imageNumber = PlayerPrefs.GetInt ("ImagesNumber");
			PlayerPrefs.SetInt ("ImagesNumber", imageNumber + 1);
			string fileName = "image" + imageNumber+".png";
			PlayerPrefs.SetString (URL, fileName);
			SaveImageToMobile(tex,fileName);
		}
	}

	public void SavePersistentData(){
		PlayerPrefs.Save ();
	}
}
