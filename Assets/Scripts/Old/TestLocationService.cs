using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;



public class TestLocationService : MonoBehaviour

{

	public Text lat,lon,dis,giro,disX,disY;
	private PosicionGPS posicionBase;
	private PosicionGPS posicionTarget;



	public GameObject canvas;

	public bool flag;

	private int contador=0;

	IEnumerator Start()
	{
		canvas.SetActive (flag);

		posicionBase = new PosicionGPS (-38.695212, -62.249344);
		//Debug.Log ("pepe");
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser)
			yield break;

		// Start service before querying location
		Input.location.Start(1,0.1f);

		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
			Debug.Log("Timed out");
			yield break;
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			Debug.Log("Unable to determine device location");
			yield break;
		}
		else
		{
			// Access granted and location value could be retrieved
			InvokeRepeating("mostrar",0,1);
		}

		// Stop service if there is no need to query location updates continuously
		//Input.location.Stop();
	}


	public void mostrar(){

		contador++;
		giro.text = contador + "";

		//label.text=("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
		//lat.text = Input.location.lastData.latitude+"";
		//lon.text = Input.location.lastData.longitude+"";
		posicionTarget = new PosicionGPS (Input.location.lastData.latitude, Input.location.lastData.longitude);
		//dis.text = calcularDistancia (posicionBase,posicionTarget)+"";
		PosicionGPS nuevaY = new PosicionGPS (posicionTarget.lat, posicionBase.lon);
		PosicionGPS nuevaX = new PosicionGPS (posicionBase.lat, posicionTarget.lon);
		double posX=calcularDistancia (posicionBase, nuevaX);
		double posY=calcularDistancia (posicionBase, nuevaY);
		//disX.text = posX + "";
		//disY.text = posY + "";

		if (posicionTarget.lat < posicionBase.lat)
			posY *= -1.0;

		if (posicionTarget.lon < posicionBase.lon)
			posX *= -1.0;

		transform.position = new Vector3 (((float)(posX*1000)), 2, ((float)(posY*1000)));
	}

	public double calcularDistancia(PosicionGPS p1, PosicionGPS p2){
		double R = 6371.0; // Radius of the earth in km
		double dLat = (p2.lat-p1.lat) * Math.PI / 180.0;  // deg2rad below
		double dLon = (p2.lon-p1.lon) * Math.PI / 180.0; 

		double a = 
			Math.Sin(dLat/2) * Math.Sin(dLat/2) +
			Math.Cos(p1.lat * Math.PI / 180.0) * Math.Cos(p2.lat * Math.PI / 180.0) * 
			Math.Sin(dLon/2) * Math.Sin(dLon/2);
		
		double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a)); 
		double d = R * c; // Distance in km

		return d;
	}
}