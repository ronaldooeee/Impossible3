using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { set; get; }
    private bool[,] allowedMoves { set; get; }
    private bool[,] allowedAttacks { set; get; }
    private bool[,] allowedAbilities { set; get; }

    public static Unit[,] Units { set; get; }
    public Unit selectedUnit;
    public Unit selectedTarget;
    private Component selectedTargetsHealth;

    public int selectedAbility = 0;

    public OpenMapSpots[,] Spots { set; get; }

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    public int selectionX = -1;
    public int selectionY = -1;

    public static int score;
    private GUIText abilityUnlock;

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

    public int unitAccuracy;
    public int targetDodgeChance;

    private void Start()
    {
        score = 40;
        playerUnits = new List<GameObject>();
        enemyUnits = new List<GameObject>();
        mapTiles = new List<GameObject>();
        Instance = this;

        SpawnAllUnits();
        SpawnMapTiles();
        ColorMapTiles();
        SpawnWalls();
        SpawnAllObstacles(20);

        this.useGUILayout = true;
    }

    // Update world to show changes
    private void Update()
    {
        while (enemyUnits.Count < quota)
        {
            int Spawndistance = 6;
            Coordinate[] bound = findBound();
            bound[0].x = bound[0].x - Spawndistance >= 0 ? bound[0].x - Spawndistance : bound[0].x;
            bound[0].y = bound[0].y - Spawndistance >= 0 ? bound[0].y - Spawndistance : bound[0].y;
            bound[1].x = bound[1].x + Spawndistance < mapSize ? bound[1].x + Spawndistance : bound[1].x;
            bound[1].y = bound[1].y + Spawndistance < mapSize ? bound[1].y + Spawndistance : bound[1].y;
            SpawnUnit(random.Next(6, 10), random.Next((bound[0].x), bound[1].x),  random.Next(bound[0].y, bound[1].y));
        }

        //Let player know of new abilities
        tellScore(score);

        if (playerUnits.Count < 1)
        {
            //UnityEditor.EditorUtility.DisplayDialog("Failure!", "You have lost the game...", "Okay");
            //UnityEditor.EditorApplication.ExecuteMenuItem("Edit/Play");
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
                if (Units[selectionX, selectionY] && Units[selectionX, selectionY].isPlayer)
                {
                    BoardHighlights.Instance.Hidehighlights();
                    selectedTarget = null;
                    selectedUnit = null;
                    SelectUnitForMove(selectionX, selectionY);
                }
                else if (selectedUnit != null)
                {
                    if (allowedMoves[selectionX, selectionY])
                    {
                        MoveUnit(selectionX, selectionY);
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
                //If you click on a player bring up the attack UI
                if (Units[selectionX, selectionY] && Units[selectionX, selectionY].isPlayer)
                {
                    BoardHighlights.Instance.Hidehighlights();
                    selectedTarget = null;
                    selectedUnit = null;
                    SelectUnitForAttack(selectionX, selectionY);
					selectedUnit.GetComponent<PlayerUnit>().ResetAttackRanges();
                    selectedAbility = 0;
                }
                else if (selectedUnit != null)
                {
                    //int damage = Units[selectionX, selectionY].damageAmount;

                    if (allowedAttacks[selectionX, selectionY])
                    {
						SelectTarget (selectionX, selectionY);
                        if (selectedAbility == 0)
                        {
                            selectedUnit.GetComponent<Abilities>().RegAttack(selectedUnit, selectedTarget);
                        }
                        else if (selectedAbility == 1)
                        {
                            if (Abilities.unlockedAbilities[0])
                            {
                                selectedUnit.GetComponent<Abilities>().Ability1(selectedUnit, selectedTarget);
                            }

                        }
                        else if (selectedAbility == 2)
                        {
                            if (Abilities.unlockedAbilities[1])
                            {
                                selectedUnit.GetComponent<Abilities>().Ability2(selectedUnit, selectedTarget);
                            }

                        }
                        else if (selectedAbility == 3)
                        {
                            if (Abilities.unlockedAbilities[2])
                            {
                                selectedUnit.GetComponent<Abilities>().Ability3(selectedUnit, selectedTarget);
                            }

                        }
                        else if (selectedAbility == 4)
                        {
                            if (Abilities.unlockedAbilities[3])
                            {
                                selectedUnit.GetComponent<Abilities>().Ability4(selectedUnit, selectedTarget);
                            }

                        }
                        else if (selectedAbility == 5)
                        {
                            if (Abilities.unlockedAbilities[4])
                            {
                                selectedUnit.GetComponent<Abilities>().Ability5(selectedUnit, selectedTarget);
                            }

                        }
                        else if (selectedAbility == 6)
                        {
                            if (Abilities.unlockedAbilities[5])
                            {
                                selectedUnit.GetComponent<Abilities>().Ability6(selectedUnit, selectedTarget);
                            }

                        }
                    }
                }
            }
        }

        //Abilties
        if (selectedUnit != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (Abilities.unlockedAbilities[0])
                {
                    selectedUnit.GetComponent<Abilities>().Ability1(selectedUnit, selectedTarget);
                    selectedAbility = 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (Abilities.unlockedAbilities[1])
                {
                    selectedUnit.GetComponent<Abilities>().Ability2(selectedUnit, selectedTarget);
                    selectedAbility = 2;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (Abilities.unlockedAbilities[2])
                {
                    selectedUnit.GetComponent<Abilities>().Ability3(selectedUnit, selectedTarget);
                    selectedAbility = 3;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (Abilities.unlockedAbilities[3])
                {
                    selectedUnit.GetComponent<Abilities>().Ability4(selectedUnit, selectedTarget);
                    selectedAbility = 4;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                if (Abilities.unlockedAbilities[4])
                {
                    selectedUnit.GetComponent<Abilities>().Ability5(selectedUnit, selectedTarget);
                    selectedAbility = 5;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                if (Abilities.unlockedAbilities[5])
                {
                    selectedUnit.GetComponent<Abilities>().Ability6(selectedUnit, selectedTarget);
                    selectedAbility = 6;
                }
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
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f, LayerMask.GetMask("BoardPlane")))
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

    public void SelectUnitForMove(int x, int y)
    {
        // If no unit is selected when clicked, return
        if (Units[x, y] == null)
            return;

        // If unit clicked is an enemy or obstacle, return
        if (!Units[x, y].isPlayer)
            return;

        //What are you, and where do you want to go?
        allowedMoves = Units[x, y].PossibleMove();
        //Make Sure unit is selected
        selectedUnit = Units[x, y];
        BoardHighlights.Instance.Hidehighlights();
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
    }

    public void MoveUnit(int x, int y)
    {
        //If you movement to selected space is allowed, do this
		if (allowedMoves[x, y] && Units[x,y] == null && selectedUnit.timeStampMove <= Time.time)
        {
            //Deselect any other unit that might be selected by accident
            Units[selectedUnit.CurrentX, selectedUnit.CurrentY] = null;
            //Find the coordinates for destination
            selectedUnit.transform.position = GetTileCenter(x, y, 0);
            //Move it there
            selectedUnit.SetPosition(x, y);
            //Set that unit's coordinates to desinations coordinates
            Units[x, y] = selectedUnit;
            selectedUnit.timeStampMove = Time.time + selectedUnit.cooldownMoveSeconds;
        }
        else if (selectedUnit.timeStampMove > Time.time)
        {
            return;
        }
        //Deselect unit and get rid of highlight
        BoardHighlights.Instance.Hidehighlights();
        selectedUnit = null;
        selectedTarget = null;
    }

    public void SelectUnitForAttack(int x, int y)
    {
		selectedUnit = Units[x, y];
        allowedAttacks = Units[x, y].PossibleAttack();
        BoardHighlights.Instance.Hidehighlights();
        BoardHighlights.Instance.HighlightAllowedAttacks(allowedAttacks);
    }

	public void SelectTarget(int x, int y)
	{
		selectedTarget = Units [x, y];
	}

	public void AttackTarget(Unit selectedTarget, int damage, float cooldownAttackSeconds)
    {
        unitAccuracy = selectedUnit.accuracy;
        //Debug.Log (unitAccuracy);
        //selectedTarget = Units[x, y];
        if (selectedTarget != null && selectedUnit.timeStampAttack <= Time.time && selectedTarget.isPlayer != selectedUnit.isPlayer)
        {
            if (unitAccuracy >= selectedTarget.dodgeChance + Random.Range(0, 100))
            {
                GameObject enemy = selectedTarget.gameObject;
                HealthSystem health = (HealthSystem)enemy.GetComponent(typeof(HealthSystem));
                health.takeDamageAndDie(damage);
                selectedUnit.timeStampAttack = Time.time + cooldownAttackSeconds;
            }
            else
            {
                HealthSystem health = (HealthSystem)selectedTarget.gameObject.GetComponent(typeof(HealthSystem));
                health.ConfirmHit(null);
                Debug.Log("Player Missed!");
            }
        }
        else if (selectedUnit.timeStampAttack > Time.time)
        {
            return;
        }
        BoardHighlights.Instance.Hidehighlights();
        selectedAbility = 0;
        selectedTarget = null;
        selectedUnit = null;
    }

	public void BuffTarget(Unit selectedTarget, int buff, float cooldownAttackSeconds)
    {
        //selectedTarget = Units[x, y];
        if (selectedTarget != null && selectedUnit.timeStampAttack <= Time.time && selectedTarget.isPlayer == selectedUnit.isPlayer)
        {
            GameObject ally = selectedTarget.gameObject;
            int health = ally.GetComponent<PlayerUnit>().health;
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
    private void SpawnUnit(int unit, int x, int y)
    {
        if (Units[x, y] == null) {
            int index = unit - 3 >= 0 ? unit - 3 : 0;
            GameObject go = Instantiate(unitPrefabs[unit], GetTileCenter(x, y, 0), Quaternion.identity) as GameObject;
            Sprite[] spriteArray = new Sprite[] {
            Resources.Load<Sprite>("knight"), Resources.Load<Sprite>("mage1"), Resources.Load<Sprite>("archer1"),
            Resources.Load<Sprite>("golem"), Resources.Load<Sprite>("Skeleton_Knight"), Resources.Load<Sprite>("Skeleton_Spear"),Resources.Load<Sprite>("Kaboocha"),Resources.Load<Sprite>("Skeleton_Archer") };
            RuntimeAnimatorController[] animationArray = { null, null, null, null, Resources.Load<RuntimeAnimatorController>("Skeleton_Knight"), Resources.Load<RuntimeAnimatorController>("Skeleton_Spear"), null, null };
            go.GetComponent<SpriteRenderer>().sprite = spriteArray[index];
            if (animationArray[index] != null) { go.GetComponent<Animator>().runtimeAnimatorController = animationArray[index]; }
            go.transform.rotation = Camera.main.transform.rotation;
            go.transform.localScale = new Vector3(2, 2, 1);
            Units[x, y] = go.GetComponent<Unit>();
            Units[x, y].SetPosition(x, y);
            //Debug.Log(x + " " + y);
            if (unit == 0 || unit == 4 || unit == 5)
            {
                playerUnits.Add(go);
            }
            else
            {
                enemyUnits.Add(go);
            }
        }
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
        GameObject wall1 = Instantiate(unitPrefabs[3], new Vector3(0 - 5, (float)mapSize / 2 - 5, (float)mapSize / 2 + 5), Quaternion.identity) as GameObject;
        wall1.transform.Rotate(new Vector3(90f, 90f, 0));
        wall1.transform.localScale = new Vector3(mapSize, 0.0001f, (float)mapSize);
        GameObject wall2 = Instantiate(unitPrefabs[3], new Vector3((float)mapSize / 2 - 5, (float)mapSize / 2 - 5, mapSize + 5), Quaternion.identity) as GameObject;
        wall2.transform.Rotate(new Vector3(90f, 0, 180f));
        wall2.transform.localScale = new Vector3(mapSize, 0.0001f, (float)mapSize);
        Texture2D walltex = Resources.Load("wall") as Texture2D;
        wall1.GetComponent<Renderer>().material.mainTexture = walltex;
        wall2.GetComponent<Renderer>().material.mainTexture = walltex;
    }

    private void SpawnAllObstacles(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnObstacle(random.Next(0, mapSize), random.Next(0, mapSize), "boulder");
        }
    }

    public void SpawnObstacle(int x, int y, string sprite)
    {
        GameObject cube = Instantiate(unitPrefabs[1], GetTileCenter(x, y, 0), Quaternion.identity) as GameObject;
        Vector3 temp = new Vector3(0, 0.5f, 0);
        cube.transform.position += temp;
        cube.transform.rotation = Camera.main.transform.rotation;
        cube.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(sprite);
        cube.transform.localScale = new Vector3(2, 2, 1);
        Units[x, y] = cube.GetComponent<Unit>();
        Units[x, y].SetPosition(x, y);

    }

    private void SpawnAllUnits()
    {
        Units = new Unit[mapSize, mapSize];
        //Spawn Player Units (PrefabList #,  x location, y location)
        //Knight Stats
        SpawnUnit(0, 2, 0);
        //Mage Stats
        SpawnUnit(4, 4, 0);
        //Archer Stats
        SpawnUnit(5, 6, 0);

        //Spawn Enemy Units (PrefabList #, x value, y value)
        /*
        SpawnUnit (7, 3, 3);
        SpawnUnit (7, 3, 4);
        SpawnUnit (7, 4, 4);
        SpawnUnit (7, 4, 3);
        SpawnUnit (7, 4, 2);
        SpawnUnit (7, 3, 2);
        SpawnUnit (7, 2, 2);
        SpawnUnit (7, 2, 3);
        SpawnUnit (7, 2, 4);
        */

    }

    private void SpawnEnvironment(int index, int x, int y)
    {
        GameObject tile = Instantiate(unitPrefabs[index], GetTileCenter(x, y, -0.01f), Quaternion.identity) as GameObject;
        tile.transform.SetParent(transform);
        tile.transform.Rotate(new Vector3(90f, 0, 0));
        mapTiles.Add(tile);
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
            tile.transform.Rotate(new Vector3(0, 0, (Random.Range(0, 3) * 90)));
            int rand = Random.Range(0, 10);
            if (rand < 9 && rand > 2)
            {
                tile.GetComponent<Renderer>().material.mainTexture = tile2;
            }
            else if (rand <= 2)
            {
                tile.GetComponent<Renderer>().material.mainTexture = tile1;
            }
            else
            {
                tile.GetComponent<Renderer>().material.mainTexture = tile3;
            }
            tile.GetComponent<Renderer>().material.color = Color.white;

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
            Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j <= mapSize; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heightLine);
            }
        }

        // Draw the selection box
        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1)
            );

            Debug.DrawLine(
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

    private Coordinate[] findBound()
    {
        int xMax = -1;
        int yMax = -1;
        int xMin = 100;
        int yMin = 100;
        foreach (GameObject player in playerUnits)
        {
            if (player.transform.position.x > xMax)
            {
                xMax = (int)player.transform.position.x;
            }

            if (player.transform.position.y > yMax)
            {
                yMax = (int)player.transform.position.y;
            }
            if (player.transform.position.x <xMin)
            {
                xMin = (int)player.transform.position.x;
            }

            if (player.transform.position.y < yMin)
            {
                yMin = (int)player.transform.position.y;
            }
        }
        return new Coordinate[] {new Coordinate(xMin, yMin), new Coordinate(xMax, yMax) };
    }

    private void tellScore(int score)
    {
        if (score >= 24 && !Abilities.displayed[5])
        {
            Abilities.unlockedAbilities[5] = true;
            StartCoroutine(ShowMessage("Unlocked ability 6!", 2));
            Abilities.displayed[5] = true;
        }
        else if (score >= 20 && !Abilities.displayed[4])
        {
            Abilities.unlockedAbilities[4] = true;
            StartCoroutine(ShowMessage("Unlocked ability 5!", 2));
            Abilities.displayed[4] = true;
        }
        else if (score >= 16 && !Abilities.displayed[3])
        {
            Abilities.unlockedAbilities[3] = true;
            StartCoroutine(ShowMessage("Unlocked ability 4!", 2));
            Abilities.displayed[3] = true;
        }
        else if (score >= 8 && !Abilities.displayed[2])
        {
            Abilities.unlockedAbilities[2] = true;
            StartCoroutine(ShowMessage("Unlocked ability 3!", 2));
            Abilities.displayed[2] = true;
        }
        else if (score >= 4 && !Abilities.displayed[1])
        {
            Abilities.unlockedAbilities[1] = true;
            StartCoroutine(ShowMessage("Unlocked ability 2!", 2));
            Abilities.displayed[1] = true;
        }
        else if (score >= 2 && !Abilities.displayed[0])
        {
            Abilities.unlockedAbilities[0] = true;
            StartCoroutine(ShowMessage("Unlocked ability 1!", 2));
            Abilities.displayed[0] = true;
        }
        else
        {
            // Pass.
        }
    }

    // Help from: http://answers.unity3d.com/questions/532086/how-to-make-text-pop-up-for-a-few-seconds.html
    IEnumerator ShowMessage(string message, float delay)
    {
        /*GUI.enabled = true;
        GUI.Label(new Rect(50, 50, 100, 25), message);
        //abilityUnlock.text = message;
        //abilityUnlock.enabled = true;
        yield return new WaitForSeconds(delay);
        GUI.enabled = false;
        //abilityUnlock.enabled = false;*/

        Debug.Log(message);
        yield return new WaitForSeconds(delay);
    }
}