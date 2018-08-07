using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconsRepository  {

	public static Dictionary<string,Texture2D> iconTypes;

	public IconsRepository(){
		IconsRepository.iconTypes = new Dictionary<string,Texture2D> ();
	}

}
