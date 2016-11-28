using UnityEngine;
using System.Collections;

public class DivineShield : Ability {
	private const string aName = "Divine Shield!";
	private const int rangeX = 3;
	private const int rangeY = 3;
	private const int baseDamage = 0;
	private const int tier = 2;
	private const string character = "Mage";
	private const int coolDown = 5;
	private int spellRanges = 3;
	private int statIncrease = 2;

	public DivineShield(int buff) : base(aName, rangeX, rangeY, baseDamage, tier, character, coolDown){

	}
	public int buff{
		get{return buff;}
		set{ buff = statIncrease; }
	}
}
