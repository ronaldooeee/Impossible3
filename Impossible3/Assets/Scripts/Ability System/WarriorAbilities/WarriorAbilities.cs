using UnityEngine;
using System.Collections;

public class WarriorAbilities : Abilities
{

	public int x;
	public int y;

	public int selectedTargetX;
	public int selectedTargetY;

	public int selectedUnitX;
	public int selectedUnitY;

	public int damage;
	public int selfDamage;
	public int buff;

	public Unit target;

	private void Start()
	{
		PlayerUnit stats = this.GetComponentInParent<PlayerUnit>();

		stats.health = 800;
		stats.damageAmount = 70;

		stats.straightMoveRange = 2;
		stats.diagMoveRange = 1;
		stats.circMoveRange = 0;

		stats.straightAttackRange = 2;
		stats.diagAttackRange = 1;
		stats.circAttackRange = 0;

		stats.cooldownMoveSeconds = 3;
		stats.cooldownAttackSeconds = 3;

		stats.dodgeChance = 0;
		stats.accuracy = 95;

		stats.defaultAttackRanges = new int[] { stats.straightAttackRange, stats.diagAttackRange, stats.circAttackRange , stats.accuracy};
	}

	private void Update()
	{
		damage = this.GetComponentInParent<PlayerUnit>().damageAmount;
		x = BoardManager.Instance.selectionX;
		y = BoardManager.Instance.selectionY;
	}

	public override void RegAttack(Unit selectedUnit, Unit selectedTarget)
	{
		selectedUnit.SetAttackCooldown(1.0f);
		BoardManager.Instance.AttackTarget(selectedTarget, damage);
	}

	public void Counter(Unit selectedUnit, Unit selectedTarget)
	{
		//does somehting
	}
	public void Flail(Unit selectedUnit, Unit selectedTarget)
	{
		selectedUnit.SetAttackCooldown (10.0f);
		if (selectedUnit.timeStampAttack <= Time.time)
		{
			BoardManager.Instance.AttackTarget(selectedTarget, damage);
			//straight up
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX, selectedUnit.CurrentY + 1], damage );
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX, selectedUnit.CurrentY + 2], damage );
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX, selectedUnit.CurrentY + 3], damage );
			//straight right
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX + 1, selectedUnit.CurrentY], damage );
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX + 2, selectedUnit.CurrentY], damage );
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX + 3, selectedUnit.CurrentY], damage );
			//straight back
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX, selectedUnit.CurrentY - 1], damage );
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX, selectedUnit.CurrentY - 2], damage );
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX, selectedUnit.CurrentY - 3], damage );
			//diagonals
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX - 1, selectedUnit.CurrentY - 1], damage );
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX + 1, selectedUnit.CurrentY + 1], damage );
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX + 1, selectedUnit.CurrentY - 1], damage );
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX - 1, selectedUnit.CurrentY + 1], damage );
			//straight left
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX - 1, selectedUnit.CurrentY], damage );
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX - 2, selectedUnit.CurrentY], damage );
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX - 3, selectedUnit.CurrentY], damage );
			//l shape for X
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX - 2, selectedUnit.CurrentY + 1], damage );
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX - 2, selectedUnit.CurrentY - 1], damage );
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX + 2, selectedUnit.CurrentY + 1], damage );
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX + 2, selectedUnit.CurrentY - 1], damage );
			//L shape for Y
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX - 1, selectedUnit.CurrentY + 2], damage );
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX - 1, selectedUnit.CurrentY - 2], damage );
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX + 1, selectedUnit.CurrentY + 2], damage );
			BoardManager.Instance.AttackTarget (BoardManager.Units[selectedUnit.CurrentX + 1, selectedUnit.CurrentY - 2], damage );
			selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;
		}
	}
	public void Frenzy(Unit selectedUnit, Unit selectedTarget)
	{
		selectedUnit.SetAttackCooldown(4.0f);
		HealthSystem health = (HealthSystem)selectedUnit.GetComponent(typeof(HealthSystem));
		if (selectedTarget != selectedTarget.isPlayer) {
			selfDamage = damage / 10;
			if (health.currentHealth <= selfDamage) {
				health.currentHealth = 1;
			} else {
				health.currentHealth = health.currentHealth - selfDamage;
			}
			BoardManager.Instance.AttackTarget (selectedTarget, damage * 2);
		}

	}
	public void Rally(Unit selectedUnit, Unit selectedTarget)
	{
		BoardManager.Instance.BuffTarget (selectedTarget, 0);
		if (Time.time - selectedTarget.timeStampAttack < 3) {
			selectedTarget.timeStampAttack -= 3;
		} else {
			selectedTarget.timeStampAttack = Time.time;
		}
		if (Time.time - selectedTarget.timeStampMove < 3) {
			selectedTarget.timeStampMove -= 3;
		} else {
			selectedTarget.timeStampMove = Time.time; 
		}
	}
	public void Warpath(Unit selectedUnit, Unit selectedTarget)
	{
		damage = 2 * damage;
		HealthSystem health = (HealthSystem)selectedUnit.GetComponent(typeof(HealthSystem));
		if (health.currentHealth < selectedUnit.health) {
			selectedUnit.damageAmount = 100;
		}

		if (selectedTarget.CurrentX > selectedUnit.CurrentX) {
			Debug.Log ("Move right side");
			//BoardManager.Units [selectedUnit.CurrentX, selectedUnit.CurrentY] = null;
			//selectedUnit.transform.position = BoardManager.Instance.GetTileCenter(x - 1, y);
			//selectedUnit.SetPosition(x - 1, y);
			//BoardManager.Units [x - 1, y]= selectedUnit; 
			//BoardManager.Instance.SelectUnitForMove(selectedUnit.CurrentX, selectedUnit.CurrentY);
			BoardManager.Instance.MoveUnit (selectedUnit.CurrentX + 5, selectedUnit.CurrentY);

		} else if (selectedTarget.CurrentX < selectedUnit.CurrentX) {
			Debug.Log ("Move left side");
			//BoardManager.Units [selectedUnit.CurrentX, selectedUnit.CurrentY] = null;
			//selectedUnit.transform.position = BoardManager.Instance.GetTileCenter(x - 1, y);
			//selectedUnit.SetPosition(x - 1, y);
			//BoardManager.Units [x - 1, y]= selectedUnit; 
			BoardManager.Instance.SelectUnitForMove(selectedUnit.CurrentX, selectedUnit.CurrentY);
			BoardManager.Instance.MoveUnit (selectedUnit.CurrentX - 5, selectedUnit.CurrentY);

		} else if (y > selectedUnit.CurrentY) {
			Debug.Log ("Move up");
			BoardManager.Units [selectedUnit.CurrentX, selectedUnit.CurrentY] = null;
			selectedUnit.transform.position = BoardManager.Instance.GetTileCenter (x, selectedUnit.CurrentY + 5);
			selectedUnit.SetPosition (x, selectedUnit.CurrentY + 5);
			BoardManager.Units [x, selectedUnit.CurrentY + 5] = selectedUnit; 
			Debug.Log (selectedUnit.CurrentX);
			Debug.Log (selectedUnit.CurrentY);
			BoardManager.Instance.AttackTarget (selectedTarget, damage );

		} else if (y < selectedUnit.CurrentY && selectedUnit.CurrentY - 5 == null) {
			Debug.Log ("Move down");
			BoardManager.Units [selectedUnit.CurrentX, selectedUnit.CurrentY] = null;
			selectedUnit.transform.position = BoardManager.Instance.GetTileCenter (x, selectedUnit.CurrentY - 5);
			selectedUnit.SetPosition (x, selectedUnit.CurrentY - 5);
			BoardManager.Units [x, selectedUnit.CurrentY - 5] = selectedUnit; 
			Debug.Log (selectedUnit.CurrentX);
			Debug.Log (selectedUnit.CurrentY);
		} else {
			Debug.Log ("Bug");
			return;
		}
		BoardManager.Instance.AttackTarget (selectedTarget, damage );
	}
	public void ShieldBash(Unit selectedUnit, Unit selectedTarget)
	{
		selectedUnit.SetAttackCooldown(6.0f);
		selectedTargetX = selectedTarget.CurrentX;
		selectedTargetY = selectedTarget.CurrentY;

		selectedUnitX = selectedUnit.CurrentX;
		selectedUnitY = selectedUnit.CurrentY;
		Debug.Log(selectedTarget.CurrentX + "Target's X");
		Debug.Log(selectedTarget.CurrentY + "Target's Y");
		if (selectedUnitX < selectedTargetX)
		{
			selectedTarget.CurrentX = selectedTarget.CurrentX + 1;
			Debug.Log("Right side");
		}
		else if (selectedUnitX > selectedTargetX)
		{
			selectedTarget.CurrentX = selectedTarget.CurrentX - 1;
			Debug.Log("Left side");
		}
		else if (selectedUnitY < selectedTargetY)
		{
			selectedTarget.CurrentY = selectedTarget.CurrentY + 1;
			Debug.Log("Front side");
		}
		else
		{
			selectedTarget.CurrentY = selectedTarget.CurrentY - 1;
			Debug.Log("Back side");
		}
		selectedTarget.SetAttackCooldown(5.0f);
		//Debug.Log(x + "mouse X");
		//Debug.Log(y + "mouse Y");
		BoardManager.Instance.AttackTarget(selectedTarget, damage );
		//does somehting
	}


	public override void Ability1(Unit selectedUnit, Unit selectedTarget) {
		if (BoardManager.Instance.selectedAbility == 1)
		{
			Counter(selectedUnit, selectedTarget);
		}else
		{
			OverlaySelect(selectedUnit, 0, 0, 0, 0);//Unit selectedUnit, int attack, int straightrange, int diagrange, int circrange
		}


	}

	public override void Ability2(Unit selectedUnit, Unit selectedTarget) { 
		if(BoardManager.Instance.selectedAbility ==2){
			Flail(selectedUnit, selectedTarget); 
		}
		else{
			OverlaySelect (selectedUnit, 1, 3, 1, 2);
		}
	}
	public override void Ability3(Unit selectedUnit, Unit selectedTarget) { 
		if (BoardManager.Instance.selectedAbility == 3) {
			Frenzy (selectedUnit, selectedTarget); 
		}
		else 
		{
			OverlaySelect (selectedUnit, 1, 2, 1, 0);
		}
	}

	public override void Ability4(Unit selectedUnit, Unit selectedTarget) { 
		if (BoardManager.Instance.selectedAbility == 4) {
			Rally (selectedUnit, selectedTarget); 
		} else {
			OverlaySelect (selectedUnit, 0, 3, 1, 2);
		}
	}

	public override void Ability5(Unit selectedUnit, Unit selectedTarget) { 
		if(BoardManager.Instance.selectedAbility == 5){
			Warpath(selectedUnit, selectedTarget); 
		}
		else{
			OverlaySelect (selectedUnit, 1, 5, 0, 0);
		}
	}

	public override void Ability6(Unit selectedUnit, Unit selectedTarget) { 
		if (BoardManager.Instance.selectedAbility == 6) 
		{
			ShieldBash (selectedUnit, selectedTarget);
		} else 
		{
			OverlaySelect (selectedUnit, 1, 1, 0, 0);
		}
	}
}
