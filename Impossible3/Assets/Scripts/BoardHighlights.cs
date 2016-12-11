using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardHighlights : MonoBehaviour 
{
	//So this code can be accesssed in other programs
	public static BoardHighlights Instance{ set; get; }

	public GameObject moveHighlightPrefab;
	public GameObject attackHighlightPrefab;
    public GameObject abilityHighlightPrefab;
    private List<GameObject> highlights;

	//Instantiate highlights
	private void Start()
	{
		Instance = this;
		highlights = new List<GameObject> ();
	}

    private void Update()
    {
        Unit selectedUnit = BoardManager.Instance.selectedUnit;
        if(selectedUnit != null)
        {
            if (selectedUnit.timeStampAttack <= Time.time) {
                Color highlight = attackHighlightPrefab.GetComponent<Renderer>().sharedMaterial.color;
                highlight.a = (float)0.8;
                attackHighlightPrefab.GetComponent<Renderer>().sharedMaterial.color = highlight;
            }
            else
            {
                Color highlight = attackHighlightPrefab.GetComponent<Renderer>().sharedMaterial.color;
                highlight.a = (float)0.5;
                attackHighlightPrefab.GetComponent<Renderer>().sharedMaterial.color = highlight;
            }

            if (selectedUnit.timeStampMove <= Time.time) {
                Color highlight =moveHighlightPrefab.GetComponent<Renderer>().sharedMaterial.color;
                highlight.a = (float)0.6;
                moveHighlightPrefab.GetComponent<Renderer>().sharedMaterial.color = highlight;
            } else
            {
                Color highlight = moveHighlightPrefab.GetComponent<Renderer>().sharedMaterial.color;
                highlight.a = (float)0.3;
                moveHighlightPrefab.GetComponent<Renderer>().sharedMaterial.color = highlight;
            }
        }
    }

	//Check if highlights are on or off
	private GameObject GetMovementHighlightObject() 
	{
		GameObject go = Instantiate (moveHighlightPrefab);
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
					go.transform.position = new Vector3 (i+0.5f, -0.004f, j+0.5f);
				}
			}
		}
	}

	private GameObject GetAttackHighlightObject() 
	{
		GameObject go = Instantiate (attackHighlightPrefab);
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
					go.transform.position = new Vector3 (i+0.5f, -0.004f, j+0.5f);
				}
			}
		}
	}

    private GameObject GetAbilityHighlightObject()
    {
        GameObject go = Instantiate(abilityHighlightPrefab);
        highlights.Add(go);
        return go;
    }

    public void HighlightAllowedAbilities(bool[,] moves)
    {
        for (int i = 0; i < BoardManager.mapSize; i++)
        {
            for (int j = 0; j < BoardManager.mapSize; j++)
            {
                if (moves[i, j])
                {
                    GameObject go = GetAbilityHighlightObject();
                    go.SetActive(true);
                    go.transform.position = new Vector3(i + 0.5f, -0.004f, j + 0.5f);
                }
            }
        }
    }

    public void Hidehighlights()
	{
        foreach (GameObject go in highlights)
            Destroy(go);
	}
}
