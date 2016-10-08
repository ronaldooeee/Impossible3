using UnityEngine;
using System.Collections;

public class BaseWarriorClass : CharacterClasses {

	public void WarriorClass(){ //Warrior Class
		CharacterClassName = "Warrior";
		CharacterClassName = "A frontline hero to help defend his friends or defeat his foes.";
		Health = 50;
		MovementCooldownTimer = 5;
		MovementRange = 5;
		ActionCooldownTimer = 5;
		AttackStrength = 10;
		ArmorValue = 10;
	}
}
