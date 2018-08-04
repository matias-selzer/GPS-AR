using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GyroInput : MonoBehaviour {

	Gyroscope gyro;
	Quaternion gyroInitialRotation;
	public GameObject mensajeError;

	public Text consolaLoca;

	float defasaje=0;
	float oldGrados=0;
	bool usarCompass;

	// Use this for initialization
	void Start () {
		

		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		gyro = Input.gyro; // Store the reference for Gyroscope sensor 
		gyro.enabled = true; //Enable the Gyroscope sensor 

		gyroInitialRotation = new Quaternion(Input.gyro.attitude.x,Input.gyro.attitude.y,Input.gyro.attitude.z,Input.gyro.attitude.w);
		Input.location.Start ();
		Input.compass.enabled = true;

		usarCompass = true;
		if (Input.gyro.enabled) {
			mensajeError.SetActive (false);
			usarCompass = false;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (usarCompass)
			actualizarDatosCompass ();
		else
			actualizarDatosGyro ();

		/*if(Input.GetKeyDown(KeyCode.Escape)){
			usarCompass = !usarCompass;
		}*/
	}

	public void actualizarDatosCompass(){
		

		float grados = 0;
		float miz = Input.acceleration.z;

		if (miz > -0.1f && miz < 0.1f) {
			miz = 0;
			grados = oldGrados;
		} else {
			miz = -Input.acceleration.z * 90;
			grados = Input.compass.trueHeading;
			if (Input.acceleration.z > 0)
				grados = grados - 150.0f;

			if (grados < 0)
				grados += 360.0f;

			oldGrados = grados;
		}

		Vector3 nuevaRot = new Vector3 (miz, grados, 0);
		consolaLoca.text="º: "+Input.compass.trueHeading+" - \nº2: "+grados+" - \nz: "+Input.acceleration.z;

		transform.localRotation = Quaternion.Slerp (transform.localRotation, Quaternion.Euler (nuevaRot), 1 * Time.deltaTime);
	}

	public void actualizarDatosGyro(){
		Quaternion nuevo = Input.gyro.attitude;

		nuevo.x*=-1.0f;
		nuevo.y*=-1.0f;
		nuevo = Quaternion.Euler(90, 0, 0)*nuevo;

		nuevo.eulerAngles=new Vector3(nuevo.eulerAngles.x,nuevo.eulerAngles.y-defasaje,nuevo.eulerAngles.z);

		transform.localRotation =  nuevo;


	}

	//esto es por si queres guardar la posición actual como el cero, o calibrar
	public void guardar(){
		gyroInitialRotation=Input.gyro.attitude;
		gyroInitialRotation.x*=-1.0f;
		gyroInitialRotation.y*=-1.0f;
		gyroInitialRotation = Quaternion.Euler(90, 0, 0)*gyroInitialRotation;

		defasaje=gyroInitialRotation.eulerAngles.y;

	
	}
	

}
