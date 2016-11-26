using UnityEngine;
using System.Collections;

public class BaseWarriorClass : CharacterClasses {
	public BaseWarriorClass(){ //Warrior Class
		CharacterClassName = "Warrior";
		CharacterClassDescription = "A frontline hero to help defend his friends or defeat his foes.";
		Health = 50;
		MovementCooldownTimer = 5;
		MovementRange = 5;
		ActionCooldownTimer = 5;
		AttackStrength = 10;
		ArmorValue = 10;
		Level = 1; 
		ExperiencePoints = 0; 
		SkillPoints = 1;
	} 
}
