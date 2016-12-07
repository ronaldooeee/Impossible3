using UnityEngine;
using System.Collections;

public class RangerAbilities : Abilities {


    public int x;
    public int y;

    public int damage;

    private void Start()
    {
        PlayerUnit stats = this.GetComponentInParent<PlayerUnit>();

        stats.health = 1000;
        stats.damageAmount = 40;

        stats.straightMoveRange = 3;
        stats.diagMoveRange = 1;
        stats.circMoveRange = 2;

        stats.straightAttackRange = 3;
        stats.diagAttackRange = 1;
        stats.circAttackRange = 2;

        stats.cooldownMoveSeconds = 1;
        stats.cooldownAttackSeconds = 1;
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

	public void BackStab(Unit selectedUnit, Unit selectedTarget){
		selectedUnit.SetAttackCooldown (4.0f);
		damage = 3 * damage;
		BoardManager.Instance.AttackTarget (x, y, damage, selectedUnit.cooldownAttackSeconds);
	}

	public void BlackBombArrow(Unit selectedUnit, Unit selectedTarget){
		selectedUnit.SetAttackCooldown (8.0f);
		damage = 2 * damage;

		if (BoardManager.Units [x, y]) {
			BoardManager.Instance.AttackTarget (x, y, damage, selectedUnit.cooldownAttackSeconds);
		} 
		if (BoardManager.Units [x, y + 1] != null) {
			BoardManager.Instance.AttackTarget (x, y + 1, damage / 2, 0);
		}
		if (BoardManager.Units [x + 1, y + 1] != null) {
			BoardManager.Instance.AttackTarget (x + 1, y + 1, damage / 2, 0);
		}
		if (BoardManager.Units [x + 1, y] != null) {
			BoardManager.Instance.AttackTarget (x + 1, y, damage / 2, 0);
		}
		if (BoardManager.Units [x + 1, y - 1] != null) {
			BoardManager.Instance.AttackTarget (x + 1, y - 1, damage / 2, 0);
		}
		if (BoardManager.Units [x, y - 1] != null) {
			BoardManager.Instance.AttackTarget (x + 1, y - 1, damage / 2, 0);
		}
		if (BoardManager.Units [x - 1, y - 1] != null) {
			BoardManager.Instance.AttackTarget (x + 1, y - 1, damage / 2, 0);
		}
		if (BoardManager.Units [x - 1, y] != null) {
			BoardManager.Instance.AttackTarget (x - 1, y, damage / 2, 0);
		}
		if (BoardManager.Units [x - 1, y + 1] != null) {
			BoardManager.Instance.AttackTarget (x - 1, y + 1, damage / 2, 0);
		}
	}

	public void LongShot(Unit selectedUnit, Unit selectedTarget) {
		selectedUnit.SetAttackCooldown (6.0f);
		damage = damage + (damage / 2);
		BoardManager.Instance.AttackTarget (x, y, damage, selectedUnit.cooldownAttackSeconds);
	}

    public override void Ability1(Unit selectedUnit, Unit selectedTarget) {
		if (BoardManager.Instance.selectedAbility == 1)
		{
			BackStab(selectedUnit, selectedTarget);
		}else{
			OverlaySelect(selectedUnit, 1, 1, 1, 0);
		}
	}

    public override void Ability2(Unit selectedUnit, Unit selectedTarget) {
		if (BoardManager.Instance.selectedAbility == 2)
		{
			BlackBombArrow(selectedUnit, selectedTarget);
		}else{
			OverlaySelect(selectedUnit, 1, 3, 1, 2);
		}
	}

    public override void Ability3(Unit selectedUnit, Unit selectedTarget) {
		if (BoardManager.Instance.selectedAbility == 3)
		{
			LongShot(selectedUnit, selectedTarget);
		}else{
			OverlaySelect(selectedUnit, 1, 8, 6, 7);
		}
	}

    public override void Ability4(Unit selectedUnit, Unit selectedTarget) { }

    public override void Ability5(Unit selectedUnit, Unit selectedTarget) { }

    public override void Ability6(Unit selectedUnit, Unit selectedTarget) { }
}
