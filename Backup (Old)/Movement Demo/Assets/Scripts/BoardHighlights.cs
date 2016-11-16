using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardHighlights : MonoBehaviour 
{
	//So this code can be accesssed in other programs
	public static BoardHighlights Instance{ set; get; }

	public GameObject highlightPrefab;
	private List<GameObject> highlights;

	//Instantiate highlights
	private void Start()
	{
		Instance = this;
		highlights = new List<GameObject> ();
	}

	//Check if highlights are on or off
	private GameObject GetHighlightObject() 
	{
		//Find and return the first object that matches !g.activeSelf
		GameObject go = highlights.Find (g => !g.activeSelf);

		//If none are found, instantiate one
		if (go == null) 
		{
			go = Instantiate (highlightPrefab);
			highlights.Add (go);
		}

		//Either way, return it
		return go;
	}

	public void HighlightAllowedMoves(bool[,] moves)
	{
		for (int i = 0; i < 8; i++) 
		{
			for (int j = 0; j < 8; j++) 
			{
				if (moves [i, j]) 
				{
					GameObject go = GetHighlightObject ();
					go.SetActive (true);
					go.transform.position = new Vector3 (i+0.5f, -0.45f, j+0.5f);
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
