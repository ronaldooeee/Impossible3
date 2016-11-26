using UnityEngine;
using System.Collections;

public class FireStorm : Ability {
	private const string aName = "FireStorm!";
	private const int rangeX = 3;
	private const int rangeY = 3;
	private const int baseDamage = 15;
	private const int tier = 2;
	private const string character = "Mage";
	private const int coolDown = 5;
	private int spellRanges = 3;

	public FireStorm(int spellRange) : base(aName, rangeX, rangeY, baseDamage, tier, character, coolDown){

	}
	public int spellRange{
		get{return spellRange;}
		set{ spellRange = 3; }
	}
}
