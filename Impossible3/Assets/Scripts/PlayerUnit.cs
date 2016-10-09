using UnityEngine;
using System.Collections;

public class PlayerUnit : Unit 
{
	public override bool[,] PossibleMove ()
	{
		//Get a multidimensional grid of booleans representing spaces it can move to
		bool[,] r = new bool[BoardManager.mapSize, BoardManager.mapSize];
		Unit c, c2;

		//Straight
		//If not against very top of board, do this for one square ahead
		if (CurrentY != BoardManager.mapSize - 1) 
		{
			c = BoardManager.Instance.Units [CurrentX, CurrentY + 1];
			if (c == null)
				r [CurrentX, CurrentY + 1] = true;
			if (CurrentY != BoardManager.mapSize - 2) 
			{
				c2 = BoardManager.Instance.Units [CurrentX, CurrentY + 2];
				if (c2 == null)
					r [CurrentX, CurrentY + 2] = true;
			}
		}

		//Back
		if (CurrentY != 0) 
		{
			c = BoardManager.Instance.Units [CurrentX, CurrentY - 1];
			if (c == null)
				r [CurrentX, CurrentY - 1] = true;
			if (CurrentY != 1) 
			{
				c2 = BoardManager.Instance.Units [CurrentX, CurrentY - 2];
				if (c2 == null)
					r [CurrentX, CurrentY - 2] = true;
			}
		}

		//Left
		if (CurrentX != 0) 
		{
			c = BoardManager.Instance.Units [CurrentX - 1, CurrentY];
			if (c == null)
				r [CurrentX - 1, CurrentY] = true;
			if (CurrentX != 1) 
			{
				c2 = BoardManager.Instance.Units [CurrentX - 2, CurrentY];
				if (c2 == null)
					r [CurrentX - 2, CurrentY] = true;
			}
		}

		//Right
		if (CurrentX != BoardManager.mapSize - 1) 
		{
			c = BoardManager.Instance.Units [CurrentX + 1, CurrentY];
			if (c == null)
				r [CurrentX + 1, CurrentY] = true;
			if (CurrentX != BoardManager.mapSize - 2) 
			{
				c2 = BoardManager.Instance.Units [CurrentX + 2, CurrentY];
				if (c2 == null)
					r [CurrentX + 2, CurrentY] = true;
			}
		}

		//Diag Front Left
		//If not against left side of board or at very top of board, Do this
		if (CurrentX != 0) 
		{
			if (CurrentY != BoardManager.mapSize - 1) {
				//Check BoardObject at that x,y value
				c = BoardManager.Instance.Units [CurrentX - 1, CurrentY + 1];
				//If its open, be able to move there
				if (c == null)
					r [CurrentX - 1, CurrentY + 1] = true;
			}
		}

		//Diag Front Right
		//If not against right side of board or at very top of board, Do this
		if (CurrentX != BoardManager.mapSize - 1) 
		{
			if (CurrentY != BoardManager.mapSize - 1) {
				c = BoardManager.Instance.Units [CurrentX + 1, CurrentY + 1];
				if (c == null)
					r [CurrentX + 1, CurrentY + 1] = true;
			}
		}

		//Diag Back Left
		if (CurrentX != 0) 
		{
			if (CurrentY != 0) {
				//Check BoardObject at that x,y value
				c = BoardManager.Instance.Units [CurrentX - 1, CurrentY - 1];
				//If its open, be able to move there
				if (c == null)
					r [CurrentX - 1, CurrentY - 1] = true;
			}
		}

		//Diag Back Right
		if (CurrentX != BoardManager.mapSize - 1) 
		{
			if (CurrentY != 0) {
				//Check BoardObject at that x,y value
				c = BoardManager.Instance.Units [CurrentX + 1, CurrentY - 1];
				//If its open, be able to move there
				if (c == null)
					r [CurrentX + 1, CurrentY - 1] = true;
			}
		}

		return r;
	}
}
