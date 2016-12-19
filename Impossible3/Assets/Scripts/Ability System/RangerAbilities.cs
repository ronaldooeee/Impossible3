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

	public AudioSource[] source;

	public AudioClip bowPullFireHit;
	public AudioClip backStab;
	public AudioClip explosion;
	public AudioClip dodgeNoise;
	private Boolean keepPlaying;
	private float dodgeLoop = 0.4f;
	public AudioClip snare;
	public AudioClip tripleShot;

    PlayerUnit stats;

    private void Start()
    {
        stats = this.GetComponentInParent<PlayerUnit>();

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

        stats.defaultAttackRanges = new int[] { stats.straightAttackRange, stats.diagAttackRange, stats.circAttackRange, stats.accuracy, (int)stats.cooldownAttackSeconds };

		source = GetComponents<AudioSource> ();
    }

    private void Update()
    {
        damage = this.GetComponentInParent<PlayerUnit>().damageAmount;
        spellTimer = this.GetComponentInParent<PlayerUnit>().spellTimer;
		spellCounter = this.GetComponentInParent<PlayerUnit> ().spellCounter;
        x = BoardManager.Instance.selectionX;
        y = BoardManager.Instance.selectionY;
		if(spellTimer <= Time.time && spellCounter == 1) {
			this.GetComponentInParent<PlayerUnit>().dodgeChance = 10;
			keepPlaying = false;
			Debug.Log (keepPlaying);
			spellCounter--;
		}
    }

	public override void RegAttack(Unit selectedUnit, Unit selectedTarget)
    {
        selectedUnit.SetAttackCooldown(stats.defaultAttackRanges[4]);
		source[1].PlayOneShot (bowPullFireHit);
        selectedUnit.SetAttackCooldown(2.0f);
        BoardManager.Instance.AttackTarget(selectedTarget, damage);
        selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;

    }

    public void BackStab(Unit selectedUnit, Unit selectedTarget)
    {
		selectedUnit.SetAttackCooldown (4.0f);
		damage = 3 * damage;
		BoardManager.Instance.AttackTarget (selectedTarget, damage);
		selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;
		source [1].PlayOneShot (backStab, 0.8f);		
    }

    public void BlackBombArrow(Unit selectedUnit, Unit selectedTarget)
    {
        selectedUnit.SetAttackCooldown(8.0f);
        selectedUnit.accuracy = 130;
        BoardManager.Instance.AttackTarget(selectedTarget, damage);
		BoardManager.Instance.AttackTarget(BoardManager.Units[x, y + 1], damage);
		BoardManager.Instance.AttackTarget(BoardManager.Units[x + 1, y + 1], damage);
		BoardManager.Instance.AttackTarget(BoardManager.Units[x + 1, y], damage);
		BoardManager.Instance.AttackTarget(BoardManager.Units[x + 1, y - 1], damage);
		BoardManager.Instance.AttackTarget(BoardManager.Units[x, y - 1], damage);
		BoardManager.Instance.AttackTarget(BoardManager.Units[x - 1, y - 1], damage);
		BoardManager.Instance.AttackTarget(BoardManager.Units[x - 1, y], damage);
		BoardManager.Instance.AttackTarget(BoardManager.Units[x - 1, y + 1], damage);
        selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;
		source[1].PlayOneShot (explosion);
        selectedUnit.accuracy = 90;
    }

    public void LongShot(Unit selectedUnit, Unit selectedTarget)
    {
			selectedUnit.SetAttackCooldown (6.0f);
			damage = damage + (damage / 2);
			BoardManager.Instance.AttackTarget (selectedTarget, damage);
			selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;
			source [1].PlayOneShot (bowPullFireHit);
    }
		
    public void ShadowStep(Unit selectedUnit, Unit selectedTarget)
    {
        selectedUnit.SetAttackCooldown(6.0f);
        selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;
        selectedUnit.spellTimer = Time.time + 10;
        selectedUnit.dodgeChance = 80;
		selectedUnit.spellCounter += 1;
		keepPlaying = true;
		StartCoroutine (DodgeNoise ());
        selectedUnit.GetComponent<HealthSystem>().ConfirmHit(0, "Stealth!");
        BoardHighlights.Instance.Hidehighlights();
    }

	private IEnumerator DodgeNoise() {
		while (keepPlaying) {
			source[0].PlayOneShot (dodgeNoise, 0.1f);
			yield return new WaitForSeconds (dodgeLoop);
		}
	}

    //selectedUnit and selectedTarget both sending back the Ranger - Not Working
    public void Snare(Unit selectedUnit, Unit selectedTarget)
    {
        selectedUnit.SetAttackCooldown (7.0f);
		selectedUnit.SetDamage (20);
		selectedTarget.timeStampAttack += 10;
		selectedTarget.timeStampMove += 10;
        selectedTarget.GetComponent<HealthSystem>().ConfirmHit(1, "Snared!");
        selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;
		source[1].PlayOneShot (snare);
    }

	public void TripleShot(Unit selectedUnit, Unit selectedTarget)
	{
		selectedUnit.SetAttackCooldown (7.0f);
		selectedUnit.SetDamage (25);
		List<GameObject> enemyGOs = BoardManager.enemyUnits;
		Shuffle (enemyGOs);
        Unit[] enemies = new Unit[] {null,null,null};
        for(int i= 0; i<=2 && i < enemyGOs.Count; i++) {
			Unit enemyUnit = enemyGOs[i].GetComponent<Unit>();
                enemies[i] = enemyUnit;
				spellCounter++;
		}
        BoardManager.Instance.AttackTarget(enemies[0], damage);
        BoardManager.Instance.AttackTarget(enemies[1], damage);
        BoardManager.Instance.AttackTarget(enemies[2], damage);
        selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;
		source[1].PlayOneShot (tripleShot);
        
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
        ShadowStep(selectedUnit, selectedTarget);
    }

    public override void Ability5(Unit selectedUnit, Unit selectedTarget)
    {
        if (BoardManager.Instance.selectedAbility == 5)
        {
            Snare(selectedUnit, selectedTarget);
        }
        else
        {
            OverlaySelect(selectedUnit, 1, 5, 0, 5);
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
