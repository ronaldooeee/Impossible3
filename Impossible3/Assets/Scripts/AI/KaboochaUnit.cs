using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class KaboochaUnit : Unit
{
    List<GameObject> playerUnits;
    List<GameObject> enemyUnits;
    PlayerUnit enemyUnit;


    public float timeStampDelay;

    private void Start()
    {
        timeStampDelay = Time.time;
        playerUnits = BoardManager.playerUnits;
        enemyUnits = BoardManager.enemyUnits;
        enemyUnit = this.GetComponentInParent<PlayerUnit>();

        enemyUnit.timeStampAttack = Time.time + enemyUnit.cooldownAttackSeconds;
        enemyUnit.timeStampMove = Time.time + enemyUnit.cooldownMoveSeconds;
    }

    private void Update()
    {
        int damage = this.GetComponent<PlayerUnit>().damageAmount;
        int accuracy = this.GetComponent<PlayerUnit>().accuracy;
        int targetDodgeChance;
        Unit closestPlayer = null;
        int playerDistance = 100;

        Unit closestEnemy = null;
        int enemyDistance = 100;

        //find closest player
        for (int i = 0; i < playerUnits.Count; i++)
        {
            PlayerUnit currentPlayer = playerUnits[i].GetComponent<PlayerUnit>();
            int xDist = Math.Abs(enemyUnit.CurrentX - currentPlayer.CurrentX);
            int yDist = Math.Abs(enemyUnit.CurrentY - currentPlayer.CurrentY);
            if (xDist + yDist < playerDistance)
            {
                closestPlayer = currentPlayer;
                playerDistance = xDist + yDist;
            }
        }

        //find closest enemy
        for (int i = 0; i < enemyUnits.Count; i++)
        {
            if (enemyUnits[i].GetComponent<KaboochaAbilities>() == null)
            {
                PlayerUnit currentEnemy = enemyUnits[i].GetComponent<PlayerUnit>();
                int xDist = Math.Abs(enemyUnit.CurrentX - currentEnemy.CurrentX);
                int yDist = Math.Abs(enemyUnit.CurrentY - currentEnemy.CurrentY);
                if (xDist + yDist < playerDistance)
                {
                    closestEnemy = currentEnemy;
                    enemyDistance = xDist + yDist;
                }
            }
        }

        //If Attack cooldown over, then attack
        if (enemyUnit.timeStampAttack <= Time.time && timeStampDelay <= Time.time)
        {
            bool[,] allowedEnemyAttacks = enemyUnit.PossibleAttack();
            bool[,] allowedEnemyAbilities = enemyUnit.PossibleAbility();

            //BoardHighlights.Instance.Hidehighlights();
            //BoardHighlights.Instance.HighlightAllowedAbilities(allowedEnemyAbilities);
            if (allowedEnemyAbilities[closestEnemy.CurrentX, closestEnemy.CurrentY]) {

                    HealthSystem health = (HealthSystem)BoardManager.Units[closestEnemy.CurrentX, closestEnemy.CurrentY].GetComponent(typeof(HealthSystem));
                    int rand = UnityEngine.Random.Range(1, 4);
                    Debug.Log(closestEnemy);
                    Debug.Log("Buffing");
                    if (rand == 1)
                    {
                        BoardManager.Units[closestEnemy.CurrentX, closestEnemy.CurrentY].GetComponent<Unit>().timeStampAttack = Time.time;
                    }
                    else if (rand == 2)
                    {
                        BoardManager.Units[closestEnemy.CurrentX, closestEnemy.CurrentY].GetComponent<HealthSystem>().takeDamageAndDie(-20);
                    }
                    else if (rand == 3)
                    {
                        BoardManager.Units[closestEnemy.CurrentX, closestEnemy.CurrentY].GetComponent<Unit>().timeStampMove = Time.time;
                    }
                    enemyUnit.timeStampAttack = Time.time + enemyUnit.cooldownAttackSeconds;
                    return;
            }
            

            
        }



        //If Movemvent cooldown over, then Move
        if (enemyUnit.timeStampMove <= Time.time)
        {
            List<int[]> allowedEnemyMoves = getTrueMoves();
            Shuffle(allowedEnemyMoves);
            //BoardHighlights.Instance.Hidehighlights();
            //BoardHighlights.Instance.HighlightAllowedMoves(enemyUnit.PossibleMove());

            Debug.Log(closestEnemy);    
            foreach (int[] move in allowedEnemyMoves)
            {
                //Check if this will move enemy closer to a player
                if (Math.Abs(enemyUnit.CurrentX - closestEnemy.CurrentX) > Math.Abs(move[0] - closestEnemy.CurrentX) || Math.Abs(enemyUnit.CurrentY - closestEnemy.CurrentY) > Math.Abs(move[1] - closestEnemy.CurrentY))
                {
                    if (BoardManager.Units[move[0], move[1]] == null)
                    {
                        BoardManager.Units[enemyUnit.CurrentX, enemyUnit.CurrentY] = null;
                        enemyUnit.transform.position = BoardManager.Instance.GetTileCenter(move[0], move[1], 0);
                        enemyUnit.SetPosition(move[0], move[1]);
                        BoardManager.Units[move[0], move[1]] = enemyUnit;
                        enemyUnit.timeStampMove = Time.time + enemyUnit.cooldownMoveSeconds;
                        return;
                    }
                }
            }
                

        }
    }





    private List<int[]> getTrueMoves()
    {
        bool[,] allowedEnemyMoves = enemyUnit.PossibleMove();
        List<int[]> trueMoves = new List<int[]> { };
        for (int x = 0; x < allowedEnemyMoves.GetLength(0); x++)
        {
            for (int y = 0; y < allowedEnemyMoves.GetLength(1); y++)
            {
                if (allowedEnemyMoves[x, y])
                {
                    trueMoves.Add(new int[] { x, y });
                }
            }
        }
        return trueMoves;
    }



    public void Shuffle(List<int[]> list)
    {
        //UnityEngine.Random rng = new UnityEngine.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = (int)UnityEngine.Random.Range(0, n + 1);
            int[] value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

