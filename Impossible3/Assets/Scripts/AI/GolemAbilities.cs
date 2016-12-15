using UnityEngine;
using System.Collections;

public class GolemAbilities : Abilities
{

    public int x;
    public int y;

    public int damage;
    PlayerUnit stats;

    private void Start()
    {
        stats = this.GetComponentInParent<PlayerUnit>();

        stats.health = 120;
        stats.damageAmount = 60;

        stats.straightMoveRange = 0;
        stats.diagMoveRange = 1;
        stats.circMoveRange = 1;

        stats.straightAttackRange = 0;
        stats.diagAttackRange = 0;
        stats.circAttackRange = 1;

        stats.cooldownMoveSeconds = 4;
        stats.cooldownAttackSeconds = 4;

		stats.dodgeChance = 0;
		stats.accuracy = 80;
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
