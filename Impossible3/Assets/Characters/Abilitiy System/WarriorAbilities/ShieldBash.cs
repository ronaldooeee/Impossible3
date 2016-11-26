using UnityEngine;
using System.Collections;

public class ShieldBash : Ability {
	private const string aName = "Shield Bash!";
	private const int rangeX = 1;
	private const int rangeY = 1;
	private const int baseDamage = 15;
	private const int tier = 1;
	private const string character = "Warrior";
	private const int coolDown = 5;
	private const int pushBackTile = 1; //tiles enemy is pushed back

	public ShieldBash(int pushBack) : base(aName, rangeX, rangeY, baseDamage, tier, character, coolDown){

	}
	public int pushBack{
		get{ return pushBack; }
		set{pushBack = pushBackTile;}
	}
}
