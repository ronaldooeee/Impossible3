using UnityEngine;
using System.Collections;

public class SkeletonAbilties : Abilities {

	public int x;
	public int y;

	public int damage;
    PlayerUnit stats;

    private void Start(){
        stats = this.GetComponentInParent<PlayerUnit>();

        stats.health = 80;
        stats.damageAmount = 30;

        stats.straightMoveRange = 0;
        stats.diagMoveRange = 1;
        stats.circMoveRange = 1;

        stats.straightAttackRange = 1;
        stats.diagAttackRange = 1;
        stats.circAttackRange = 0;

        stats.cooldownMoveSeconds = 2;
        stats.cooldownAttackSeconds = 3;

		stats.dodgeChance = 5;
		stats.accuracy = 80;
    }

	private void Update() {
        stats.straightAttackRange = 1;
        stats.diagAttackRange = 1;
        stats.circAttackRange = 0;
        damage = this.GetComponentInParent<PlayerUnit>().damageAmount;
		x = BoardManager.Instance.selectionX;
		y = BoardManager.Instance.selectionY;
	}

	public override void RegAttack(Unit selectedUnit, Unit selectedTarget) {
		selectedUnit.SetAttackCooldown (1.0f);
		BoardManager.Instance.AttackTarget (x, y, damage, selectedUnit.cooldownAttackSeconds);
	}

    public override void Ability1(Unit selectedUnit, Unit selectedTarget) { }

    public override void Ability2(Unit selectedUnit, Unit selectedTarget) { }

    public override void Ability3(Unit selectedUnit, Unit selectedTarget) { }

    public override void Ability4(Unit selectedUnit, Unit selectedTarget) { }

    public override void Ability5(Unit selectedUnit, Unit selectedTarget) { }

    public override void Ability6(Unit selectedUnit, Unit selectedTarget) { }
}
