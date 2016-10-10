using UnityEngine;
using System.Collections;

public class ColliderResizer : MonoBehaviour {

	public GameObject BoardPlane;
	private int newMapSize = BoardManager.mapSize / 2;

	// Use this for initialization
	void Start () {
		BoardPlane = GameObject.Find ("BoardPlane");
		transform.localScale = new Vector3 (BoardManager.mapSize, 0, BoardManager.mapSize);
		transform.localPosition = new Vector3 (newMapSize + 0.5f, 0, newMapSize + 0.5f);
	}
}
