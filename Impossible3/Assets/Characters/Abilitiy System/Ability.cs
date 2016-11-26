using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ability : AbilityBehaviors {

	//Abiltiy Description
	private string aName; //ability 
	private string description; //ability description
	private Sprite icon; //icon of ability
	private List<AbilityBehaviors> behaviors; 
	private bool requiresTarget; //needs target to activate
	private bool canCastOnParty; //cast on party
	private bool canCastOnSelf; //be able to on self
	private int cooldown; //ability cooldown
	private List<AbilityBehaviors> affectedStats; //list of affected stats
	private GameObject particleEffect; //needs assigned when we create the ability 
	private int rangeX; //rangeX of ability
	private int rangeY;
	private bool aoe; //area of effect ability
	private bool singleTarget; // single target ability 
	private int damage; 
	private int tier;
	private string type; 

	public Ability(string abilityName){
		this.aName = abilityName;
	}
	public Ability(string abilityName, int rangeX, int rangeY, int damage, int tier, string type, int coolDown){
		this.aName = abilityName;
		this.rangeX = rangeX;
		this.rangeY = rangeY; 
		this.damage = damage; 
		this.tier = tier; 
		this.type = type; 
		this.cooldown = coolDown;
	}

	public Ability(string abilityName, string abilityDescription, Sprite abilityIcon, List<AbilityBehaviors> abilityBehaviors, GameObject abilityParticle, bool abilityRequiresTarget, int abilityRange){ //basic ability / basic attack
		aName = abilityName; 
		description = abilityDescription;
		icon = abilityIcon;
		behaviors = new List<AbilityBehaviors> ();
		behaviors = abilityBehaviors;
		cooldown = 0; 
		requiresTarget = abilityRequiresTarget; 
		canCastOnSelf = false;
		canCastOnParty = false; 
		rangeX = abilityRange;
		aoe = false; 
		singleTarget = false; 

	}
	public Ability(string abilityName, string abilityDescription, Sprite abilityIcon, List<AbilityBehaviors> abilityBehaviors, int abilityCooldown, GameObject particle, bool abilityRequiresTarget, int abilityRange, bool abilitySingleTarget){ //single target ability
		aName = abilityName;
		description = abilityDescription; 
		icon = abilityIcon;
		behaviors = new List<AbilityBehaviors> ();
		behaviors = abilityBehaviors;
		cooldown = abilityCooldown; 
		requiresTarget = abilityRequiresTarget; 
		canCastOnSelf = false;
		canCastOnParty = false; 
		rangeX = abilityRange; 
		aoe = false;
		singleTarget = abilitySingleTarget;
	}

	public string AbilityName{
		get{
			return aName; 
		}
	}
	public string AbilityDescription{
		get{
			return description; 
		}
	}
	public int AbilityRangeX{
		get {
			return rangeX;
		}
	}
	public int AbilityRangeY {
		get {
			return rangeY;
		}
	}
	public int AbilityDamage {
		get {
			return damage;
		}
	}
	public int AbilityTier {
		get {
			return tier;
		}
	}
	public string AbilityClass {
		get {
			return type;
		}
	}
	public Sprite AbilityIcon{
		get{
			return icon; 
		}
	}
	public int AbilityCooldown{
		get{
			return cooldown; 
		}
	}

}
