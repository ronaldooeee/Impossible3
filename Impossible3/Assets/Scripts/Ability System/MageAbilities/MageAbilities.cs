using UnityEngine;
using System.Collections;

public class MageAbilities : Abilities {
	
	public int x;
	public int y;

	public int damage;

	private void Start(){
        PlayerUnit stats = this.GetComponentInParent<PlayerUnit>();

        stats.health = 70;
        stats.damageAmount= 40;

        stats.straightMoveRange = 3;
        stats.diagMoveRange = 2;
        stats.circMoveRange =  1;
   
        stats.straightAttackRange = 4;
        stats.diagAttackRange = 3;
        stats.circAttackRange = 2;

        stats.cooldownMoveSeconds = 3;
        stats.cooldownAttackSeconds = 4;
}

	private void Update() {
		damage = this.GetComponentInParent<PlayerUnit>().damageAmount;

		x = BoardManager.Instance.selectionX;
		y = BoardManager.Instance.selectionY;
	}

	public override void RegAttack(Unit selectedUnit) {
		selectedUnit.SetAttackCooldown (1.0f);
		BoardManager.Instance.AttackTarget (x, y, damage, selectedUnit.cooldownAttackSeconds);
	}

	public void Fireball(Unit selectedUnit) {
		//BoardManager.SelectTarget (BoardManager.selectionX, BoardManager.selectionY);
		selectedUnit.SetAttackCooldown (8.0f);
		damage = 2* damage;
		BoardManager.Instance.AttackTarget (x, y, damage, selectedUnit.cooldownAttackSeconds);
	}
}
