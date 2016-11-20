using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyUnit : Unit
{
    private void Update()
    {
        List<GameObject> playerUnits = BoardManager.playerUnits;
        PlayerUnit enemyUnit = this.GetComponentInParent<PlayerUnit>();
        int xpos = (int)enemyUnit.transform.position.x;
        int ypos = (int)enemyUnit.transform.position.y;

        Unit unitInstance = enemyUnit.GetComponent<PlayerUnit>();

        //Check Enemy Move / Attack Timer
        bool[,] allowedEnemyAttacks = unitInstance.PossibleAttack();

        for (int x = 0; x < allowedEnemyAttacks.GetLength(0); x++)
        {
            for (int y = 0; y < allowedEnemyAttacks.GetLength(1); y++)
            {
                //Determine if player is in Attack distance


                if (allowedEnemyAttacks[x, y] && BoardManager.Instance.Units[x, y] != null)
                {
                    if (BoardManager.Instance.Units[x, y].isPlayer && unitInstance.timeStampAttack <= Time.time)
                    {
                        /*
                        //If yes then attack
                        HealthSystem health = (HealthSystem)BoardManager.Instance.Units[x, y].GetComponent(typeof(HealthSystem));
                        health.TakeDamage(damageAmmount);
                        unitInstance.timeStampAttack = Time.time + unitInstance.cooldownAttackSeconds;
                        */
                    }
                }

            }

            //Determine if player is in Attack distance


            //Reset Attack Cooldown

        }

        bool[,] allowedEnemyMoves = unitInstance.PossibleMove();
        //If Movemvent cooldown over and 

        for (int x = 0; x < allowedEnemyMoves.GetLength(0); x++)
        {
            for (int y = 0; y < allowedEnemyMoves.GetLength(1); y++)
            {
                if (unitInstance.timeStampMove <= Time.time)
                {
                    /*
                    enemyUnit.transform.position = BoardManager.Instance.GetTileCenter(x, y, 0);
                    //Move it there
                    enemyUnit.SetPosition(x, y);
                    //Set that unit's coordinates to desinations coordinates
                    BoardManager.Instance.Units[x, y] = enemyUnit;
                    unitInstance.timeStampMove = Time.time + unitInstance.cooldownMoveSeconds;
                    */
                }
            }
        }


    }

}