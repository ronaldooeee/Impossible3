﻿using UnityEngine;
using System.Collections;

public class WarriorAbilities : Abilities {

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

        stats.health = 80;
        stats.damageAmount = 70;

        stats.straightMoveRange = 2;
        stats.diagMoveRange = 1;
        stats.circMoveRange = 0;

        stats.straightAttackRange = 2;
        stats.diagAttackRange = 1;
        stats.circAttackRange = 0;

        stats.cooldownMoveSeconds = 3;
        stats.cooldownAttackSeconds = 3;
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
        BoardManager.Instance.AttackTarget(x, y, damage, selectedUnit.cooldownAttackSeconds);
    }

    public void Counter(Unit selectedUnit, Unit selectedTarget)
    {
        //does somehting
    }
    public void Flail(Unit selectedUnit, Unit selectedTarget)
    {
		for (int a = selectedUnit.straightMoveRange; selectedUnit.CurrentX < a; a = a - 1) {
			Debug.Log (a);
			for (int b = selectedUnit.straightMoveRange; selectedUnit.CurrentY <b; b = b -1){
				Debug.Log(b);
			}
		}
    }
    public void Frenzy(Unit selectedUnit, Unit selectedTarget)
	{		selectedUnit.SetAttackCooldown (8.0f);
		damage = 2* damage;
		BoardManager.Instance.AttackTarget (x, y, damage, selectedUnit.cooldownAttackSeconds);
		/*selectedUnit.SetAttackCooldown(2.0f);
        HealthSystem health = (HealthSystem)selectedUnit.GetComponent(typeof(HealthSystem));
        selfDamage = damage / 7;
		if (health.currentHealth <= selfDamage)
        {
            health.currentHealth = 1;
        }
        else
        {
            health.currentHealth = health.currentHealth - selfDamage;
        }
        BoardManager.Instance.AttackTarget(x, y, damage * 2, selectedUnit.cooldownAttackSeconds);*/

    }
    public void Rally(Unit selectedUnit, Unit selectedTarget)
    {
        buff = 0;
        BoardManager.Instance.BuffTarget(x, y, buff, selectedUnit.cooldownAttackSeconds);
    }
    public void Warpath(Unit selectedUnit, Unit selectedTarget)
    {
        //does somehting
    }
    public void ShieldBash(Unit selectedUnit, Unit selectedTarget)
    {
		selectedUnit.SetAttackCooldown (6.0f);
		selectedTargetX = selectedTarget.CurrentX;
		selectedTargetY = selectedTarget.CurrentY;

		selectedUnitX = selectedUnit.CurrentX;
		selectedUnitY = selectedUnit.CurrentY; 
		if (selectedUnitX < selectedTargetX) {
			selectedTarget.CurrentX = selectedTarget.CurrentX + 1;
		} else if (selectedUnitX > selectedTargetX) {
			selectedTarget.CurrentX = selectedTarget.CurrentX - 1;
		} else if (selectedUnitY < selectedUnitY) {
			selectedTarget.CurrentY = selectedTarget.CurrentY + 1;
		} else {
			selectedTarget.CurrentY = selectedTarget.CurrentY - 1;
		}
		selectedTarget.SetAttackCooldown (5.0f);
        //Debug.Log(x + "mouse X");
        //Debug.Log(y + "mouse Y");
		BoardManager.Instance.AttackTarget(x, y, damage, selectedUnit.cooldownAttackSeconds);
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

    public override void Ability2(Unit selectedUnit, Unit selectedTarget) { Flail(selectedUnit, selectedTarget); }

    public override void Ability3(Unit selectedUnit, Unit selectedTarget) { 
		if (BoardManager.Instance.selectedAbility == 3) {
			Frenzy (selectedUnit, selectedTarget); 
		}
		else 
		{
			OverlaySelect (selectedUnit, 1, 2, 1, 0);
		}
	}

    public override void Ability4(Unit selectedUnit, Unit selectedTarget) { Rally(selectedUnit, selectedTarget); }

    public override void Ability5(Unit selectedUnit, Unit selectedTarget) { Warpath(selectedUnit, selectedTarget); }

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
