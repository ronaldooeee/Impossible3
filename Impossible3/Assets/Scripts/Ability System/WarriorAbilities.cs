using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class WarriorAbilities : Abilities
{

	public int x;
	public int y;

	public int selectedTargetX;
	public int selectedTargetY;

	public int selectedUnitX;
	public int selectedUnitY;

	public float spellTimer;
	public int spellCounter;

	public int damage;
	public int selfDamage;
	public int buff;
	public int knightOriginalHealth;
	public HealthSystem knightHealth;

	public Unit target;

	private Boolean keepPlaying; 
	private AudioSource source;

	public AudioClip regAttack;
	public AudioClip counter;
	public AudioClip flail;
	public AudioClip frenzy;
	public AudioClip rally;
	public AudioClip warpath;
	public AudioClip shieldBash;

    PlayerUnit stats;


    private void Start()
	{
		stats = this.GetComponentInParent<PlayerUnit>();

		stats.health = 120;
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

		stats.defaultAttackRanges = new int[] { stats.straightAttackRange, stats.diagAttackRange, stats.circAttackRange , stats.accuracy, (int)stats.cooldownAttackSeconds };

		source = GetComponent<AudioSource> ();
	}

	private void Update()
	{
		damage = this.GetComponentInParent<PlayerUnit>().damageAmount;
		spellTimer = this.GetComponentInParent<PlayerUnit>().spellTimer;
		spellCounter = this.GetComponentInParent<PlayerUnit> ().spellCounter;
		x = BoardManager.Instance.selectionX;
		y = BoardManager.Instance.selectionY;

		if (spellTimer <= Time.time && spellCounter == 1) {
			this.GetComponentInParent<PlayerUnit>().dodgeChance = 0;
			this.GetComponentInParent<PlayerUnit>().cooldownMoveSeconds = 3;
			keepPlaying = false;
			spellCounter--;
		}
	}

	public override void RegAttack(Unit selectedUnit, Unit selectedTarget)
	{
		selectedUnit.SetAttackCooldown(stats.defaultAttackRanges[4]);
		BoardManager.Instance.AttackTarget (selectedTarget, damage);
		selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;
		source.PlayOneShot (regAttack);		
	}

	public void Sentinel(Unit selectedUnit, Unit selectedTarget)
	{
		//does somehting
		selectedUnit.SetAttackCooldown(10.0f); 
		GameObject self = selectedUnit.gameObject;
		HealthSystem health = (HealthSystem)self.GetComponent(typeof(HealthSystem));
		knightOriginalHealth = health.currentHealth;
		selectedUnit.spellTimer = Time.time + 10;
		selectedUnit.dodgeChance = 50;
		selectedUnit.spellCounter += 1;
		source.PlayOneShot (counter);
		selectedUnit.SetMoveCooldown (2.0f);
		selectedUnit.GetComponent<HealthSystem>().ConfirmHit(0, "Sentinel!");
		selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;		
	}
	public void Flail(Unit selectedUnit, Unit selectedTarget)
	{
		selectedUnit.SetAttackCooldown (10.0f);
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
		source.PlayOneShot (flail);
		
	}
	public void Frenzy(Unit selectedUnit, Unit selectedTarget)
	{
		selectedUnit.SetAttackCooldown(4.0f);
		HealthSystem health = (HealthSystem)selectedUnit.GetComponent (typeof(HealthSystem));
		if (selectedTarget != selectedTarget.isPlayer) {
			selfDamage = damage / 10;
			if (health.currentHealth <= selfDamage) {
				health.currentHealth = 1;
			} else {
				health.currentHealth = health.currentHealth - selfDamage;
			}
			BoardManager.Instance.AttackTarget (selectedTarget, damage * 2);
		}
		selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;
		source.PlayOneShot (frenzy);

	}
	public void Rally(Unit selectedUnit, Unit selectedTarget)
	{

		selectedUnit.SetAttackCooldown (7.0f);
		if (selectedUnit.timeStampAttack <= Time.time){
			BoardManager.Instance.BuffTarget (selectedTarget, 0);
			selectedTarget.timeStampAttack = selectedTarget.timeStampAttack - 3 < Time.time ? Time.time : selectedTarget.timeStampAttack - 3;
			selectedTarget.timeStampMove = selectedTarget.timeStampMove - 3 < Time.time ? Time.time : selectedTarget.timeStampMove - 3;
			selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;
			source.PlayOneShot (rally);
		}
	}
	public void Warpath(Unit selectedUnit, Unit selectedTarget)
	{
		selectedUnit.SetAttackCooldown (10.0f);
		selectedUnitX = selectedUnit.CurrentX;
		selectedUnitY = selectedUnit.CurrentY;
		damage = 2 * damage;
		HealthSystem health = (HealthSystem)selectedUnit.GetComponent(typeof(HealthSystem));
		if (health.currentHealth < selectedUnit.health) {
			selectedUnit.damageAmount = 100;
		}
		if (selectedUnit.timeStampAttack <= Time.time){
			if (selectedTarget.CurrentX > selectedUnit.CurrentX && BoardManager.Units[selectedUnit.CurrentX + 6, y] == null) {
				Debug.Log ("Move right side");
				BoardManager.Units [selectedUnit.CurrentX + 6, y] = selectedUnit; 
				selectedUnit.transform.position = BoardManager.Instance.GetTileCenter (selectedUnit.CurrentX + 6, y);
				selectedUnit.SetPosition (selectedUnit.CurrentX + 6, y);
				BoardManager.Units [selectedUnitX, selectedUnitY] = null; 
				for (int i = 0; i < 6; i++) {
					BoardManager.Instance.AttackTarget(BoardManager.Units[selectedUnitX + i, y], damage);
				}
				source.PlayOneShot (warpath);
				Debug.Log ("Right side");

			} else if (selectedTarget.CurrentX < selectedUnit.CurrentX && BoardManager.Units[selectedUnit.CurrentX - 6, y] == null) {
				BoardManager.Units [selectedUnit.CurrentX - 6, y] = selectedUnit; 
				selectedUnit.transform.position = BoardManager.Instance.GetTileCenter (selectedUnit.CurrentX - 6, y);
				selectedUnit.SetPosition (selectedUnit.CurrentX - 6, y);
				BoardManager.Units [selectedUnitX, selectedUnitY] = null; 
				for (int i = 0; i < 6; i++) {
					BoardManager.Instance.AttackTarget(BoardManager.Units[selectedUnitX - i, y], damage);
				}
				source.PlayOneShot (warpath);
				Debug.Log ("Move left side");

			} else if (y > selectedUnit.CurrentY && BoardManager.Units[x, selectedUnit.CurrentY + 6] == null) {
				BoardManager.Units [x, selectedUnit.CurrentY + 6] = selectedUnit; 
				selectedUnit.transform.position = BoardManager.Instance.GetTileCenter (x, selectedUnit.CurrentY + 6);
				selectedUnit.SetPosition (x, selectedUnit.CurrentY + 6);
				BoardManager.Units [selectedUnitX, selectedUnitY] = null; 
				for (int i = 0; i < 6; i++) {
					BoardManager.Instance.AttackTarget(BoardManager.Units[x, selectedUnitY + i], damage);
				}
				source.PlayOneShot (warpath);

			} else if (y < selectedUnit.CurrentY && BoardManager.Units[x, selectedUnit.CurrentY - 6] == null) {
				BoardManager.Units [x, selectedUnit.CurrentY - 6] = selectedUnit; 
				selectedUnit.transform.position = BoardManager.Instance.GetTileCenter (x, selectedUnit.CurrentY - 6);
				selectedUnit.SetPosition (x, selectedUnit.CurrentY - 6);
				BoardManager.Units [selectedUnitX, selectedUnitY] = null; 
				for (int i = 0; i < 6; i++) {
					BoardManager.Instance.AttackTarget(BoardManager.Units[x, selectedUnitY - i], damage);
				}
				source.PlayOneShot (warpath);
			} else {
				Debug.Log ("Cannot do WarPath");
				return;
			}
			selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;
		}
	}

	public void ShieldBash(Unit selectedUnit, Unit selectedTarget)
	{
		selectedUnit.SetAttackCooldown(6.0f);
		if (selectedUnit.timeStampAttack <= Time.time){
			source.PlayOneShot (shieldBash);
			if (selectedUnit.CurrentX < selectedTarget.CurrentX && BoardManager.Units [x + 1, y] == null) {
				selectedTarget.transform.position = BoardManager.Instance.GetTileCenter (x + 1, y);
				selectedTarget.SetPosition (x + 1, y);
				BoardManager.Units [x + 1, y] = selectedTarget; 
				BoardManager.Units [x, y] = null; 
				Debug.Log ("Right side");
				BoardManager.Instance.AttackTarget(selectedTarget, damage/2 );
			} else if (selectedUnit.CurrentX > selectedTarget.CurrentX && BoardManager.Units [x - 1, y] == null) {
				selectedTarget.transform.position = BoardManager.Instance.GetTileCenter (x - 1, y);
				selectedTarget.SetPosition (x - 1, y);
				BoardManager.Units [x - 1, y] = selectedTarget; 
				BoardManager.Units [x, y] = null; 
				Debug.Log ("Left side");
				BoardManager.Instance.AttackTarget(selectedTarget, damage/2 );
			} else if (selectedUnit.CurrentY < selectedTarget.CurrentY && BoardManager.Units [x, y + 1] == null) {
				selectedTarget.transform.position = BoardManager.Instance.GetTileCenter (x, y + 1);
				selectedTarget.SetPosition (x, y + 1);
				BoardManager.Units [x, y + 1] = selectedTarget; 
				BoardManager.Units [x, y] = null; 
				Debug.Log ("Front side");
				BoardManager.Instance.AttackTarget(selectedTarget, damage/2 );
			} else if (selectedUnit.CurrentY < selectedTarget.CurrentY && BoardManager.Units [x, y - 1] == null) {
				selectedTarget.transform.position = BoardManager.Instance.GetTileCenter (x, y - 1);
				selectedTarget.SetPosition (x, y - 1);
				BoardManager.Units [x, y - 1] = selectedTarget; 
				BoardManager.Units [x, y] = null; 
				BoardManager.Instance.AttackTarget(selectedTarget, damage/2 );
			} else {
				BoardManager.Instance.AttackTarget(selectedTarget, damage );
			}
			selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;

		}
	}


	public override void Ability1(Unit selectedUnit, Unit selectedTarget) {
		if (BoardManager.Instance.selectedAbility == 1)
		{
			Sentinel(selectedUnit, selectedTarget);
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
