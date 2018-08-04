using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

	public GameObject persona;
	public GameObject[] objetos;

	// Use this for initialization
	void Start () {
		Screen.SetResolution (800, 480,true);
	}

	public void setearObjetosYComenzar(GameObject[] objs){
		objetos = objs;
		InvokeRepeating ("actualizarPosiciones", 0, 1);
	}


	public void actualizarPosiciones(){
		persona.GetComponent<ActualizarPosicionGPS> ().actualizar ();
		PosicionGPS posicionBase = persona.GetComponent<PosicionGPS> ();

		//desactivo el menu interacciones nivel 2, luego cada uno lo activa si corresponde
		//GameObject.Find("Menu").GetComponent<MenuManager> ().desactivarMenuInteracciones ();

		for (int i = 0; i < objetos.Length; i++) {
			// Obtengo posición gps del objeto
			PosicionGPS posicionTarget = objetos [i].GetComponent<PosicionGPS> ();

			// Creo una posicion auxiliar para luego calcular la distancia en x e y
			PosicionGPS nuevaY = new PosicionGPS (posicionTarget.lat, posicionBase.lon);
			PosicionGPS nuevaX = new PosicionGPS (posicionBase.lat, posicionTarget.lon);
			double posX = PosicionGPS.calcularDistancia (posicionBase, nuevaX);
			double posY = PosicionGPS.calcularDistancia (posicionBase, nuevaY);

			if (posicionTarget.lat < posicionBase.lat)
				posY *= -1.0;

			if (posicionTarget.lon < posicionBase.lon)
				posX *= -1.0;

			// Asumo que el objeto esta a 2m de altura.
			//TODO ver de modelar la altura en el objeto y preguntarle el mismo.
			objetos[i].transform.position = new Vector3 (((float)(posX*1000)), 0, ((float)(posY*1000)));

			objetos [i].GetComponent<ControladorObjetoEnElMundo> ().setearDistObs (PosicionGPS.calcularDistancia (posicionBase, posicionTarget)*1000);
		}
	}
}
