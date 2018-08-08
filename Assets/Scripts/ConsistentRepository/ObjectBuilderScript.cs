using UnityEngine;
using System.Collections;

public class ObjectBuilderScript : MonoBehaviour 
{

	public void DeleteAllPlayerPrefs()
	{
		PlayerPrefs.DeleteAll ();
	}
}