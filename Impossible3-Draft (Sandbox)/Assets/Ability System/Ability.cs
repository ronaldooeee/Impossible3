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

	public Ability(string abilityName, string abilityDescription, Sprite abilityIcon, List<AbilityBehaviors> abilityBehaviors, int abilityCooldown){
		name = abilityName;
		icon = abilityIcon;
		behaviors = new List<AbilityBehaviors> ();
		behaviors = abilityBehaviors;
		cooldown = abilityCooldown; 
		requiresTarget = false; 
		canCastOnSelf = false;
		canCastOnParty = false; 
		description = "Default" 

	}



}
