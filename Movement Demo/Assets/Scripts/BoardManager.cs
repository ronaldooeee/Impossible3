using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

	public Unit[,] Units{ set; get; }
	private Unit selectedUnit;

	private const float TILE_SIZE = 1.0f;
	private const float TILE_OFFSET = 0.5f;

	private int selectionX = -1;
	private int selectionY = -1;

	public List<GameObject> unitPrefabs;
	private List<GameObject> activeUnit = new List<GameObject>();

	private void Start ()
	{
		SpawnAllUnits ();
	}

	// Update world to show changes
	private void Update()
	{
		UpdateSelection ();
		DrawBoard ();
	}

	private void UpdateSelection()
	{
		if (!Camera.main)
			return;
		// Measures where mouse is hitting from Camera Perspective, puts it in the out parameter to use later, only extends 25 units,
		// and will only hit things on the "BoardPlane" Layer
		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 25.0f, LayerMask.GetMask ("BoardPlane"))) 
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

	//Spawns whatever unit is in the index of prefabs on BoardManager.cs
	private void SpawnUnit(int index, int x, int y)
	{
		GameObject go = Instantiate (unitPrefabs [index], GetTileCenter(x,y), Quaternion.identity) as GameObject;
		go.transform.SetParent (transform);
		Units [x, y] = go.GetComponent<Unit> ();
		activeUnit.Add (go);
	}

	private void SpawnAllUnits()
	{
		activeUnit = new List<GameObject> ();
		Units = new Unit[8, 8];

		//Spawn Player Units
		SpawnUnit (0, 0, 0);

		//Spawn Enemy Units
		SpawnUnit (1,0,7);
	}

	//Draws 8x8 grid to show selection
	private void DrawBoard()
	{
		Vector3 widthLine = Vector3.right * 8;
		Vector3 heightLine = Vector3.forward * 8;
		
		for (int i = 0; i <= 8; i++) 
		{
			Vector3 start = Vector3.forward * i;
			Debug.DrawLine (start, start + widthLine);
			for (int j = 0; j <= 8; j++) 
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
	private Vector3 GetTileCenter(int x, int z)
	{
		Vector3 origin = Vector3.zero;
		origin.x += (TILE_SIZE * x) + TILE_OFFSET;
		origin.z += (TILE_SIZE * z) + TILE_OFFSET;
		return origin;
	}
}
