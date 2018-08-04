using UnityEngine;
using System.Collections;

public class ActualizarPosicionGPS : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
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

	}


	public void actualizar(){
		GetComponent<PosicionGPS>().lat=Input.location.lastData.latitude;
		GetComponent<PosicionGPS>().lon=Input.location.lastData.longitude;
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
