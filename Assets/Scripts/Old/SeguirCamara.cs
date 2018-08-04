using UnityEngine;
using System.Collections;

public class SeguirCamara : MonoBehaviour {

	public GameObject target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 n =  target.transform.position ;
		//n.z *= -1;
		transform.LookAt (n);
		//transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x,transform.rotation.y*-1,transform.rotation.z));
	}
}
