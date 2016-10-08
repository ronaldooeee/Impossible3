using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ability : AbilityBehaviors {

	//Abiltiy Description
	private string name; //ability 
	private string description; //ability description
	private Sprite icon; //icon of ability
	private List<AbilityBehaviors> behaviors; 
	private bool requiresTarget; //needs target to activate
	private bool canCastOnParty; //cast on party
	private bool canCastOnSelf; //be able to on self
	private int cooldown; //ability cooldown
	private List<AbilityBehaviors> affectedStats; //list of affected stats
	private GameObject particleEffect; //needs assigned when we create the ability 
	private int range; //range of ability
	private bool aoe; //area of effect ability
	private bool singleTarget; // single target ability 

	public Ability(string abilityName, string abilityDescription, Sprite abilityIcon, List<AbilityBehaviors> abilityBehaviors, GameObject abilityParticle, bool abilityRequiresTarget, int abilityRange){ //basic ability / basic attack
		name = abilityName; 
		description = abilityDescription;
		icon = abilityIcon;
		behaviors = new List<AbilityBehaviors> ();
		behaviors = abilityBehaviors;
		cooldown = 0; 
		requiresTarget = abilityRequiresTarget; 
		canCastOnSelf = false;
		canCastOnParty = false; 
		range = abilityRange;
		aoe = false; 
		singleTarget = false; 

	}
	public Ability(string abilityName, string abilityDescription, Sprite abilityIcon, List<AbilityBehaviors> abilityBehaviors, int abilityCooldown, GameObject particle, bool abilityRequiresTarget, int abilityRange, bool abilitySingleTarget){ //single target ability
		name = abilityName;
		description = abilityDescription; 
		icon = abilityIcon;
		behaviors = new List<AbilityBehaviors> ();
		behaviors = abilityBehaviors;
		cooldown = abilityCooldown; 
		requiresTarget = abilityRequiresTarget; 
		canCastOnSelf = false;
		canCastOnParty = false; 
		range = abilityRange; 
		aoe = false;
		singleTarget = abilitySingleTarget;
	}

	public string AbilityName{
		get{
			return name; 
		}
	}
	public string AbilityDescription{
		get{
			return description; 
		}
	}
	public string AbilityIcon{
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
