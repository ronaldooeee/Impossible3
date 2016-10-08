using UnityEngine;
using System.Collections;

public class CharacterClasses{
	//Class Type and Description
	private string characterClassName;
	private string characterClassDescription;

	//Stats
	private int health; 
	private int movementCooldownTimer;
	private int movementRange;
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
	public string CharacterClassDescription{ //Description of class (aka more fore lore)
		get{ 
			return characterClassDescription; 
		}
		set{
			characterClassName = value; 
		}
	}
	public int Health{ //Health value
		get{ 
			return health; 
		}
		set{
			health = value; 
		}
	}
	public int MovementCooldownTimer{ //Cooldown timer for taking a movement
		get{ 
			return movementCooldownTimer; 
		}
		set{
			movementCooldownTimer = value; 
		}
	}
	public int movementRange{ //Number of tiles a character can move in either direction.
		get{ 
			return movementRange; 
		}
		set{
			characterClassName = value; 
		}
	}
	public int ActionCooldownTimer{ //Cooldown timer for taking an action
		get{ 
			return actionCoooldownTimer; 
		}
		set{
			actionCoooldownTimer = value; 
		}
	}
	public int AttackStrength{ //Character's damage 
		get{ 
			return attackStrength; 
		}
		set{
			attackStrength = value; 
		}
	}
	public int ArmorValue{ //Armor Value
		get{ 
			return armorValue; 
		}
		set{
			armorValue = value; 
		}
	}
}
