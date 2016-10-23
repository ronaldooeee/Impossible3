using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardHighlights : MonoBehaviour 
{
	//So this code can be accesssed in other programs
	public static BoardHighlights Instance{ set; get; }

	public GameObject mHighlightPrefab;
	public GameObject aHighlightPrefab;
	private List<GameObject> highlights;

	//Instantiate highlights
	private void Start()
	{
		Instance = this;
		highlights = new List<GameObject> ();
	}

	//Check if highlights are on or off
	private GameObject GetMovementHighlightObject() 
	{
		GameObject go = Instantiate (mHighlightPrefab);
		highlights.Add (go);

		return go;
	}

	public void HighlightAllowedMoves(bool[,] moves)
	{
		for (int i = 0; i < BoardManager.mapSize; i++) 
		{
			for (int j = 0; j < BoardManager.mapSize; j++) 
			{
				if (moves [i, j]) 
				{
					GameObject go = GetMovementHighlightObject ();
					go.SetActive (true);
					go.transform.position = new Vector3 (i+0.5f, 0, j+0.5f);
				}
			}
		}
	}

	private GameObject GetAttackHighlightObject() 
	{
		GameObject go = Instantiate (aHighlightPrefab);
		highlights.Add (go);

		return go;
	}

	public void HighlightAllowedAttacks(bool[,] moves)
	{
		for (int i = 0; i < BoardManager.mapSize; i++) 
		{
			for (int j = 0; j < BoardManager.mapSize; j++) 
			{
				if (moves [i, j]) 
				{
					GameObject go = GetAttackHighlightObject ();
					go.SetActive (true);
					go.transform.position = new Vector3 (i+0.5f, 0, j+0.5f);
				}
			}
		}
	}

	public void Hidehighlights()
	{
		foreach (GameObject go in highlights)
			go.SetActive (false);
	}
}
