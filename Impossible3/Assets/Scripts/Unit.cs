using UnityEngine;
using System.Collections;

public abstract class Unit : MonoBehaviour {

	public int CurrentX { set; get; }
	public int CurrentY { set; get; }

	public bool isPlayer;

	public float cooldownMoveSeconds = 3.0f;
	public float cooldownAttackSeconds = 5.0f;
	public float timeStampMove = 0.0f;
	public float timeStampAttack = 0.0f;

	public void SetPosition(int x, int y)
	{
		CurrentX = x;
		CurrentY = y;
	}

	public virtual bool[,] PossibleMove()
	{
		return new bool[BoardManager.mapSize, BoardManager.mapSize];
	}

	public virtual bool[,] PossibleAttack()
	{
		return new bool[BoardManager.mapSize, BoardManager.mapSize];
	}
}
