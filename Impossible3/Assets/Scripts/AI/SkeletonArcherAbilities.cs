using UnityEngine;
using System.Collections;

public class SkeletonArcherAbilities : Abilities
{

	public int x;
	public int y;

	public int damage;

	private void Start()
	{
		PlayerUnit stats = this.GetComponentInParent<PlayerUnit>();

		stats.health = 60;
		stats.damageAmount = 20;

		stats.straightMoveRange = 3;
		stats.diagMoveRange = 1;
		stats.circMoveRange = 2;

		stats.straightAttackRange = 6;
		stats.diagAttackRange = 4;
		stats.circAttackRange = 5;

		stats.cooldownMoveSeconds = 3;
		stats.cooldownAttackSeconds = 4;

		stats.dodgeChance = 10;
		stats.accuracy = 70;
	}

	private void Update()
	{
		damage = this.GetComponentInParent<PlayerUnit>().damageAmount;
		x = BoardManager.Instance.selectionX;
		y = BoardManager.Instance.selectionY;
	}

	public override void RegAttack(Unit selectedUnit, Unit selectedTarget)
    {
	}

	public override void Ability1(Unit selectedUnit, Unit selectedTarget) { }

	public override void Ability2(Unit selectedUnit, Unit selectedTarget) { }

	public override void Ability3(Unit selectedUnit, Unit selectedTarget) { }

	public override void Ability4(Unit selectedUnit, Unit selectedTarget) { }

	public override void Ability5(Unit selectedUnit, Unit selectedTarget) { }

	public override void Ability6(Unit selectedUnit, Unit selectedTarget) { }
}
