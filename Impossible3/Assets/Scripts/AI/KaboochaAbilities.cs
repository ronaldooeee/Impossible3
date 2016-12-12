using UnityEngine;
using System.Collections;

public class KaboochaAbilities : Abilities
{

    public int x;
    public int y;

    public int damage;
    PlayerUnit stats;

    private void Start()
    {
        stats = this.GetComponentInParent<PlayerUnit>();

        stats.health = 60;
        stats.damageAmount = 10;

        stats.straightMoveRange = 2;
        stats.diagMoveRange = 1;
        stats.circMoveRange = 0;

        stats.straightAttackRange = 4;
        stats.diagAttackRange = 2;
        stats.circAttackRange = 3;

        stats.cooldownMoveSeconds = 2;
        stats.cooldownAttackSeconds = 3;

		stats.dodgeChance = 5;
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

    public override void Ability1(Unit selectedUnit, Unit selectedTarget) {

    }

    public override void Ability2(Unit selectedUnit, Unit selectedTarget) { }

    public override void Ability3(Unit selectedUnit, Unit selectedTarget) { }

    public override void Ability4(Unit selectedUnit, Unit selectedTarget) { }

    public override void Ability5(Unit selectedUnit, Unit selectedTarget) { }

    public override void Ability6(Unit selectedUnit, Unit selectedTarget) { }

}
