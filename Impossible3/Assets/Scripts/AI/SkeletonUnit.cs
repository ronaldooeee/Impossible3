using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SkeletonUnit : Unit
{
    List<GameObject> playerUnits;
    PlayerUnit enemyUnit;

    public float timeStampDelay;

    /*private void Start()
    {
        timeStampDelay = Time.time;
        playerUnits = BoardManager.playerUnits;
        enemyUnit = this.GetComponentInParent<PlayerUnit>();

        enemyUnit.timeStampAttack = Time.time + enemyUnit.cooldownAttackSeconds;
        enemyUnit.timeStampMove = Time.time + enemyUnit.cooldownMoveSeconds;
    }

    private void Update()
    {
        int damage = this.GetComponent<PlayerUnit>().damageAmount;
		int accuracy = this.GetComponent<PlayerUnit> ().accuracy;
		int targetDodgeChance;
        Unit closestPlayer = null;
        int playerDistance = 100;

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

        //If Attack cooldown over, then attack
        if (enemyUnit.timeStampAttack <= Time.time && timeStampDelay <= Time.time)
        {
            bool[,] allowedEnemyAttacks = enemyUnit.PossibleAttack();
            //BoardHighlights.Instance.Hidehighlights();
            //BoardHighlights.Instance.HighlightAllowedAttacks(allowedEnemyAttacks);

            //Determine if player is in Attack distance
            if (allowedEnemyAttacks[closestPlayer.CurrentX, closestPlayer.CurrentY])
            {
                //If yes then attack
				targetDodgeChance = closestPlayer.dodgeChance + UnityEngine.Random.Range(0, 100);
                HealthSystem health = (HealthSystem)BoardManager.Units[closestPlayer.CurrentX, closestPlayer.CurrentY].GetComponent(typeof(HealthSystem));
				if (accuracy >= targetDodgeChance) {
					if (health.takeDamageAndDie (damage)) {

						// Remove player from list.
						foreach (GameObject spawn in playerUnits) {
							if (System.Object.ReferenceEquals (spawn, BoardManager.Units [closestPlayer.CurrentX, closestPlayer.CurrentY].gameObject)) {
								playerUnits.Remove (spawn);
								Destroy (spawn);
								BoardHighlights.Instance.Hidehighlights ();
							}
						}
					}
					enemyUnit.timeStampAttack = Time.time + enemyUnit.cooldownAttackSeconds;
					return;
				} else {
					Debug.Log ("Enemy Missed!");
					enemyUnit.timeStampAttack = Time.time + enemyUnit.cooldownAttackSeconds;
					return;
				}
            }                        
        }



        //If Movemvent cooldown over, then Move
        if (enemyUnit.timeStampMove <= Time.time)
        {
            List<int[]> allowedEnemyMoves = getTrueMoves();
            Shuffle(allowedEnemyMoves);
            //BoardHighlights.Instance.Hidehighlights();
            //BoardHighlights.Instance.HighlightAllowedMoves(enemyUnit.PossibleMove());
            foreach (int[] move in allowedEnemyMoves)
            {
                //Check if this will move enemy closer to a player
                if (Math.Abs(enemyUnit.CurrentX - closestPlayer.CurrentX) > Math.Abs(move[0] - closestPlayer.CurrentX) || Math.Abs(enemyUnit.CurrentY - closestPlayer.CurrentY) > Math.Abs(move[1] - closestPlayer.CurrentY))
                {
                    if (BoardManager.Units[move[0], move[1]] == null)
                    {
                        BoardManager.Units[enemyUnit.CurrentX, enemyUnit.CurrentY] = null;
                        enemyUnit.transform.position = BoardManager.Instance.GetTileCenter(move[0], move[1], 0);
                        enemyUnit.SetPosition(move[0], move[1]);
                        BoardManager.Units[move[0], move[1]] = enemyUnit;
                        enemyUnit.timeStampMove = Time.time + enemyUnit.cooldownMoveSeconds;
                        //enemyUnit.timeStampAttack += 2;
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
    }*/
}

