using UnityEngine;
using System.Collections;

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

        stats.defaultAttackRanges = new int[] { stats.straightAttackRange, stats.diagAttackRange, stats.circAttackRange};
    }

    private void Update()
    {
        damage = this.GetComponentInParent<PlayerUnit>().damageAmount;
        spellTimer = this.GetComponentInParent<PlayerUnit>().spellTimer;
		spellCounter = this.GetComponentInParent<PlayerUnit> ().spellCounter;
        x = BoardManager.Instance.selectionX;
        y = BoardManager.Instance.selectionY;
		Debug.Log (this.GetComponentInParent<PlayerUnit>().dodgeChance);
		if(spellTimer <= Time.time && spellCounter == 1) {
			this.GetComponentInParent<PlayerUnit>().dodgeChance = 10;
			spellCounter--;
		}
    }

    public override void RegAttack(Unit selectedUnit, Unit selectedTarget)
    {
		Debug.Log (selectedUnit);
		Debug.Log (selectedTarget);
        selectedUnit.SetAttackCooldown(2.0f);
		BoardManager.Instance.AttackTarget(selectedTarget, damage, selectedUnit.cooldownAttackSeconds);
    }

    public void BackStab(Unit selectedUnit, Unit selectedTarget)
    {
        selectedUnit.SetAttackCooldown(4.0f);
        damage = 3 * damage;
		BoardManager.Instance.AttackTarget(selectedTarget, damage, selectedUnit.cooldownAttackSeconds);
    }

    public void BlackBombArrow(Unit selectedUnit, Unit selectedTarget)
    {
        selectedUnit.SetAttackCooldown(8.0f);
        selectedUnit.accuracy = 130;
        if (selectedUnit.timeStampAttack <= Time.time)
        {
            BoardManager.Instance.AttackTarget(selectedTarget, damage, 0);
            //Debug.Log ("First Hit");
			BoardManager.Instance.AttackTarget(BoardManager.Units[x, y + 1], damage, 0);
            //Debug.Log ("Second Hit");
			BoardManager.Instance.AttackTarget(BoardManager.Units[x + 1, y + 1], damage, 0);
			BoardManager.Instance.AttackTarget(BoardManager.Units[x + 1, y], damage, 0);
			BoardManager.Instance.AttackTarget(BoardManager.Units[x + 1, y - 1], damage, 0);
			BoardManager.Instance.AttackTarget(BoardManager.Units[x, y - 1], damage, 0);
			BoardManager.Instance.AttackTarget(BoardManager.Units[x - 1, y - 1], damage, 0);
			BoardManager.Instance.AttackTarget(BoardManager.Units[x - 1, y], damage, 0);
			BoardManager.Instance.AttackTarget(BoardManager.Units[x - 1, y + 1], damage, 0);
            selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;
        }
        selectedUnit.accuracy = 90;
    }

    public void LongShot(Unit selectedUnit, Unit selectedTarget)
    {
        selectedUnit.SetAttackCooldown(6.0f);
        damage = damage + (damage / 2);
        BoardManager.Instance.AttackTarget(selectedTarget, damage, selectedUnit.cooldownAttackSeconds);
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
        /*selectedUnit.SetAttackCooldown (7.0f);
		selectedUnit.SetDamage (20);
		if (selectedUnit.timeStampAttack <= Time.time) {
			BoardManager.Instance.AttackTarget (x, y, damage, selectedUnit.cooldownAttackSeconds);
			Debug.Log (selectedTarget);
			Debug.Log (selectedUnit);
			selectedTarget.timeStampAttack += 10;
			selectedTarget.timeStampMove += 10;
		}*/
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
            OverlaySelect(selectedUnit, 1, 10, 8, 9);
        }
    }

    public override void Ability6(Unit selectedUnit, Unit selectedTarget) { }
}
