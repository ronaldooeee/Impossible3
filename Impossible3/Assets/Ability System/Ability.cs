using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ability : AbilityBehaviors {

	//Abiltiy Description
	private BasicObjectInformation objectInfo; //basic object with name, description and icon
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

	public Ability(){
		//empty constructor to fix bugs
	}
	public Ability(BasicObjectInformation abilityBasicInfo, List<AbilityBehaviors> abilityBehaviors, GameObject abilityParticle, bool abilityRequiresTarget, int abilityRange){ //basic ability / basic attack
		objectInfo = abilityBasicInfo;
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
	public Ability(BasicObjectInformation abilityBasicInfo, List<AbilityBehaviors> abilityBehaviors, int abilityCooldown, GameObject particle, bool abilityRequiresTarget, int abilityRange, bool abilitySingleTarget){ //single target ability
		objectInfo = abilityBasicInfo;
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


	public BasicObjectInformation AbilityInfo{
		get{
			return objectInfo;
		}
	}
	public int AbilityCooldown{
		get{
			return cooldown; 
		}
	}
	public List<AbilityBehaviors> AbilityBehaviors{
		get{
			return behaviors;
		}
	}
	public void UseAbility(){
	}
}