using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerUnit : Unit
{
    //Make another pubilic override bool called PossibleAttack, with 2 sets of if statements
    //One to check if ranged attack, another to see if melee with checking surrounding spots
    //Have it check if !self.Team is in range, then subtract that things health when clicked on
    //BoardHighlights do a red prefab plane to show attack range
    //In BoardManager do a deal damage function, which gets attack power of 1st GameObject and subtracts that from health of 2nd/target GameObject
    //If health <= 0 then destroy GameObject

    //this variable isnt used but it's references elsewhere so i cant remove it
    public bool isRanged;

    public override bool[,] PossibleMove(int currentXPos = -1, int currentYPos = -1)
    {
        if (currentXPos == -1) { currentXPos = CurrentX; }
        if (currentYPos == -1) { currentYPos = CurrentY; }

        //Tile boundries are (0,0),(0,mapsize),(mapsize,0)&(mapsize,mapsize)
        //BoardManager.mapSize

        // List of units by space
        //BoardManager.Instance.Units[x, y];

        //Current Unit
        //BoardManager.Instance.Units[currentXPos,currentYPos]

        //Current selection
        //Debug.Log(currentXPos + " " + currentYPos);
        //Debug.Log(CurrentX + " " + CurrentY);

        bool[,] isAcceptedMove = new bool[BoardManager.mapSize, BoardManager.mapSize];

        for (int i = 1; i <= straightMoveRange; i++)
        {
            foreach (List<int> pair in new List<List<int>> { new List<int> { currentXPos, currentYPos + i }, new List<int> { currentXPos + i, currentYPos }, new List<int> { currentXPos, currentYPos - i }, new List<int> { currentXPos - i, currentYPos } })
            {
                int x = pair[0];
                int y = pair[1];
                if (x < BoardManager.mapSize && y < BoardManager.mapSize && x >= 0 && y >= 0)
                {
                    if (BoardManager.Units[x, y] == null)
                    {
                        isAcceptedMove[x, y] = true;
                    }
                }
            }
        }

        for (int i = 1; i <= diagMoveRange; i++)
        {
            foreach (List<int> pair in new List<List<int>> { new List<int> { currentXPos + i, currentYPos + i }, new List<int> { currentXPos + i, currentYPos - i }, new List<int> { currentXPos - i, currentYPos - i }, new List<int> { currentXPos - i, currentYPos + i } })
            {
                int x = pair[0];
                int y = pair[1];
                if (x < BoardManager.mapSize && y < BoardManager.mapSize && x >= 0 && y >= 0)
                {
                    if (BoardManager.Units[x, y] == null)
                    {
                        isAcceptedMove[x, y] = true;
                    }
                }
            }
        }

        for (int i = 0 - circMoveRange; i <= circMoveRange; i++)
        {
            int x = currentXPos + i;
            for (int j = 0 - circMoveRange; j <= circMoveRange; j++)
            {
                int y = currentYPos + j;
                if (x < BoardManager.mapSize && y < BoardManager.mapSize && x >= 0 && y >= 0 && isAcceptedMove[x, y] != true)
                {
                    if (BoardManager.Units[x, y] == null)
                    {
                        if ((x != currentXPos + circMoveRange && x != currentXPos - circMoveRange) || (y != currentYPos + circMoveRange && y != currentYPos - circMoveRange))
                        {
                            isAcceptedMove[x, y] = true;
                        }
                    }
                }
            }
        }
        return isAcceptedMove;

    }

    public override bool[,] PossibleAttack(int currentXPos = -1, int currentYPos = -1)
    {
        if (currentXPos == -1) { currentXPos = CurrentX; }
        if (currentYPos == -1) { currentYPos = CurrentY; }
        bool[,] isAcceptedAttack = new bool[BoardManager.mapSize, BoardManager.mapSize];

        for (int i = 1; i <= straightAttackRange; i++)
        {
            foreach (List<int> pair in new List<List<int>> { new List<int> { currentXPos, currentYPos + i }, new List<int> { currentXPos + i, currentYPos }, new List<int> { currentXPos, currentYPos - i }, new List<int> { currentXPos - i, currentYPos } })
            {
                int x = pair[0];
                int y = pair[1];
                if (x < BoardManager.mapSize && y < BoardManager.mapSize && x >= 0 && y >= 0)
                {
                    if (BoardManager.Units[x, y] == null || (this.isPlayer != BoardManager.Units[x, y].isPlayer && !BoardManager.Units[x, y].isObstacle))
                    {
                        isAcceptedAttack[x, y] = true;
                    }
                }
            }
        }

        for (int i = 1; i <= diagAttackRange; i++)
        {
            foreach (List<int> pair in new List<List<int>> { new List<int> { currentXPos + i, currentYPos + i }, new List<int> { currentXPos + i, currentYPos - i }, new List<int> { currentXPos - i, currentYPos - i }, new List<int> { currentXPos - i, currentYPos + i } })
            {
                int x = pair[0];
                int y = pair[1];
                if (x < BoardManager.mapSize && y < BoardManager.mapSize && x >= 0 && y >= 0)
                {
                    if (BoardManager.Units[x, y] == null || (this.isPlayer != BoardManager.Units[x, y].isPlayer && !BoardManager.Units[x, y].isObstacle))
                    {
                        isAcceptedAttack[x, y] = true;
                    }
                }
            }
        }

        for (int i = 0 - circAttackRange; i <= circAttackRange; i++)
        {
            int x = currentXPos + i;
            for (int j = 0 - circAttackRange; j <= circAttackRange; j++)
            {
                int y = currentYPos + j;
                if (x < BoardManager.mapSize && y < BoardManager.mapSize && x >= 0 && y >= 0 && isAcceptedAttack[x, y] != true)
                {
                    if ((x != currentXPos + circAttackRange && x != currentXPos - circAttackRange) || (y != currentYPos + circAttackRange && y != currentYPos - circAttackRange))
                    {
                        if (BoardManager.Units[x, y] == null || (this.isPlayer != BoardManager.Units[x, y].isPlayer && !BoardManager.Units[x, y].isObstacle))
                        {
                            isAcceptedAttack[x, y] = true;
                        }
                    }
                }
            }
        }

        return isAcceptedAttack;
    }


    public override bool[,] PossibleAbility(int currentXPos = -1, int currentYPos = -1)
    {
        if (currentXPos == -1) { currentXPos = CurrentX; }
        if (currentYPos == -1) { currentYPos = CurrentY; }
        bool[,] isAcceptedAbility = new bool[BoardManager.mapSize, BoardManager.mapSize];

        for (int i = 1; i <= straightAttackRange; i++)
        {
            foreach (List<int> pair in new List<List<int>> { new List<int> { currentXPos, currentYPos + i }, new List<int> { currentXPos + i, currentYPos }, new List<int> { currentXPos, currentYPos - i }, new List<int> { currentXPos - i, currentYPos } })
            {
                int x = pair[0];
                int y = pair[1];
                if (x < BoardManager.mapSize && y < BoardManager.mapSize && x >= 0 && y >= 0)
                {
                    if (BoardManager.Units[x, y] == null || (this.isPlayer == BoardManager.Units[x, y].isPlayer && !BoardManager.Units[x, y].isObstacle))
                    {
                        isAcceptedAbility[x, y] = true;
                    }
                }
            }
        }

        for (int i = 1; i <= diagAttackRange; i++)
        {
            foreach (List<int> pair in new List<List<int>> { new List<int> { currentXPos + i, currentYPos + i }, new List<int> { currentXPos + i, currentYPos - i }, new List<int> { currentXPos - i, currentYPos - i }, new List<int> { currentXPos - i, currentYPos + i } })
            {
                int x = pair[0];
                int y = pair[1];
                if (x < BoardManager.mapSize && y < BoardManager.mapSize && x >= 0 && y >= 0)
                {
                    if (BoardManager.Units[x, y] == null || (this.isPlayer == BoardManager.Units[x, y].isPlayer && !BoardManager.Units[x, y].isObstacle))
                    {
                        isAcceptedAbility[x, y] = true;
                    }
                }
            }
        }

        for (int i = 0 - circAttackRange; i <= circAttackRange; i++)
        {
            int x = currentXPos + i;
            for (int j = 0 - circAttackRange; j <= circAttackRange; j++)
            {
                int y = currentYPos + j;
                if (x < BoardManager.mapSize && y < BoardManager.mapSize && x >= 0 && y >= 0 && isAcceptedAbility[x, y] != true)
                {
                    if (BoardManager.Units[x, y] == null || (this.isPlayer == BoardManager.Units[x, y].isPlayer && !BoardManager.Units[x, y].isObstacle))
                    {
                        if ((x != currentXPos + circAttackRange && x != currentXPos - circAttackRange) || (y != currentYPos + circAttackRange && y != currentYPos - circAttackRange))
                        {
                            if (BoardManager.Units[x, y] == null || (this.isPlayer != BoardManager.Units[x, y].isPlayer && !BoardManager.Units[x, y].isObstacle))
                            {
                                isAcceptedAbility[x, y] = true;
                            }
                        }
                    }
                }
            }
        }

        isAcceptedAbility[currentXPos, currentYPos] = true;

        return isAcceptedAbility;
    }

}
