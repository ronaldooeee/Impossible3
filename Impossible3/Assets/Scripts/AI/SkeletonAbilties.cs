using UnityEngine;
using System.Collections;

public class SkeletonAbilties : Abilities {

	public int x;
	public int y;

	public int damage;

	private void Start(){
        PlayerUnit stats = this.GetComponentInParent<PlayerUnit>();

        stats.health = 100;
        stats.damageAmount = 20;

        stats.straightMoveRange = 0;
        stats.diagMoveRange = 0;
        stats.circMoveRange = 1;

        stats.straightAttackRange = 2;
        stats.diagAttackRange = 1;
        stats.circAttackRange = 0;

        stats.cooldownMoveSeconds = 2;
        stats.cooldownAttackSeconds = 3;
    }

	private void Update() {
		damage = this.GetComponentInParent<PlayerUnit>().damageAmount;
		x = BoardManager.Instance.selectionX;
		y = BoardManager.Instance.selectionY;
	}

	public void RegAttack(Unit selectedUnit) {
		selectedUnit.SetAttackCooldown (1.0f);
		damage = 2 * damage;
		BoardManager.Instance.AttackTarget (x, y, damage, selectedUnit.cooldownAttackSeconds);
	}
}
