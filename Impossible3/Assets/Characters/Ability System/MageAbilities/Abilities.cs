using UnityEngine;
using System.Collections;

public class Abilities : MonoBehaviour {
	public int x = BoardManager.Instance.selectionX;
	public int y = BoardManager.Instance.selectionY;

	public void RegAttack(Unit selectedTarget) {
		selectedTarget.SetAttackCooldown (1.0f);
		selectedTarget.SetDamage (50);
		BoardManager.AttackTarget (x, y, selectedTarget.damageAmount, selectedTarget.cooldownAttackSeconds);
	}

	public void Fireball(Unit selectedTarget) {
		//BoardManager.SelectTarget (BoardManager.selectionX, BoardManager.selectionY);
		selectedTarget.SetAttackCooldown (8.0f);
		selectedTarget.SetDamage (100);
		BoardManager.AttackTarget (x, y, selectedTarget.damageAmount, selectedTarget.cooldownAttackSeconds);
	}
}
