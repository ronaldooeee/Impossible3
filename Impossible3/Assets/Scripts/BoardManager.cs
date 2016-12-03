﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

    public Transform pauseMenu, pSettingsMenu, pExitMenu, pAudioMenu, pGraphicMenu;

    public static BoardManager Instance { set; get; }
	private bool[,] allowedMoves{ set; get; }
	private bool[,] allowedAttacks{ set; get; }
	private bool[,] allowedAbilities{ set; get; }

	public static Unit[,] Units{ set; get; }
	private Unit selectedUnit;
	private Unit selectedTarget;
	private Component selectedTargetsHealth;

	public OpenMapSpots[,] Spots{ set; get; }

	private const float TILE_SIZE = 1.0f;
	private const float TILE_OFFSET = 0.5f;

	public int selectionX = -1;
	public int selectionY = -1;

    public static int score;

	public List<GameObject> unitPrefabs;
    public List<GameObject> mapTiles;
    public static List<GameObject> playerUnits;
    public static List<GameObject> enemyUnits;
    private int quota = 2; // Amount of enemy units to keep on board at a time.
    private System.Random random = new System.Random();  // For generating random numbers.

    private Material previousMat;
	public Material selectedMat;

	public bool isCooldownOff = true;

	public static int mapSize = 30;

	private void Start ()
	{
        score = 0;
        playerUnits = new List<GameObject>();
        enemyUnits = new List<GameObject>();
        mapTiles = new List<GameObject>();
        Instance = this;

		SpawnAllUnits();
		SpawnMapTiles();
		ColorMapTiles();
		SpawnWalls();
	}

    // Update world to show changes
    private void Update()
    {
        while (enemyUnits.Count < quota)
        {
            SpawnUnit(1, random.Next(3, 5), random.Next(6, 10), random.Next(6, 10), 2, 2, 1);
        }
        if (playerUnits.Count < 1)
        {
            UnityEditor.EditorUtility.DisplayDialog("Failure!", "You have lost the game...", "Okay");
            UnityEditor.EditorApplication.ExecuteMenuItem("Edit/Play");
            Application.Quit();
        }
        UpdateSelection();
        DrawBoard();

        //Measure when right mouse button is clicked for Movement
        if (Input.GetMouseButtonDown(1))
        {
            //Make sure x and y value is on the board
            if (selectionX >= 0 && selectionY >= 0)
            {

                // If you click on a unit, select that unit
                if (selectedUnit == null)
                {
                    SelectUnit(selectionX, selectionY);

                    //Or if theres is a unit selected, move it to that space
                }
                else if (selectedUnit != null)
                {
                    if (allowedMoves[selectionX, selectionY])
                    {
                        MoveUnit(selectionX, selectionY);
                    }
                    else
                    {
                        BoardHighlights.Instance.Hidehighlights();
                        selectedTarget = null;
                        selectedUnit = null;
                    }
                }
            }
        }

        //Measure when left mouse button is clicked For Regular Attack
        if (Input.GetMouseButtonDown(0))
        {
            if (selectionX >= 0 && selectionY >= 0)
            {
                //Clear existing movement higlights

                SelectUnit(selectionX, selectionY);
                BoardHighlights.Instance.Hidehighlights();
                //If you click on a player bring up the attack UI
                if (selectedTarget == null && Units[selectionX, selectionY] && Units[selectionX, selectionY].isPlayer)
                {
                    SelectTarget(selectionX, selectionY);
                }
                else if (selectedTarget != null)
                {
                    //int damage = Units[selectionX, selectionY].damageAmount;

                    if (allowedAttacks[selectionX, selectionY])
                    {
                        Abilities abilityScript = null;
                        abilityScript = selectedUnit.GetComponent<MageAbilities>();
                        if (abilityScript == null) { abilityScript = selectedUnit.GetComponent<WarriorAbilities>(); }
                        if (abilityScript == null) { abilityScript = selectedUnit.GetComponent<RangerAbilities>(); }
                        //AttackTarget(selectionX, selectionY, damage, 1.0f);
                        abilityScript.RegAttack(selectedTarget);
                    }
                    else
                    {
                        BoardHighlights.Instance.Hidehighlights();
                        selectedTarget = null;
                        selectedUnit = null;
                    }
                }
            }
        }

        //Abilties
        if (selectedTarget != null) { 
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedUnit.GetComponent<Abilities>().Ability1(selectedUnit);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                selectedUnit.GetComponent<Abilities>().Ability2(selectedUnit);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                selectedUnit.GetComponent<Abilities>().Ability3(selectedUnit);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                selectedUnit.GetComponent<Abilities>().Ability4(selectedUnit);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                selectedUnit.GetComponent<Abilities>().Ability5(selectedUnit);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                selectedUnit.GetComponent<Abilities>().Ability6(selectedUnit);
            }
        }

        //In Game Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.gameObject.SetActive(false);
            }

        }
    }

    private void UpdateSelection()
	{
		if (!Camera.main)
			return;
		// Measures where mouse is hitting from Camera Perspective, puts it in the out parameter to use later, only extends 50 units,
		// and will only hit things on the "BoardPlane" Layer
		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 50.0f, LayerMask.GetMask ("BoardPlane"))) 
		{
			selectionX = (int)hit.point.x;
			selectionY = (int)hit.point.z;
		} 
		else 
		{
			selectionX = -1;
			selectionY = -1;
		}
	}

	private void SelectUnit(int x, int y)
	{
		// If no unit is selected when clicked, return
		if (Units [x, y] == null)
			return;

		// If unit that is clicked still has cooldown, or unit clicked is an enemy, return
		if (!Units [x, y].isPlayer)
			return;

		//What are you, and where do you want to go?
		allowedMoves = Units[x,y].PossibleMove ();
		//Make Sure unit is selected
		selectedUnit = Units [x, y];
		BoardHighlights.Instance.HighlightAllowedMoves (allowedMoves);
	}

	private void MoveUnit(int x, int y)
	{
		//If you movement to selected space is allowed, do this
		if (allowedMoves [x, y] && selectedTarget == null && selectedUnit.timeStampMove <= Time.time) {
			//Deselect any other unit that might be selected by accident
			Units [selectedUnit.CurrentX, selectedUnit.CurrentY] = null;
			//Find the coordinates for destination
			selectedUnit.transform.position = GetTileCenter (x, y, 0);
			//Move it there
			selectedUnit.SetPosition (x, y);
			//Set that unit's coordinates to desinations coordinates
			Units [x, y] = selectedUnit;           
            selectedUnit.timeStampMove = Time.time + selectedUnit.cooldownMoveSeconds;
        } else if (selectedUnit.timeStampMove > Time.time) {
			return;
		}
		//Deselect unit and get rid of highlight
		BoardHighlights.Instance.Hidehighlights();
		selectedUnit = null;
        selectedTarget = null;
	}

	public void SelectTarget(int x, int y)
	{
		allowedAttacks = Units [x, y].PossibleAttack ();
		selectedTarget = Units [x, y];
		BoardHighlights.Instance.HighlightAllowedAttacks (allowedAttacks);
	}

	public void AttackTarget(int x, int y, int damage, float cooldownAttackSeconds)
	{
		selectedTarget = Units[x,y];
		if (selectedTarget != null && selectedUnit.timeStampAttack <= Time.time && selectedTarget.isPlayer != selectedUnit.isPlayer)
        {
            GameObject enemy = selectedTarget.gameObject;
			HealthSystem health = (HealthSystem) enemy.GetComponent (typeof(HealthSystem));
			if (health.takeDamageAndDie(damage))
            {
                // Remove enemy from list.
                foreach (GameObject spawn in enemyUnits)
                {
                    if (Object.ReferenceEquals(spawn, selectedTarget.gameObject))
                    {
                        enemyUnits.Remove(spawn);
                        Destroy(spawn);
                        score += 1;
                        break;
                    }
                }
            }
            selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;
        } else  {
			return;
		}
		BoardHighlights.Instance.Hidehighlights();

        selectedTarget = null;
        selectedUnit = null;
    }

    public void BuffTarget(int x, int y, int buff, float cooldownAttackSeconds)
    {
        selectedTarget = Units[x, y];
        if (selectedTarget != null && selectedUnit.timeStampAttack <= Time.time && selectedTarget.isPlayer == selectedUnit.isPlayer)
        {
            GameObject ally = selectedTarget.gameObject;
            HealthSystem health = (HealthSystem)ally.GetComponent(typeof(HealthSystem));
            health = health + buff;
            selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;
        }
        else
        {
            return;
        }
        BoardHighlights.Instance.Hidehighlights();

        selectedTarget = null;
        selectedUnit = null;
    }

    //Spawns whatever unit is in the index of prefabs on BoardManager.cs
    private void SpawnUnit(int unit, int index, int x, int y, int straightAttack, int diagAttack, int circAttack)
	{
		GameObject go = Instantiate (unitPrefabs [unit], GetTileCenter(x,y,0), Quaternion.identity) as GameObject;
        Sprite[] spriteArray = new Sprite[] {
            Resources.Load<Sprite>("knight"), Resources.Load<Sprite>("mage1"), Resources.Load<Sprite>("archer1"),
            Resources.Load<Sprite>("golem"), Resources.Load<Sprite>("skeleton_knight") };
        RuntimeAnimatorController[] animationArray = {null, null, null, null, Resources.Load<RuntimeAnimatorController>("skeleton_knight") };
        go.GetComponent<SpriteRenderer>().sprite = spriteArray[index];
        if (animationArray[index] != null) { go.GetComponent<Animator>().runtimeAnimatorController = animationArray[index]; }
        go.transform.rotation = Camera.main.transform.rotation;
        go.transform.localScale = new Vector3(2, 2, 1);
        Units [x, y] = go.GetComponent<Unit> ();
		Units [x, y].SetPosition (x, y);
		if (unit == 0 || unit == 4 || unit == 5){
            playerUnits.Add(go);
        } else {
			enemyUnits.Add(go); 
		}
		setUnitAttributes(go, straightAttack, diagAttack, circAttack);
    }

	public static void setUnitAttributes(GameObject go, int straightAttack, int diagAttack, int circAttack)
    {
        PlayerUnit unit = go.GetComponent<PlayerUnit>();
        unit.straightMoveRange = 2;
        unit.diagMoveRange = 1;
        unit.circMoveRange = 1;

		unit.straightAttackRange = straightAttack;
		unit.diagAttackRange = diagAttack;
		unit.circAttackRange = circAttack;
	}

    private void SpawnWalls()
	{
		GameObject wall1 = Instantiate(unitPrefabs[3], new Vector3 (0-5, (float)mapSize/2-5 , (float)mapSize/2+5), Quaternion.identity) as GameObject;
		wall1.transform.Rotate(new Vector3(90f, 90f, 0));
		wall1.transform.localScale = new Vector3(mapSize, 0.0001f, (float)mapSize);
		GameObject wall2 = Instantiate(unitPrefabs[3], new Vector3((float)mapSize/2-5, (float)mapSize/2-5, mapSize+5), Quaternion.identity) as GameObject;
		wall2.transform.Rotate(new Vector3(90f, 0, 180f));
		wall2.transform.localScale = new Vector3(mapSize, 0.0001f, (float)mapSize);
		Texture2D walltex = Resources.Load("wall") as Texture2D;
		wall1.GetComponent<Renderer>().material.mainTexture = walltex;
		wall2.GetComponent<Renderer>().material.mainTexture = walltex;
	}

	private void SpawnAllUnits()
	{
		Units = new Unit[mapSize, mapSize];
		//Spawn Player Units (PrefabList #, Sprite number, x location, y location, straightAttack, diagAttack, circAttack)
		//Knight Stats
		SpawnUnit (0, 0, 2, 0, 1, 1, 1);
		//Mage Stats
		SpawnUnit (4, 1, 4, 0, 0, 0, 5);
		//Archer Stats
        SpawnUnit (5, 2, 6, 0, 0, 0, 3);

        //Spawn Enemy Units (1 = Enemy Prefab,Sprite number, x value, y value)
        SpawnUnit (1, 3, random.Next(6, 10), random.Next(6, 10), 2, 2, 1);
        SpawnUnit (1, 4, random.Next(6, 10), random.Next(6, 10), 2, 2 ,1);
    }

	private void SpawnEnvironment(int index, int x, int y)
	{
		GameObject tile = Instantiate (unitPrefabs [index], GetTileCenter(x,y,-0.01f), Quaternion.identity) as GameObject;
		tile.transform.SetParent (transform);
		tile.transform.Rotate(new Vector3(90f,0,0));
		mapTiles.Add (tile);
	}

	private void SpawnMapTiles()
	{
		Spots = new OpenMapSpots[mapSize, mapSize];
		//Spawn Tiles
		for (int i = 0; i < mapSize; i++)
		{
			for (int j = 0; j < mapSize; j++)
			{
				SpawnEnvironment(2, i, j);
			}

		}
	}

	private void ColorMapTiles()
	{
		Texture2D tile1 = Resources.Load("tile1") as Texture2D;
		Texture2D tile2 = Resources.Load("tile2") as Texture2D;
		Texture2D tile3 = Resources.Load("tile3") as Texture2D;
		foreach (GameObject tile in mapTiles)
		{
            tile.transform.Rotate(new Vector3(0,0, (Random.Range(0, 3) * 90)));
			int rand = Random.Range(0, 10);
			if (rand < 9 && rand >2)
			{
				tile.GetComponent<Renderer>().material.mainTexture = tile2;
			}else if(rand <= 2)
			{
				tile.GetComponent<Renderer>().material.mainTexture = tile1;
			}else
			{
				tile.GetComponent<Renderer>().material.mainTexture = tile3;
			}
			tile.GetComponent<Renderer>().material.color= Color.white;

		}
	}
	//Draws X.Y grid to show selection
	private void DrawBoard()
	{
		Vector3 widthLine = Vector3.right * mapSize;
		Vector3 heightLine = Vector3.forward * mapSize;

		for (int i = 0; i <= mapSize; i++) 
		{
			Vector3 start = Vector3.forward * i;
			Debug.DrawLine (start, start + widthLine);
			for (int j = 0; j <= mapSize; j++) 
			{
				start = Vector3.right * j;
				Debug.DrawLine (start, start + heightLine);
			}
		}

		// Draw the selection box
		if (selectionX >= 0 && selectionY >= 0) 
		{
			Debug.DrawLine (
				Vector3.forward * selectionY + Vector3.right * selectionX,
				Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1)
			);

			Debug.DrawLine (
				Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
				Vector3.forward * selectionY + Vector3.right * (selectionX + 1)
			);
		}
	}

	//Helper function to get center of tiles for unit placement
	public Vector3 GetTileCenter(int x, int z, float y = 0)
	{
		Vector3 origin = Vector3.zero;
		origin.x += (TILE_SIZE * x) + TILE_OFFSET;
		origin.z += (TILE_SIZE * z) + TILE_OFFSET;
		origin.y = y;
		return origin;
	}
}