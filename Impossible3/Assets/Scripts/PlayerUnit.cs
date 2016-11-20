using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerUnit : Unit 
{
    //Make another pubilic override bool called PossibleAttack, with 2 sets of if statements
    //One to check if ranged attack, another to see if melee with checking surrounding spots
    //Have it check if !self.Team is in range, then subtract that things health when clicked on
    //BoardHighlights do a red prefab plane to show attack range
    //In BoardManager do a deal damage function, which gets attack power of 1st GameObject and subtracts that from health of 2nd/target GameObject
    //If health <= 0 then destroy GameObject

    public int straightMoveRange = 4;
    public int diagMoveRange = 2;
    public int straightAttackRange = 3;
    public int diagAttackRange = 3;

    //this variable isnt used but it's references elsewhere so i cant remove it
    public bool isRanged;

    public override bool[,] PossibleMove ()
	{
        //I am become Flanders Destroyer of Code

        //Tile boundries are (0,0),(0,mapsize),(mapsize,0)&(mapsize,mapsize)
        //BoardManager.mapSize

        // List of units by space
        //BoardManager.Instance.Units[x, y];

        //Current Unit
        //BoardManager.Instance.Units[CurrentX,CurrentY]

        //Current selection
        //CurrentX
        //CurrentY

        bool[,] isAcceptedMove = new bool[BoardManager.mapSize, BoardManager.mapSize];

        for(int i =1; i <= straightMoveRange; i++)
        {
            foreach (List<int> pair in new List<List<int>>{ new List<int>{CurrentX,CurrentY+i },new List<int> { CurrentX+i,CurrentY },new List<int> {CurrentX,CurrentY-i },new List<int> {CurrentX-i,CurrentY } })
            {
                int x = pair[0];
                int y = pair[1];
                if(x< BoardManager.mapSize && y< BoardManager.mapSize && x>=0 && y>=0)
                {
                    if (BoardManager.Instance.Units[x, y] == null)
                    {
                        isAcceptedMove[x, y] = true;
                    }
                }
            }
        }

        for (int i = 1; i <= diagMoveRange; i++)
        {
            foreach (List<int> pair in new List<List<int>> { new List<int> { CurrentX+i, CurrentY + i }, new List<int> { CurrentX + i, CurrentY-i }, new List<int> { CurrentX-i, CurrentY - i }, new List<int> { CurrentX - i, CurrentY+i } })
            {
                int x = pair[0];
                int y = pair[1];
                if (x < BoardManager.mapSize && y < BoardManager.mapSize && x >= 0 && y >= 0)
                {
                    if (BoardManager.Instance.Units[x, y] == null)
                    {
                        isAcceptedMove[x, y] = true;
                    }
                }
            }
        }

        return isAcceptedMove;
        
	}

	public override bool[,] PossibleAttack ()
	{
        bool[,] isAcceptedAttack = new bool[BoardManager.mapSize, BoardManager.mapSize];

        for (int i = 1; i <= straightAttackRange; i++)
        {
            foreach (List<int> pair in new List<List<int>> { new List<int> { CurrentX, CurrentY + i }, new List<int> { CurrentX + i, CurrentY }, new List<int> { CurrentX, CurrentY - i }, new List<int> { CurrentX - i, CurrentY } })
            {
                int x = pair[0];
                int y = pair[1];
                if (x < BoardManager.mapSize && y < BoardManager.mapSize && x >= 0 && y >= 0)
                {
                    if (BoardManager.Instance.Units[x, y] == null || BoardManager.Instance.Units[CurrentX, CurrentY].isPlayer!= BoardManager.Instance.Units[x, y].isPlayer)
                    {
                        isAcceptedAttack[x, y] = true;
                    }
                }
            }
        }

        for (int i = 1; i <= diagAttackRange; i++)
        {
            foreach (List<int> pair in new List<List<int>> { new List<int> { CurrentX + i, CurrentY + i }, new List<int> { CurrentX + i, CurrentY - i }, new List<int> { CurrentX - i, CurrentY - i }, new List<int> { CurrentX - i, CurrentY + i } })
            {
                int x = pair[0];
                int y = pair[1];
                if (x < BoardManager.mapSize && y < BoardManager.mapSize && x >= 0 && y >= 0)
                {
                    if (BoardManager.Instance.Units[x, y] == null || BoardManager.Instance.Units[CurrentX, CurrentY].isPlayer != BoardManager.Instance.Units[x, y].isPlayer)
                    {
                        isAcceptedAttack[x, y] = true;
                    }
                }
            }
        }

        return isAcceptedAttack;
    }


}
