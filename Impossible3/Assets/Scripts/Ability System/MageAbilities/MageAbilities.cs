using UnityEngine;
using System.Collections;

public class MageAbilities : Abilities {
	
	public int x;
	public int y;

	public int damage;

	private void Start(){
		this.GetComponentInParent<PlayerUnit>().SetDamage (20);
	}

	private void Update() {
		damage = this.GetComponentInParent<PlayerUnit>().damageAmount;
		x = BoardManager.Instance.selectionX;
		y = BoardManager.Instance.selectionY;
	}

	public override void RegAttack(Unit selectedUnit) {
		selectedUnit.SetAttackCooldown (1.0f);
		damage = 5 * damage;
		BoardManager.Instance.AttackTarget (x, y, damage, selectedUnit.cooldownAttackSeconds);
	}

	public void Fireball(Unit selectedUnit) {
		//BoardManager.SelectTarget (BoardManager.selectionX, BoardManager.selectionY);
		selectedUnit.SetAttackCooldown (8.0f);
		damage = 10 * damage;
		BoardManager.Instance.AttackTarget (x, y, damage, selectedUnit.cooldownAttackSeconds);
	}
}
