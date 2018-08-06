using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

	private GameObject target;

	// Use this for initialization
	void Start () {
		target = GameObject.Find ("User");
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (target.transform.position-transform.position);
	}
}
