using UnityEngine;
using System.Collections;

public class BasicObjectInformation { //constructor for basic objects

	private string name;
	private string description;
	private Sprite icon;

	public BasicObjectInformation(string objName){ //constructor for just name
		name = objName;
	}

	public BasicObjectInformation(string objName, string objDescription){ //constructor for name and description
		name = objName;
		description = objDescription;
	}

	public BasicObjectInformation(string objName, string objDescription, Sprite objIcon){ //constructor for name, description, and sprite.
		name = objName;
		description = objDescription;
		icon = objIcon;
	}

	public string ObjectName{
		get{ return name; }
	}
	public string ObjectDescription{
		get{ return description; }
	}
	public Sprite ObjectIcon{
		get{ return icon; }
	}
}
