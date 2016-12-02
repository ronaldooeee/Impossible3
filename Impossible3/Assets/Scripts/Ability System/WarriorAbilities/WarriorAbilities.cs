using UnityEngine;
using System.Collections;

public class WarriorAbilities : Abilities {

    public int x;
    public int y;

    public int damage;

    private void Start()
    {
        PlayerUnit stats = this.GetComponentInParent<PlayerUnit>();

        stats.health = 80;
        stats.damageAmount = 70;

        stats.straightMoveRange = 2;
        stats.diagMoveRange = 1;
        stats.circMoveRange = 0;

        stats.straightAttackRange = 2;
        stats.diagAttackRange = 1;
        stats.circAttackRange = 0;

        stats.cooldownMoveSeconds = 3;
        stats.cooldownAttackSeconds = 3;
    }

    private void Update()
    {
        damage = this.GetComponentInParent<PlayerUnit>().damageAmount;
        x = BoardManager.Instance.selectionX;
        y = BoardManager.Instance.selectionY;
    }

    public override void RegAttack(Unit selectedUnit)
    {
        selectedUnit.SetAttackCooldown(1.0f);
        BoardManager.Instance.AttackTarget(x, y, damage, selectedUnit.cooldownAttackSeconds);
    }

    public override void Ability1(Unit selectedUnit) { }

    public override void Ability2(Unit selectedUnit) { }

    public override void Ability3(Unit selectedUnit) { }

    public override void Ability4(Unit selectedUnit) { }

    public override void Ability5(Unit selectedUnit) { }

    public override void Ability6(Unit selectedUnit) { }
}
