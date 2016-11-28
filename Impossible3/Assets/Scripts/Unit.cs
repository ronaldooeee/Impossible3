using UnityEngine;
using System.Collections;

public abstract class Unit : MonoBehaviour {

    public int CurrentX;
    public int CurrentY;

    public bool isPlayer = false;

	public float cooldownMoveSeconds;
	public float cooldownAttackSeconds;
	public float timeStampMove = Time.time;
	public float timeStampAttack = Time.time;

    public int damageAmount;

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

	public virtual bool[,] PossibleMove(int CurrentX = -1, int CurrentY = -1)
    {
		return new bool[BoardManager.mapSize, BoardManager.mapSize];
	}

	public virtual bool[,] PossibleAttack(int CurrentX = -1, int CurrentY = -1)
    {
		return new bool[BoardManager.mapSize, BoardManager.mapSize];
	}
	public virtual bool[,] PossibleAbility(int CurrentX = -1, int CurrentY = -1)
	{
		return new bool[BoardManager.mapSize, BoardManager.mapSize];
	}
}
