using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataLoader : MonoBehaviour {

	public abstract void LoadData (Dictionary<string,Texture2D> iconTypes,List<POI> pois);
}
