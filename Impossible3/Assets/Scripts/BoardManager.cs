using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

	public static BoardManager Instance { set; get; }
	private bool[,] allowedMoves{ set; get; }
	private bool[,] allowedAttacks{ set; get; }
	private bool[,] allowedAbilities{ set; get; }

	public Unit[,] Units{ set; get; }
	private Unit selectedUnit;
	private Unit selectedTarget;
	private Component selectedTargetsHealth;

	public OpenMapSpots[,] Spots{ set; get; }

	private const float TILE_SIZE = 1.0f;
	private const float TILE_OFFSET = 0.5f;

	private int selectionX = -1;
	private int selectionY = -1;

	public List<GameObject> unitPrefabs;
    public List<GameObject> mapTiles;
    public static List<GameObject> playerUnits;
    public static List<GameObject> enemyUnits;

    private Material previousMat;
	public Material selectedMat;

	public bool isCooldownOff = true;

	public static int mapSize = 30;

	private void Start ()
	{
        playerUnits = new List<GameObject>();
        enemyUnits = new List<GameObject>();
        mapTiles = new List<GameObject>();
        Instance = this;
		//Spawn Black Background
		GameObject black = Instantiate(unitPrefabs[3], new Vector3(-32, -35, 30), Quaternion.identity) as GameObject;
		black.transform.Rotate(new Vector3(-50, -45, -9));
		black.transform.localScale = new Vector3(65, 0.1f, 30);
		black.GetComponent<Renderer>().material.color = Color.black;
		black.GetComponent<Renderer>().material.SetFloat("_Metallic", 1);

		SpawnAllUnits ();
		SpawnMapTiles();
		ColorMapTiles();
		SpawnWalls();
	}

    // Update world to show changes
    private void Update()
    {
        UpdateSelection();
        DrawBoard();

        //Measure when right mouse button is clicked
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
                    if(allowedMoves[selectionX, selectionY])
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

        //Measure when left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            if (selectionX >= 0 && selectionY >= 0)
            {
                //Clear existing movement higlights
                
                SelectUnit(selectionX, selectionY);
                BoardHighlights.Instance.Hidehighlights();
                //If you click on a player bring up the attack UI
                if (selectedTarget == null)
                {
                    SelectTarget(selectionX, selectionY);
                }
                else if (selectedTarget != null)
                {
                    if (allowedAttacks[selectionX, selectionY])
                    {
                        AttackTarget(selectionX, selectionY);
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
		if (Input.GetKeyDown("space"))
		{
			if (selectionX >= 0 && selectionY >= 0)
			{
				//Clear existing movement higlights

				SelectUnit(selectionX, selectionY);
				BoardHighlights.Instance.Hidehighlights();
				//If you click on a player bring up the attack UI
				if (selectedTarget == null)
				{
					SelectTarget(selectionX, selectionY);
				}
				else if (selectedTarget != null)
				{
					if (allowedAttacks[selectionX, selectionY])
					{
						AttackTarget(selectionX, selectionY);
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
    }

    private void UpdateSelection()
	{
		if (!Camera.main)
			return;
		// Measures where mouse is hitting from Camera Perspective, puts it in the out parameter to use later, only extends 25 units,
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
		if (!Units [x, y].isPlayer && isCooldownOff)
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

	private void SelectTarget(int x, int y)
	{
		allowedAttacks = Units [x, y].PossibleAttack ();
		selectedTarget = Units [x, y];
		BoardHighlights.Instance.HighlightAllowedAttacks (allowedAttacks);
	}

	private void AttackTarget(int x, int y)
	{
		selectedTarget = Units[x,y];
		if (selectedTarget != null && selectedUnit.timeStampAttack <= Time.time && selectedTarget.isPlayer != selectedUnit.isPlayer)
        {
            GameObject enemy = selectedTarget.gameObject;
			HealthSystem health = (HealthSystem) enemy.GetComponent (typeof(HealthSystem));
			health.TakeDamage (50);
            selectedUnit.timeStampAttack = Time.time + selectedUnit.cooldownAttackSeconds;
        } else if (selectedUnit.timeStampMove > Time.time) {
			return;
		}
		BoardHighlights.Instance.Hidehighlights();
		selectedTarget = null;
		selectedUnit = null;
	}

	//Spawns whatever unit is in the index of prefabs on BoardManager.cs
	private void SpawnUnit(int unit, int index, int x, int y)
	{
		GameObject go = Instantiate (unitPrefabs [unit+4], GetTileCenter(x,y,0), Quaternion.identity) as GameObject;
        Sprite[] spriteArray = new Sprite[] {
            Resources.Load<Sprite>("knight"), Resources.Load<Sprite>("knight2"), Resources.Load<Sprite>("knight3"),
            Resources.Load<Sprite>("golem"), Resources.Load<Sprite>("skeleton_knight") };
        RuntimeAnimatorController[] animationArray = {null, null, null, null, Resources.Load<RuntimeAnimatorController>("skeleton_knight") };
        go.GetComponent<SpriteRenderer>().sprite = spriteArray[index];
        if (animationArray[index] != null) { go.GetComponent<Animator>().runtimeAnimatorController = animationArray[index]; }
        go.transform.rotation = Camera.main.transform.rotation;
        go.transform.localScale = new Vector3(2, 2, 1);
        Units [x, y] = go.GetComponent<Unit> ();
		Units [x, y].SetPosition (x, y);
        if (unit == 0){
            playerUnits.Add(go);
        }
        else{enemyUnits.Add(go); }
    }

	private void SpawnWalls()
	{
		GameObject wall1 = Instantiate(unitPrefabs[3], new Vector3 (0, (float)mapSize/2 , (float)mapSize/2), Quaternion.identity) as GameObject;
		wall1.transform.Rotate(new Vector3(90f, 90f, 0));
		wall1.transform.localScale = new Vector3(mapSize, 0.0001f, (float)mapSize);
		GameObject wall2 = Instantiate(unitPrefabs[3], new Vector3((float)mapSize/2, (float)mapSize/2, mapSize), Quaternion.identity) as GameObject;
		wall2.transform.Rotate(new Vector3(90f, 0, 180f));
		wall2.transform.localScale = new Vector3(mapSize, 0.0001f, (float)mapSize);
		Texture2D walltex = Resources.Load("wall") as Texture2D;
		wall1.GetComponent<Renderer>().material.mainTexture = walltex;
		wall2.GetComponent<Renderer>().material.mainTexture = walltex;
	}

	private void SpawnAllUnits()
	{

		Units = new Unit[mapSize, mapSize];

		//Spawn Player Units (0 = Player,Sprite number, x value, y value)
		SpawnUnit (0, 0, 2, 0);
		SpawnUnit (0, 1, 4, 0);
        SpawnUnit (0, 2, 6, 0);

        //Spawn Enemy Units (1 = Enemy,Sprite number, x value, y value)
        SpawnUnit (1, 3, 1, 7);
        SpawnUnit (1, 4, 2, 9);
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