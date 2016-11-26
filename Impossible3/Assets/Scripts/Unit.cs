using UnityEngine;
using System.Collections;

public abstract class Unit : MonoBehaviour {

    public int CurrentX;
    public int CurrentY;

    public bool isPlayer = false;

	public float cooldownMoveSeconds = 3.0f;
	public float cooldownAttackSeconds = 5.0f;
	public float timeStampMove = Time.time;
	public float timeStampAttack = Time.time;

    public int damageAmmount = 50;

    public void SetPosition(int x, int y)
	{
		CurrentX = x;
		CurrentY = y;
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
