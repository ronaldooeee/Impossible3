using UnityEngine;
using System.Collections;

public class Fireball : Ability {
	private const string aName = "Fireball!";
	private const int rangeX = 3;
	private const int rangeY = 3;
	private const int baseDamage = 25;
	private const int tier = 1;
	private const string character = "Mage";
	private const int coolDown = 5; 

	public Fireball() : base(aName, rangeX, rangeY, baseDamage, tier, character, coolDown){
		
	}
}
