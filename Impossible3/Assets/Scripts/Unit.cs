using UnityEngine;
using System.Collections;

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
	public float spellTimer;

    public int straightMoveRange;
    public int diagMoveRange;
    public int circMoveRange;

    public int straightAttackRange;
    public int diagAttackRange;
    public int circAttackRange;

	public int dodgeChance;
	public int accuracy;

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

	public void SetDodgeChance(int dodgeNew) {
		dodgeChance = dodgeNew;
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
