using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageContent : Content {

	public string url;
	public string caption;

	public ImageContent(string url, string caption){
		this.url = url;
		this.caption = caption;
	}
}
