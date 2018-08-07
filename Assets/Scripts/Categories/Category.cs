using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Category  {

	public string iconType;
	public List<Content> contents;

	public Category(){
		contents = new List<Content> ();
	}
}
