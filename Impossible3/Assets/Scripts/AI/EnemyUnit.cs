using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EnemyUnit : Unit
{
    Unit unitInstance;
    List<GameObject> playerUnits;
    PlayerUnit enemyUnit;

    private void Start()
    {
        playerUnits = BoardManager.playerUnits;
        enemyUnit = this.GetComponentInParent<PlayerUnit>();
        unitInstance = enemyUnit.GetComponent<PlayerUnit>();

        //Enemy positions inherited from Unit
        //CurrentX, CurrentY

        unitInstance.timeStampAttack = Time.time + unitInstance.cooldownAttackSeconds;
        unitInstance.timeStampMove = Time.time + unitInstance.cooldownMoveSeconds;
    }

    private void Update()
    {
		int damage = this.GetComponent<PlayerUnit> ().damageAmount;
        //Check Enemy Move / Attack Timer

        if (unitInstance.timeStampAttack <= Time.time)
        {
            bool[,] allowedEnemyAttacks = unitInstance.PossibleAttack(CurrentX, CurrentY);
            //BoardHighlights.Instance.Hidehighlights();
            //BoardHighlights.Instance.HighlightAllowedAttacks(allowedEnemyAttacks);
            //Determine if player is in Attack distance
            foreach (GameObject player in playerUnits)
            {
                if (player)
                {
                    Unit playerU = player.GetComponent<PlayerUnit>();
                    int playerX = playerU.CurrentX;
                    int playerY = playerU.CurrentY;
                    if (allowedEnemyAttacks[playerX, playerY])
                    {
                        //If yes then attack
                        HealthSystem health = (HealthSystem)BoardManager.Units[playerX, playerY].GetComponent(typeof(HealthSystem));
						Debug.Log (damageAmount);
                        if (health.takeDamageAndDie(damage))
                        {
							
                            // Remove player from list.
                            foreach (GameObject spawn in playerUnits)
                            {
                                if (System.Object.ReferenceEquals(spawn, BoardManager.Units[playerX, playerY].gameObject))
                                {
                                    playerUnits.Remove(spawn);
                                    Destroy(spawn);
                                }
                            }
                        }
                        unitInstance.timeStampAttack = Time.time + unitInstance.cooldownAttackSeconds;
                        return;

                    }
                }

            }


        }


        //If Movemvent cooldown over and 
        if (unitInstance.timeStampMove <= Time.time)
        {
            List<int[]> allowedEnemyMoves = getTrueMoves();
            Shuffle(allowedEnemyMoves);
            //BoardHighlights.Instance.Hidehighlights();
            //BoardHighlights.Instance.HighlightAllowedMoves(allowedEnemyMoves);
            foreach (int[] move in allowedEnemyMoves)
            {
                foreach (GameObject player in playerUnits)
                {
                    if (player)
                    {
                        Unit playerU = player.GetComponent<PlayerUnit>();
                        int playerX = playerU.CurrentX;
                        int playerY = playerU.CurrentY;
                        //Check if this will move enemy closer to a player
                        if (Math.Abs(CurrentX - playerX) > Math.Abs(move[0] - playerX) || Math.Abs(CurrentY - playerY) > Math.Abs(move[1] - playerY))
                        {
                            BoardManager.Units[CurrentX, CurrentY] = null;
                            this.transform.position = BoardManager.Instance.GetTileCenter(move[0], move[1], 0);
                            this.SetPosition(move[0], move[1]);
                            BoardManager.Units[move[0], move[1]] = enemyUnit;
                            unitInstance.timeStampMove = Time.time + unitInstance.cooldownMoveSeconds;
                            return;
                        }
                    }

                }



            }
        }
    }

    

    private List<int[]> getTrueMoves()
    {
        bool[,] allowedEnemyMoves = unitInstance.PossibleMove(CurrentX, CurrentY);
        List<int[]> trueMoves = new List<int[]> { };
        for (int x = 0; x < allowedEnemyMoves.GetLength(0); x++)
        {
            for (int y = 0; y < allowedEnemyMoves.GetLength(1); y++)
            {
                if (allowedEnemyMoves[x, y])
                {
                    trueMoves.Add(new int[] {x,y});
                }
            }
        }
        return trueMoves;
    }



    public void Shuffle(List<int[]> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int[] value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

