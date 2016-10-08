using UnityEngine;
using System.Collections;

public class BaseRangerClass : CharacterClasses {

	public BaseRangerClass(){ //Ranger Class
		CharacterClassName = "Ranger";
		CharacterClassName = "A man of the North, ready to use his bow and daggers to defeat his enemies.";
		Health = 40;
		MovementCooldownTimer = 5;
		MovementRange = 5;
		ActionCooldownTimer = 5;
		AttackStrength = 10;
		ArmorValue = 10;
	}
}
