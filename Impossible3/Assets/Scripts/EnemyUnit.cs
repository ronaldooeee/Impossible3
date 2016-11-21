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
                    Debug.Log("Looking for players");
                    Unit playerU = player.GetComponent<PlayerUnit>();
                    int playerX = playerU.CurrentX;
                    int playerY = playerU.CurrentY;
                    if (allowedEnemyAttacks[playerX, playerY])
                    {
                        Debug.Log("Found a Target");
                        //If yes then attack
                        HealthSystem health = (HealthSystem)BoardManager.Instance.Units[playerX, playerY].GetComponent(typeof(HealthSystem));
                        health.TakeDamage(damageAmmount);
                        unitInstance.timeStampAttack = Time.time + unitInstance.cooldownAttackSeconds;
                        return;

                    }
                }

             }

         
        }

        
        //If Movemvent cooldown over and 
        if (unitInstance.timeStampMove <= Time.time)
        {
            bool[,] allowedEnemyMoves = unitInstance.PossibleMove(CurrentX, CurrentY);
            //BoardHighlights.Instance.Hidehighlights();
            //BoardHighlights.Instance.HighlightAllowedMoves(allowedEnemyMoves);
            for (int x = 0; x < allowedEnemyMoves.GetLength(0); x++)
            {
                for (int y = 0; y < allowedEnemyMoves.GetLength(1); y++)
                {
                    if (allowedEnemyMoves[x, y])
                    {
                        foreach (GameObject player in playerUnits)
                        {
                            if (player)
                            {
                                Unit playerU = player.GetComponent<PlayerUnit>();
                                int playerX = playerU.CurrentX;
                                int playerY = playerU.CurrentY;
                                //Check if this will move enemy closer to a player
                                Debug.Log("I'm trying as hard as I can");
                                if (Math.Abs(CurrentX - playerX) > Math.Abs(x - playerX) || Math.Abs(CurrentY - playerY) > Math.Abs(y - playerY))
                                {
                                    this.transform.position = BoardManager.Instance.GetTileCenter(x, y, 0);
                                    this.SetPosition(x, y);
                                    BoardManager.Instance.Units[x, y] = enemyUnit;
                                    unitInstance.timeStampMove = Time.time + unitInstance.cooldownMoveSeconds;
                                    return;
                                }
                            }
                        }

                    }
                }
            }
        }

    }

}