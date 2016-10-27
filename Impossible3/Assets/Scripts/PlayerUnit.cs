using UnityEngine;
using System.Collections;

public class PlayerUnit : Unit 
{
	public bool isRanged;

	//Make another pubilic override bool called PossibleAttack, with 2 sets of if statements
	//One to check if ranged attack, another to see if melee with checking surrounding spots
	//Have it check if !self.Team is in range, then subtract that things health when clicked on
	//BoardHighlights do a red prefab plane to show attack range
	//In BoardManager do a deal damage function, which gets attack power of 1st GameObject and subtracts that from health of 2nd/target GameObject
	//If health <= 0 then destroy GameObject

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

	public override bool[,] PossibleAttack ()
	{
		//Get a multidimensional grid of booleans representing spaces it can attack
		bool[,] aSpaces = new bool[BoardManager.mapSize, BoardManager.mapSize];
		Unit u, u2;

		//Straight
		//If not against very top of board, do this for one square ahead
		if (CurrentY != BoardManager.mapSize - 1) 
		{
			u = BoardManager.Instance.Units [CurrentX, CurrentY + 1];
			//if (u.GetComponent<EnemyUnit>() != null)
			aSpaces [CurrentX, CurrentY + 1] = true;

			if (CurrentY != BoardManager.mapSize - 2 && isRanged) 
			{
				u2 = BoardManager.Instance.Units [CurrentX, CurrentY + 2];
				//if (u2.GetComponent<EnemyUnit>() != null)
				aSpaces [CurrentX, CurrentY + 2] = true;
			}
		}

		//Back
		if (CurrentY != 0) 
		{
			u = BoardManager.Instance.Units [CurrentX, CurrentY - 1];
			//if (u.GetComponent<EnemyUnit>() != null)
			aSpaces [CurrentX, CurrentY - 1] = true;

			if (CurrentY != 1 && isRanged) 
			{
				u2 = BoardManager.Instance.Units [CurrentX, CurrentY - 2];
				//if (u2.GetComponent<EnemyUnit>() != null)
				aSpaces [CurrentX, CurrentY - 2] = true;
			}
		}

		//Left
		if (CurrentX != 0) 
		{
			u = BoardManager.Instance.Units [CurrentX - 1, CurrentY];
			//if (u.GetComponent<EnemyUnit>() != null)
			aSpaces [CurrentX - 1, CurrentY] = true;

			if (CurrentX != 1 && isRanged) 
			{
				u2 = BoardManager.Instance.Units [CurrentX - 2, CurrentY];
				//if (u2.GetComponent<EnemyUnit>() != null)
				aSpaces [CurrentX - 2, CurrentY] = true;
			}
		}

		//Right

		if (CurrentX != BoardManager.mapSize - 1) 
		{
			u = BoardManager.Instance.Units [CurrentX + 1, CurrentY];
			//if (u.GetComponent<EnemyUnit>() != null)
			aSpaces [CurrentX + 1, CurrentY] = true;

			if (CurrentX != BoardManager.mapSize - 2 && isRanged) 
			{
				u2 = BoardManager.Instance.Units [CurrentX + 2, CurrentY];
				//if (u2.GetComponent<EnemyUnit>() != null)
				aSpaces [CurrentX + 2, CurrentY] = true;
			}
		}

		//Diag Front Left
		//If not against left side of board or at very top of board, Do this
		if (CurrentX != 0 && isRanged) 
		{
			if (CurrentY != BoardManager.mapSize - 1) {
				//Check BoardObject at that x,y value
				u = BoardManager.Instance.Units [CurrentX - 1, CurrentY + 1];
				//If its open, be able to move there
				//if (u.GetComponent<EnemyUnit>() != null)
				aSpaces [CurrentX - 1, CurrentY + 1] = true;
			}
		}

		//Diag Front Right
		//If not against right side of board or at very top of board, Do this
		if (CurrentX != BoardManager.mapSize - 1 && isRanged) 
		{
			if (CurrentY != BoardManager.mapSize - 1) {
				u = BoardManager.Instance.Units [CurrentX + 1, CurrentY + 1];
				//if (u.GetComponent<EnemyUnit>() != null)
				aSpaces [CurrentX + 1, CurrentY + 1] = true;
			}
		}

		//Diag Back Left
		if (CurrentX != 0 && isRanged) 
		{
			if (CurrentY != 0) {
				//Check BoardObject at that x,y value
				u = BoardManager.Instance.Units [CurrentX - 1, CurrentY - 1];
				//If its open, be able to move there
				//if (u.GetComponent<EnemyUnit>() != null)
				aSpaces [CurrentX - 1, CurrentY - 1] = true;
			}
		}

		//Diag Back Right
		if (CurrentX != BoardManager.mapSize - 1 && isRanged) 
		{
			if (CurrentY != 0) {
				//Check BoardObject at that x,y value
				u = BoardManager.Instance.Units [CurrentX + 1, CurrentY - 1];
				//If its open, be able to move there
				//if (u.GetComponent<EnemyUnit>() != null)
				aSpaces [CurrentX + 1, CurrentY - 1] = true;
			}
		}

		return aSpaces;
	}
}
