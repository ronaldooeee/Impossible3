using UnityEngine;
using System.Collections;
using System.Threading;

public class MageAbilities : Abilities
{

    public int x;
    public int y;

    public int damage;

    private System.Random random = new System.Random();

    private void Start()
    {
        PlayerUnit stats = this.GetComponentInParent<PlayerUnit>();

        stats.health = 70;
        stats.damageAmount = 50;

        stats.straightMoveRange = 3;
        stats.diagMoveRange = 2;
        stats.circMoveRange = 1;

        stats.straightAttackRange = 4;
        stats.diagAttackRange = 3;
        stats.circAttackRange = 2;

        stats.cooldownMoveSeconds = 3;
        stats.cooldownAttackSeconds = 4;

        stats.dodgeChance = 5;
        stats.accuracy = 90;

        stats.defaultAttackRanges = new int[] { stats.straightAttackRange, stats.diagAttackRange, stats.circAttackRange, stats.accuracy };
    }

    private void Update()
    {
        damage = this.GetComponentInParent<PlayerUnit>().damageAmount;

        x = BoardManager.Instance.selectionX;
        y = BoardManager.Instance.selectionY;
    }

    public override void RegAttack(Unit selectedUnit, Unit selectedTarget)
    {
        selectedUnit.SetAttackCooldown(1.0f);
		BoardManager.Instance.AttackTarget(selectedTarget, damage);
    }

    private void Fireball(Unit selectedUnit, Unit selectedTarget)
    {
        selectedUnit.SetAttackCooldown(8.0f);
        damage = 2 * damage;
        BoardManager.Instance.AttackTarget(selectedTarget, damage);
    }

    public void HealSpell(Unit selectedUnit, Unit selectedTarget)
    {
        selectedUnit.SetAttackCooldown(5.0f);
        BoardManager.Instance.BuffTarget(selectedTarget, 50);
    }

    private bool Firestorm(Unit selectedUnit, Unit selectedTarget, System.Random random)
    {
        selectedUnit.SetAttackCooldown(5.0f);
        damage = (int)System.Math.Floor(damage * 1.25);
        return BoardManager.Instance.AttackTarget(selectedTarget, damage, random.Next(0, 100));
    }

    private void FireTheStorm(object o)
    {
        ArrayList parameters = (ArrayList)o;
        int count = 0;
        int total = ((System.Random)parameters[2]).Next(1, 4);
        while (count < total)
        {
            if (Firestorm((Unit)parameters[0], (Unit)parameters[1], (System.Random)parameters[2]))
            {
                //Debug.Log("hit");
                count++;
            }
        }
    }

    private void BlindingLight(Unit selectedUnit, Unit selectedTarget)
    {
        selectedTarget.accuracy -= 40;
    }

    private void Decay(Unit selectedUnit, Unit selectedTarget)
    {
        selectedTarget.health = (int)System.Math.Floor(selectedTarget.health * 0.80);
    }

    private void Slowness(Unit selectedUnit, Unit selectedTarget)
    {
        selectedTarget.cooldownMoveSeconds += 3;
    }

    private void DivineShield()
    {
        foreach (GameObject go in BoardManager.playerUnits)
        {
            Debug.Log("buff");
            Unit player = go.GetComponent<Unit>();
            player.health *= 2;
            player.dodgeChance = 1;
        }
    }

    private int[] DivineShieldPrep()
    {
        int[] dodgeChances = new int[BoardManager.playerUnits.Count];
        int iterate = 0;
        foreach (GameObject go in BoardManager.playerUnits)
        {
            Unit player = go.GetComponent<Unit>();
            player.health += 10;
            dodgeChances[iterate] = player.dodgeChance;
            iterate++;
        }
        return dodgeChances;
    }
    public override void Ability1(Unit selectedUnit, Unit selectedTarget)
    {
        if (BoardManager.Instance.selectedAbility == 1)
        {
            Fireball(selectedUnit, selectedTarget);
        }
        else
        {
            OverlaySelect(selectedUnit, 1, 3, 2, 1);
        }
    }

    public override void Ability2(Unit selectedUnit, Unit selectedTarget)
    {
        if (BoardManager.Instance.selectedAbility == 2)
        {
            //Thread rain = new Thread(FireTheStorm);
            ArrayList parameters = new ArrayList();
            parameters.Add(selectedUnit);
            parameters.Add(selectedTarget);
            parameters.Add(this.random);
            //rain.Start((object)parameters);
            FireTheStorm(parameters);
            selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;
        }
        else
        {
            OverlaySelect(selectedUnit, 1, 2, 2, 2);
        }
    }

    public override void Ability3(Unit selectedUnit, Unit selectedTarget) {
        if (BoardManager.Instance.selectedAbility == 3)
        {
            HealSpell(selectedUnit, selectedTarget);
        }
        else
        {
            OverlaySelect(selectedUnit, 0, 3, 2, 1);
        }
    }

    public override void Ability4(Unit selectedUnit, Unit selectedTarget) {
        if (BoardManager.Instance.selectedAbility == 4)
        {
            float start = Time.time;
            float end = Time.time + 6.0f;
            int initialAccuracy = selectedTarget.accuracy;
            BlindingLight(selectedUnit, selectedTarget);
            while (Time.time < start + end)
            {
                //Pass.
            }
            selectedTarget.accuracy = initialAccuracy;
        }
        else
        {
            OverlaySelect(selectedUnit, 1, 3, 3, 3);
        }
    }

    public override void Ability5(Unit selectedUnit, Unit selectedTarget) {
        if (BoardManager.Instance.selectedAbility == 5)
        {
            int three = 0;
            float go = 0.0f;
            float initialSpeed = selectedTarget.cooldownMoveSeconds;
            while (three < 3)
            {
                if (Time.time > go)
                {
                    Decay(selectedUnit, selectedTarget);
                    Slowness(selectedUnit, selectedTarget);
                    go = Time.time + 3.0f;
                    three++;
                }

            }
            while (go > Time.time)
            {
                //Pass.
            }
            selectedTarget.cooldownMoveSeconds = initialSpeed;
        }
        else
        {
            OverlaySelect(selectedUnit, 1, 3, 3, 1);
        }
    }

    public override void Ability6(Unit selectedUnit, Unit selectedTarget) {
        if (BoardManager.Instance.selectedAbility == 6)
        {
            int three = 0;
            float go = 0.0f;
            int[] initialDodgeChances = DivineShieldPrep();
            while (three < 3)
            {
                Debug.Log(Time.time);
                if (Time.time > go)
                {
                    DivineShield();
                    go = Time.time + 3.0f;
                    three++;
                }

            }
            while (go > Time.time)
            {
                //Pass.
            }
            int iterate = 0;
            foreach (GameObject playerOnBoard in BoardManager.playerUnits)
            {
                Unit player = playerOnBoard.GetComponent<Unit>();
                player.dodgeChance = initialDodgeChances[iterate];
                iterate++;
            } //Unfortunately this assumes that all the players are still alive. Thankfully, throughout the course of the game, we never spawn more players. We'll always have enough dodgeChances to go around, but they may be misattributed.
        }
        else
        {
            OverlaySelect(selectedUnit, 0, 2, 2, 1);
        }
    }
}
