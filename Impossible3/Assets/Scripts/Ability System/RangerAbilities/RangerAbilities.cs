using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RangerAbilities : Abilities
{

    public int x;
    public int y;

    public int damage;
    public float spellTimer;
	public int spellCounter;

    //public GameObject ranger = this.GetComponentInParent<PlayerUnit>().gameObject;

    private void Start()
    {
        PlayerUnit stats = this.GetComponentInParent<PlayerUnit>();

        stats.health = 80;
        stats.damageAmount = 40;

        stats.straightMoveRange = 3;
        stats.diagMoveRange = 1;
        stats.circMoveRange = 2;

        stats.straightAttackRange = 3;
        stats.diagAttackRange = 1;
        stats.circAttackRange = 2;

        stats.cooldownMoveSeconds = 2;
        stats.cooldownAttackSeconds = 2;

        stats.dodgeChance = 10;
        stats.accuracy = 90;

        stats.spellTimer = 0;
		stats.spellCounter = 0;

        stats.defaultAttackRanges = new int[] { stats.straightAttackRange, stats.diagAttackRange, stats.circAttackRange, stats.accuracy };
    }

    private void Update()
    {
        damage = this.GetComponentInParent<PlayerUnit>().damageAmount;
        spellTimer = this.GetComponentInParent<PlayerUnit>().spellTimer;
		spellCounter = this.GetComponentInParent<PlayerUnit> ().spellCounter;
        x = BoardManager.Instance.selectionX;
        y = BoardManager.Instance.selectionY;
		//Debug.Log (this.GetComponentInParent<PlayerUnit>().dodgeChance);
		if(spellTimer <= Time.time && spellCounter == 1) {
			this.GetComponentInParent<PlayerUnit>().dodgeChance = 10;
			spellCounter--;
		}
    }

    public override void RegAttack(Unit selectedUnit, Unit selectedTarget)
    {
        selectedUnit.SetAttackCooldown(2.0f);
		BoardManager.Instance.AttackTarget(selectedTarget, damage);
    }

    public void BackStab(Unit selectedUnit, Unit selectedTarget)
    {
        selectedUnit.SetAttackCooldown(4.0f);
        damage = 3 * damage;
		BoardManager.Instance.AttackTarget(selectedTarget, damage);
    }

    public void BlackBombArrow(Unit selectedUnit, Unit selectedTarget)
    {
        selectedUnit.SetAttackCooldown(8.0f);
        selectedUnit.accuracy = 130;
        if (selectedUnit.timeStampAttack <= Time.time)
        {
            BoardManager.Instance.AttackTarget(selectedTarget, damage);
            //Debug.Log ("First Hit");
			BoardManager.Instance.AttackTarget(BoardManager.Units[x, y + 1], damage);
            //Debug.Log ("Second Hit");
			BoardManager.Instance.AttackTarget(BoardManager.Units[x + 1, y + 1], damage);
			BoardManager.Instance.AttackTarget(BoardManager.Units[x + 1, y], damage);
			BoardManager.Instance.AttackTarget(BoardManager.Units[x + 1, y - 1], damage);
			BoardManager.Instance.AttackTarget(BoardManager.Units[x, y - 1], damage);
			BoardManager.Instance.AttackTarget(BoardManager.Units[x - 1, y - 1], damage);
			BoardManager.Instance.AttackTarget(BoardManager.Units[x - 1, y], damage);
			BoardManager.Instance.AttackTarget(BoardManager.Units[x - 1, y + 1], damage);
            selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;
        }
        selectedUnit.accuracy = 90;
    }

    public void LongShot(Unit selectedUnit, Unit selectedTarget)
    {
        selectedUnit.SetAttackCooldown(6.0f);
        damage = damage + (damage / 2);
        BoardManager.Instance.AttackTarget(selectedTarget, damage);
    }
		
    public void ShadowStep(Unit selectedUnit, Unit selectedTarget)
    {
        selectedUnit.SetAttackCooldown(6.0f);
        if (selectedUnit.timeStampAttack <= Time.time)
        {
            selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;
            selectedUnit.spellTimer = Time.time + 10;
            selectedUnit.dodgeChance = 80;
			selectedUnit.spellCounter += 1;
        }
    }

    //selectedUnit and selectedTarget both sending back the Ranger - Not Working
    public void Snare(Unit selectedUnit, Unit selectedTarget)
    {
        selectedUnit.SetAttackCooldown (7.0f);
		selectedUnit.SetDamage (20);
		if (selectedUnit.timeStampAttack <= Time.time) {
			BoardManager.Instance.AttackTarget (selectedTarget, damage);
			selectedTarget.timeStampAttack += 10;
			selectedTarget.timeStampMove += 10;
		}
    }

	public void TripleShot(Unit selectedUnit, Unit selectedTarget)
	{
		selectedUnit.SetAttackCooldown (7.0f);
		selectedUnit.SetDamage (25);
		List<GameObject> enemyGOs = BoardManager.enemyUnits;
		Shuffle (enemyGOs);

		if (selectedUnit.timeStampAttack <= Time.time) {
			foreach (GameObject enemy in enemyGOs) {
				Unit enemyUnit = (Unit)enemy.GetComponent (typeof(Unit));
				if (spellCounter < 3) {
					BoardManager.Instance.AttackTarget (enemyUnit, damage);
					Debug.Log (enemyUnit);
					spellCounter++;
				}
			}
		}
	}

    public override void Ability1(Unit selectedUnit, Unit selectedTarget)
    {
        if (BoardManager.Instance.selectedAbility == 1)
        {
            BackStab(selectedUnit, selectedTarget);
        }
        else
        {
            OverlaySelect(selectedUnit, 1, 1, 1, 0);
        }
    }

    public override void Ability2(Unit selectedUnit, Unit selectedTarget)
    {
        if (BoardManager.Instance.selectedAbility == 2)
        {
            BlackBombArrow(selectedUnit, selectedTarget);
        }
        else
        {
            OverlaySelect(selectedUnit, 1, 3, 1, 2);
        }
    }

    public override void Ability3(Unit selectedUnit, Unit selectedTarget)
    {
        if (BoardManager.Instance.selectedAbility == 3)
        {
            LongShot(selectedUnit, selectedTarget);
        }
        else
        {
            OverlaySelect(selectedUnit, 1, 8, 6, 7);
        }
    }

    public override void Ability4(Unit selectedUnit, Unit selectedTarget)
    {
        if (BoardManager.Instance.selectedAbility == 4)
        {
            ShadowStep(selectedUnit, selectedTarget);
        }
        else
        {
            OverlaySelect(selectedUnit, 0, 0, 0, 1);
        }
    }

    public override void Ability5(Unit selectedUnit, Unit selectedTarget)
    {
        if (BoardManager.Instance.selectedAbility == 5)
        {
            Snare(selectedUnit, selectedTarget);
        }
        else
        {
            OverlaySelect(selectedUnit, 1, 8, 6, 7);
        }
    }

    public override void Ability6(Unit selectedUnit, Unit selectedTarget) {
		if (BoardManager.Instance.selectedAbility == 6) {
			TripleShot (selectedUnit, selectedTarget);
		} else {
			OverlaySelect (selectedUnit, 1, 6, 4, 5);
		}
	}

	public void Shuffle(List<GameObject> list)
	{
		//UnityEngine.Random rng = new UnityEngine.Random();
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = (int)UnityEngine.Random.Range(0, n + 1);
			GameObject old = list[k];
			list[k] = list[n];
			list[n] = old;
		}
	}
}
