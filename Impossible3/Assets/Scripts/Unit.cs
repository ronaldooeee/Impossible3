using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

    public int CurrentX;
    public int CurrentY;

    public bool isPlayer = false;
    public bool isObstacle = false;

    public int health = -1;
    public int damageAmount;

    public float cooldownMoveSeconds;
	public float cooldownAttackSeconds;
    public float timeStampMove;
    public float timeStampAttack;

    /*public int straightMoveRange;
    public int diagMoveRange;
    public int circMoveRange;

    public int straightAttackRange;
    public int diagAttackRange;
    public int circAttackRange;*/

	public int minMoveRange;
	public int maxMoveRange;

	public int minAttackRange;
	public int maxAttackRange;

    private void Awake()
    {
        timeStampMove = Time.time;
        timeStampAttack = Time.time;
    }

    public void SetPosition(int x, int y)
	{
		CurrentX = x;
		CurrentY = y;
	}

	public void SetMoveCooldown(float cdMove){
		cooldownMoveSeconds = cdMove;
	}

	public void SetAttackCooldown(float cdAttack){
		cooldownAttackSeconds = cdAttack;
	}

	public void SetDamage(int damage) {
		damageAmount = damage;
	}

	public virtual HashSet<Coord>[] PossibleMove(int CurrentX = -1, int CurrentY = -1)
    {
		return new HashSet<Coord>[maxMoveRange + 1];
	}

	public virtual HashSet<Coord>[] PossibleAttack(int CurrentX = -1, int CurrentY = -1)
    {
		return new HashSet<Coord>[maxAttackRange + 1];
	}
	public virtual HashSet<Coord>[] PossibleAbility(int CurrentX = -1, int CurrentY = -1)
	{
		return new HashSet<Coord>[maxAttackRange + 1];
	}
}
