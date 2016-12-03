using UnityEngine;
using System.Collections;

public class WarriorAbilities : Abilities {

    public int x;
    public int y;

    public int damage;
    public int selfDamage;
    public int buff;

    public Unit target;

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

    public void Counter(Unit selectedUnit)
    {
        //does somehting
    }
    public void Flail(Unit selectedUnit)
    {
        //does somehting
    }
    public void Frenzy(Unit selectedUnit)
    {
        HealthSystem health = (HealthSystem)selectedUnit.GetComponent(typeof(HealthSystem));
        selfDamage = damage / 7;
        if (health.currentHealth <= selfDamage)
        {
            health.currentHealth = 1;
        }
        else
        {
            health.currentHealth = health.currentHealth - selfDamage;
        }
        BoardManager.Instance.AttackTarget(x, y, damage * 2, selectedUnit.cooldownAttackSeconds);
    }
    public void Rally(Unit selectedUnit)
    {
        buff = 0;
        BoardManager.Instance.BuffTarget(x, y, buff, selectedUnit.cooldownAttackSeconds);
    }
    public void Warpath(Unit selectedUnit)
    {
        //does somehting
    }
    public void ShieldBash(Unit selectedUnit)
    {
        //Debug.Log(x + "mouse X");
        //Debug.Log(y + "mouse Y");
        BoardManager.Instance.AttackTarget(x, y, damage, selectedUnit.cooldownAttackSeconds);
        //does somehting
    }

    public override void Ability1(Unit selectedUnit) { }

    public override void Ability2(Unit selectedUnit) { }

    public override void Ability3(Unit selectedUnit) { }

    public override void Ability4(Unit selectedUnit) { }

    public override void Ability5(Unit selectedUnit) { }

    public override void Ability6(Unit selectedUnit) { }
}
