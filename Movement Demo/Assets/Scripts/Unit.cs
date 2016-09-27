using UnityEngine;
using System.Collections;

public abstract class Unit : MonoBehaviour {

	public int CurrentX{ set; get; }
	public int CurrentY{ set; get; }
	public bool isPlayer;
}
