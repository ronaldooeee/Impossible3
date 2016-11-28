using UnityEngine;
using System.Collections;

public class Backstab : Ability{
	private const string aName = "Backstab!";
	private const int rangeX = 5;
	private const int rangeY = 5;
	private const int baseDamage = 25;
	private const int tier = 2;
	private const string character = "Ranger";
	private const int coolDown = 5;

	public Backstab() : base(aName, rangeX, rangeY, baseDamage, tier, character, coolDown){

	}
}
