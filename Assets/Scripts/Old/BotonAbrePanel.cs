using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonAbrePanel : MonoBehaviour {

	public GameObject panel;

	public void abrirPanel(){
		panel.SetActive (true);
	}
}
