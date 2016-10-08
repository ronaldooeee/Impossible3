using UnityEngine;
using System.Collections;

public class BaseMageClass : CharacterClasses {

	public BaseMageClass(){ //Mage Class
		CharacterClassName = "Mage";
		CharacterClassName = "A practicioner of the arcane arts.";
		Health = 30;
		MovementCooldownTimer = 5;
		MovementRange = 5;
		ActionCooldownTimer = 5;
		AttackStrength = 10;
		ArmorValue = 10;
	}
}
