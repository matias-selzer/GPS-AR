﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POI {

	public string name;
	public string info;
	public string iconType;
	public POILocation location;
	public GraphicPOI graphicPOI;
	public List<Category> categories;

	public void UpdateIcon(Texture2D tex){
		graphicPOI.UpdateIcon (tex);
	}

	public void UpdatePosition(Vector3 newPosition, double distance){
        graphicPOI.UpdatePosition (newPosition, distance);
	}
}
