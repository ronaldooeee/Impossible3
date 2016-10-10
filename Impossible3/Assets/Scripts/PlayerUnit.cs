using UnityEngine;
using System.Collections;

public class PlayerUnit : Unit 
{
	public override bool[,] PossibleMove ()
	{
		//Get a multidimensional grid of booleans representing spaces it can move to
<<<<<<< HEAD
		bool[,] r = new bool[BoardManager.mapSize, BoardManager.mapSize];
=======
		bool[,] r = new bool[8, 8];
>>>>>>> origin/master
		Unit c, c2;

		//Straight
		//If not against very top of board, do this for one square ahead
<<<<<<< HEAD
		if (CurrentY != BoardManager.mapSize - 1) 
=======
		if (CurrentY != 7) 
>>>>>>> origin/master
		{
			c = BoardManager.Instance.Units [CurrentX, CurrentY + 1];
			if (c == null)
				r [CurrentX, CurrentY + 1] = true;
<<<<<<< HEAD
			if (CurrentY != BoardManager.mapSize - 2) 
=======
			if (CurrentY != 6) 
>>>>>>> origin/master
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
<<<<<<< HEAD
		if (CurrentX != BoardManager.mapSize - 1) 
=======
		if (CurrentX != 7) 
>>>>>>> origin/master
		{
			c = BoardManager.Instance.Units [CurrentX + 1, CurrentY];
			if (c == null)
				r [CurrentX + 1, CurrentY] = true;
<<<<<<< HEAD
			if (CurrentX != BoardManager.mapSize - 2) 
=======
			if (CurrentX != 6) 
>>>>>>> origin/master
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
<<<<<<< HEAD
			if (CurrentY != BoardManager.mapSize - 1) {
=======
			if (CurrentY != 7) {
>>>>>>> origin/master
				//Check BoardObject at that x,y value
				c = BoardManager.Instance.Units [CurrentX - 1, CurrentY + 1];
				//If its open, be able to move there
				if (c == null)
					r [CurrentX - 1, CurrentY + 1] = true;
			}
		}

		//Diag Front Right
		//If not against right side of board or at very top of board, Do this
<<<<<<< HEAD
		if (CurrentX != BoardManager.mapSize - 1) 
		{
			if (CurrentY != BoardManager.mapSize - 1) {
=======
		if (CurrentX != 7) 
		{
			if (CurrentY != 7) {
>>>>>>> origin/master
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
<<<<<<< HEAD
		if (CurrentX != BoardManager.mapSize - 1) 
=======
		if (CurrentX != 7) 
>>>>>>> origin/master
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
