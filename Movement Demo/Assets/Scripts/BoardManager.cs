﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

	public static BoardManager Instance { set; get; }
	private bool[,] allowedMoves{ set; get; }

	public Unit[,] Units{ set; get; }
	private Unit selectedUnit;

	private const float TILE_SIZE = 1.0f;
	private const float TILE_OFFSET = 0.5f;

	private int selectionX = -1;
	private int selectionY = -1;

	public List<GameObject> unitPrefabs;
	private List<GameObject> activeUnit = new List<GameObject>();

	private Material previousMat;
	public Material selectedMat;

	public bool isCooldownOff = true;

	private void Start ()
	{
		Instance = this;
		SpawnAllUnits ();
	}

	// Update world to show changes
	private void Update()
	{
		UpdateSelection ();
		DrawBoard ();

		//Measure when mouse button is clicked
		if (Input.GetMouseButtonDown (0))
		{
			//Make sure x and y value is on the board
			if (selectionX >= 0 && selectionY >= 0)
			{
				//If your clicking on a character while selectedUnit has nothing in it
				// select that unit
				if (selectedUnit == null) 
				{
					//Select Unit
					SelectUnit (selectionX, selectionY);
				}
				//Or if theres is a unit selected, move it to that space
				else
				{
					//Move Unit
					MoveUnit (selectionX,selectionY);
				}
			}
		}
	}

	private void SelectUnit(int x, int y)
	{
		// If no unit is selected when clicked, return
		if (Units [x, y] == null)
			return;

		// If unit that is clicked still has cooldown, or unit clicked is an enemy, return
		if (Units [x, y].isPlayer != isCooldownOff)
			return;

		//What are you, and where do you want to go?
		allowedMoves = Units[x,y].PossibleMove ();
		//Make Sure unit is selected
		selectedUnit = Units [x, y];
		//Take the old material on the character model, exchange it with a highlighting material when selected
		previousMat = selectedUnit.GetComponent<MeshRenderer> ().material;
		selectedMat.mainTexture = previousMat.mainTexture;
		selectedUnit.GetComponent<MeshRenderer> ().material = selectedMat;

		BoardHighlights.Instance.HighlightAllowedMoves (allowedMoves);
	}

	private void MoveUnit(int x, int y)
	{
		//If you movement to selected space is allowed, do this
		if (allowedMoves[x,y])
		{
			//Deselect any other unit that might be selected by accident
			Units [selectedUnit.CurrentX, selectedUnit.CurrentY] = null;
			//Find the coordinates for destination
			selectedUnit.transform.position = GetTileCenter (x, y);
			//Move it there
			selectedUnit.SetPosition (x, y);
			//Set that unit's coordinates to desinations coordinates
			Units [x, y] = selectedUnit;

			//WHERE WE WOULD CALL RESET COOLDOWN TIMER FUNCTION
		}
		//Deselect unit and get rid of highlight
		selectedUnit.GetComponent<MeshRenderer>().material = previousMat;
		BoardHighlights.Instance.Hidehighlights();
		selectedUnit = null;
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
		Units [x, y].SetPosition (x, y);
		activeUnit.Add (go);
	}

	private void SpawnAllUnits()
	{
		activeUnit = new List<GameObject> ();
		Units = new Unit[8, 8];

		//Spawn Player Units (Model number, x value, y value)
		SpawnUnit (0, 4, 4);
		SpawnUnit (0, 1, 0);

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
