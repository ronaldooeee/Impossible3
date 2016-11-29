using UnityEngine;
using System.Collections;

public class SkeletonAbilties : Abilities {

	public int x;
	public int y;

	public int damage;

	private void Start(){
		this.GetComponentInParent<PlayerUnit>().SetDamage (10);
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
