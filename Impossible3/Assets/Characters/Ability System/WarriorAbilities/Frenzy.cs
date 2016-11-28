using UnityEngine;
using System.Collections;

public class Frenzy : Ability {
	private const string aName = "Frenzy!";
	private const int rangeX = 1;
	private const int rangeY = 1;
	private const int baseDamage = 40;
	private const int tier = 1;
	private const string character = "Warrior";
	private const int coolDown = 5;
	private const int selfDamage = 5; //damage to character

	public Frenzy(int damageToSelf) : base(aName, rangeX, rangeY, baseDamage, tier, character, coolDown){

	}
	public int damageToSelf{
		get{ return damageToSelf;}
		set{damageToSelf = selfDamage;}
	}
}
