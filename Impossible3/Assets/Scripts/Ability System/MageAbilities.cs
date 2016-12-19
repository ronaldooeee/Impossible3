using UnityEngine;
using System.Collections;
using System.Threading;

public class MageAbilities : Abilities
{

    public int x;
    public int y;

    public int damage;

    private System.Random random = new System.Random();

	private AudioSource source;

	public AudioClip regAttack;
	public AudioClip fireball;
	public AudioClip heal;
	public AudioClip blindSound;
	public AudioClip slowSound;
	public AudioClip divineShield;
    PlayerUnit stats;

    private void Start()
    {
        stats = this.GetComponentInParent<PlayerUnit>();
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

        stats.defaultAttackRanges = new int[] { stats.straightAttackRange, stats.diagAttackRange, stats.circAttackRange, stats.accuracy, (int)stats.cooldownAttackSeconds };

		source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        damage = this.GetComponentInParent<PlayerUnit>().damageAmount;

        x = BoardManager.Instance.selectionX;
        y = BoardManager.Instance.selectionY;
    }

	public override void RegAttack(Unit selectedUnit, Unit selectedTarget)
    {
		selectedUnit.SetAttackCooldown (stats.defaultAttackRanges[4]);
		BoardManager.Instance.AttackTarget (selectedTarget, damage);
		selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;
		source.PlayOneShot (regAttack);
    }

    private void Fireball(Unit selectedUnit, Unit selectedTarget)
    {
		selectedUnit.SetAttackCooldown (8.0f);
		damage = 2 * damage;
		BoardManager.Instance.AttackTarget (selectedTarget, damage);
		source.PlayOneShot (fireball);
    }

    public void HealSpell(Unit selectedUnit, Unit selectedTarget)
    {
		selectedUnit.SetAttackCooldown (5.0f);
		BoardManager.Instance.BuffTarget (selectedTarget, 30);
		source.PlayOneShot (heal, 1.3f);
    }

    private bool Firestorm(Unit selectedUnit, Unit selectedTarget, System.Random random)
    {
        damage = (int)System.Math.Floor(damage * 1.25);
        return BoardManager.Instance.AttackTarget(selectedTarget, damage, random.Next(0, 100));
    }

    private void FireTheStorm(object o)
    {
        ArrayList parameters = (ArrayList)o;
        int count = 0;
        int total = ((System.Random)parameters[2]).Next(1, 4);
        ((Unit)parameters[0]).SetAttackCooldown(5.0f);
        while (count < total)
        {
            if (Firestorm((Unit)parameters[0], (Unit)parameters[1], (System.Random)parameters[2]))
            {
                count++;
            }
        }
    }

    private IEnumerator BlindingLight(Unit selectedUnit, Unit selectedTarget)
    {
        int initialAccuracy = selectedTarget.accuracy;
        selectedTarget.accuracy = 0;
        selectedTarget.GetComponent<HealthSystem>().ConfirmHit(1, "Confusion!");
		source.PlayOneShot (blindSound);
        yield return new WaitForSeconds(6.0f);
        selectedTarget.accuracy = initialAccuracy;
    }

    private IEnumerator Decay(Unit selectedUnit, Unit selectedTarget)
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject enemy = selectedTarget.gameObject;
            HealthSystem healthOfEnemy = enemy.GetComponent<HealthSystem>();
            int amount = (int)System.Math.Floor(healthOfEnemy.currentHealth - (healthOfEnemy.currentHealth * 0.80));
            healthOfEnemy.takeDamageAndDie(amount);
            yield return new WaitForSeconds(3.0f);
        }
    }

    private IEnumerator Slowness(Unit selectedUnit, Unit selectedTarget)
    {
        float initialSpeed = selectedTarget.cooldownMoveSeconds;
        for (int i = 0; i < 3; i++)
        {
            selectedTarget.cooldownMoveSeconds += 3;
            yield return new WaitForSeconds(3.0f);
        }
        selectedTarget.cooldownMoveSeconds = initialSpeed;
    }

    private IEnumerator DivineShield()
    {
        for (int i = 0; i < 3; i++)
        {
            foreach (GameObject go in BoardManager.playerUnits)
            {
                Unit player = go.GetComponent<Unit>();
                HealthSystem playerHealthSystem = go.GetComponent<HealthSystem>();
                BoardManager.Instance.BuffTarget(player, 10);
                player.dodgeChance += 1;
            }
          	
            yield return new WaitForSeconds(3.0f);
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
			Fireball (selectedUnit, selectedTarget);
			selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;			
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
            selectedUnit.SetAttackCooldown(6.0f);
            ArrayList parameters = new ArrayList();
            parameters.Add(selectedUnit);
            parameters.Add(selectedTarget);
            parameters.Add(this.random);
            FireTheStorm(parameters);
            selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;
			source.PlayOneShot (regAttack);            
        }
        else
        {
            OverlaySelect(selectedUnit, 1, 2, 2, 2);
        }
    }

    public override void Ability3(Unit selectedUnit, Unit selectedTarget) {
        if (BoardManager.Instance.selectedAbility == 3)
        {
			selectedUnit.SetAttackCooldown (7.0f);
			HealSpell (selectedUnit, selectedTarget);
			selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;			
        }
        else
        {
            OverlaySelect(selectedUnit, 0, 3, 2, 1);
        }
    }

    public override void Ability4(Unit selectedUnit, Unit selectedTarget) {
        if (BoardManager.Instance.selectedAbility == 4)
        {
            selectedUnit.SetAttackCooldown(4.0f);
            StartCoroutine(BlindingLight(selectedUnit, selectedTarget));
            selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;            
        }
        else
        {
            OverlaySelect(selectedUnit, 1, 3, 3, 3);
        }
    }

    public override void Ability5(Unit selectedUnit, Unit selectedTarget) {
        if (BoardManager.Instance.selectedAbility == 5)
        {
            StartCoroutine(Decay(selectedUnit, selectedTarget));
            StartCoroutine(Slowness(selectedUnit, selectedTarget));    
			source.PlayOneShot (slowSound, 0.5f);
            selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;            
        }
        else
        {
            OverlaySelect(selectedUnit, 1, 3, 3, 1);
        }
    }

    public override void Ability6(Unit selectedUnit, Unit selectedTarget) {
        if (BoardManager.Instance.selectedAbility == 6)
        {
			source.PlayOneShot (divineShield);
            StartCoroutine(DivineShield());
            BoardManager.Instance.selectedUnit = null;
            selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;            
        }
        else
        {
            OverlaySelect(selectedUnit, 0, 2, 2, 1);
        }
    }
}
