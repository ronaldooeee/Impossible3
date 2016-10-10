using UnityEngine;
using System.Collections;

public abstract class Unit : MonoBehaviour {

<<<<<<< HEAD
	public int CurrentX { set; get; }
	public int CurrentY { set; get; }
=======
	public int CurrentX{ set; get; }
	public int CurrentY{ set; get; }
>>>>>>> origin/master
	public bool isPlayer;

	public void SetPosition(int x, int y)
	{
		CurrentX = x;
		CurrentY = y;
	}

	public virtual bool[,] PossibleMove()
	{
<<<<<<< HEAD
		return new bool[BoardManager.mapSize, BoardManager.mapSize];
	}
}
=======
		return new bool[8,8];
	}
}
>>>>>>> origin/master
