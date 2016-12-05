using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EnemyUnit : Unit
{
    Unit unitInstance;
    List<GameObject> playerUnits;
    PlayerUnit enemyUnit;

    public float timeStampDelay;

    private void Start()
    {
        timeStampDelay = Time.time;
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
        return;
		int damage = this.GetComponent<PlayerUnit> ().damageAmount;

        //If Attack cooldown over, then attack
        if (unitInstance.timeStampAttack <= Time.time && timeStampDelay <= Time.time)
        {
			HashSet<Coord>[] allowedEnemyAttacks = unitInstance.PossibleAttack(CurrentX, CurrentY);
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
					Coord playerCoord = new Coord(playerX, playerY);
					for(int i = 0; i < allowedEnemyAttacks.Length; i ++)
						if (allowedEnemyAttacks[i].Contains(playerCoord))
	                    {
	                        //If yes then attack
	                        HealthSystem health = (HealthSystem)BoardManager.Units[playerX, playerY].GetComponent(typeof(HealthSystem));
	                        if (health.takeDamageAndDie(damage))
	                        {
								
	                            // Remove player from list.
	                            foreach (GameObject spawn in playerUnits)
	                            {
	                                if (System.Object.ReferenceEquals(spawn, BoardManager.Units[playerX, playerY].gameObject))
	                                {
	                                    playerUnits.Remove(spawn);
	                                    Destroy(spawn);
	                                    BoardHighlights.Instance.Hidehighlights();
	                                }
	                            }
	                        }
	                        unitInstance.timeStampAttack = Time.time + unitInstance.cooldownAttackSeconds;
	                        return;
	                    }
                }
            }
        }


        //If Movemvent cooldown over, then Move
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
                            if (BoardManager.Units[move[0], move[1]] == null)
                            {
                                BoardManager.Units[CurrentX, CurrentY] = null;
                                this.transform.position = BoardManager.Instance.GetTileCenter(move[0], move[1], 0);
                                this.SetPosition(move[0], move[1]);
                                BoardManager.Units[move[0], move[1]] = enemyUnit;
                                unitInstance.timeStampMove = Time.time + unitInstance.cooldownMoveSeconds;
                                timeStampDelay = Time.time + 1;
                                return;
                            }
                        }
                    }
                }
            }
        }
    }

    

    private List<int[]> getTrueMoves()
    {
		HashSet<Coord>[] allowedEnemyMoves = unitInstance.PossibleMove(CurrentX, CurrentY);
        List<int[]> trueMoves = new List<int[]> { };
		for (int x = 0; x < allowedEnemyMoves.Length; x++)
        {
            for (int y = 0; y < allowedEnemyMoves.Length; y++)
            {
				Coord enemyMove = new Coord(x, y);
				for(int i = 0; i < allowedEnemyMoves.Length; i++)
					if (allowedEnemyMoves[i].Contains(enemyMove))
	                {
	                    trueMoves.Add(new int[] {x,y});
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
            int k = (int)UnityEngine.Random.Range(0, n+1);
            int[] value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

