using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CapturadorPantalla : MonoBehaviour {

	public GameObject label;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void guardarImagen(){
		string nombre = "DCIC-Screenshot-" + System.DateTime.Now.ToString ("yyyy-MM-dd_HH-mm-ss")+".png";
		ScreenCapture.CaptureScreenshot(nombre);
		//label.GetComponent<Text>().text=Application.persistentDataPath+"\n - "+Application.dataPath;

		// Save your image on designate path
		//byte[] bytes = MyImage.EncodeToPNG();
		//string path = Application.persistentDataPath + "/MyImage.png";
		//File.WriteAllBytes(path, bytes);
		/*
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

		intentObject.Call("setAction", new Object[]{intentClass.GetStatic("ACTION_SEND")});
		intentObject.Call("setType", "image/*");
		intentObject.Call("putExtra", intentClass.GetStatic("EXTRA_SUBJECT"), "Media Sharing ");
		intentObject.Call("putExtra", intentClass.GetStatic("EXTRA_TITLE"), "Media Sharing ");
		intentObject.Call("putExtra", intentClass.GetStatic("EXTRA_TEXT"), "Media Sharing Android Demo");

		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
		AndroidJavaClass fileClass = new AndroidJavaClass("java.io.File");

		string pepe = Application.persistentDataPath + "/" + nombre;
		AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", pepe);// Set Image Path Here
		label.GetComponent<Text> ().text = pepe;

		AndroidJavaObject uriObject = uriClass.CallStatic("fromFile", fileObject);

		// string uriPath =  uriObject.Call("getPath");
		bool fileExist = fileObject.Call("exists");
		Debug.Log("File exist : " + fileExist);
		// Attach image to intent
		if (fileExist)
			intentObject.Call("putExtra", intentClass.GetStatic("EXTRA_STREAM"), uriObject);
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic("currentActivity");
		currentActivity.Call("startActivity", intentObject);
		*/
	}
}
