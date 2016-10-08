using UnityEngine;
using System.Collections;

public class CharacterClasses{
	//Class Type and Description
	private string characterClassName;
	private string characterClassDescription;

	//Stats
	private int health; 
	private int movementCooldownTimer;
	private int actionCoooldownTimer;
	private int attackStrength;
	private int armorValue;

	public string CharacterClassName{ //Returns or sets class name
		get{ 
			return characterClassName; 
		}
		set{
			characterClassName = value; 
		}
	}
	public string CharacterClassDescription{
		get{ 
			return characterClassDescription; 
		}
		set{
			characterClassName = value; 
		}
	}
	public int Health{
		get{ 
			return health; 
		}
		set{
			health = value; 
		}
	}
	public int MovementCooldownTimer{
		get{ 
			return movementCooldownTimer; 
		}
		set{
			movementCooldownTimer = value; 
		}
	}
	public int ActionCooldownTimer{
		get{ 
			return actionCoooldownTimer; 
		}
		set{
			actionCoooldownTimer = value; 
		}
	}
	public int AttackStrength{
		get{ 
			return attackStrength; 
		}
		set{
			attackStrength = value; 
		}
	}
	public int ArmorValue{
		get{ 
			return armorValue; 
		}
		set{
			armorValue = value; 
		}
	}
}
