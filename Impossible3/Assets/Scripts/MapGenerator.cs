using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {

	public Transform tilePrefab;
	public Transform obstaclePrefab;
	public Vector2 mapSize;

	[Range(0,1)]
	public float outlinePercent;

	public List<Coord> allTileCoords;
	public Queue<Coord> shuffledTileCoords;
    public List<Vector3> obstaclePositions;

    public int seed;
    public System.Random randomNumber;
    public float timeLeft;

	/*void Update(){
        timeLeft -= Time.deltaTime;

        if (timeLeft < 0)
        {
            seed = randomNumber.Next(1, 100);
            //mapSize.x = randomNumber.Next(1, 20);
            //mapSize.y = randomNumber.Next(1, 20);
            DestroyImmediate(transform.FindChild("Generated Map").gameObject);
            GenerateMap();
            timeLeft = 2;
        }

	}*/

	void Start() {
        randomNumber = new System.Random();
        timeLeft = 2;
        seed = randomNumber.Next(1, 100);
        //mapSize.x = randomNumber.Next(1, 20);
        //mapSize.y = randomNumber.Next(1, 20);
        DestroyImmediate(transform.FindChild("Generated Map").gameObject);
        GenerateMap ();
	}

	public void GenerateMap() {

		allTileCoords = new List<Coord>();
		for (int x = 0; x < mapSize.x; x ++) {
			for (int y = 0; y < mapSize.y; y ++) {
				allTileCoords.Add(new Coord(x, y));
			}
		}
		shuffledTileCoords = new Queue<Coord> (Utility.ShuffleArray (allTileCoords.ToArray(), seed));

		string holderName = "Generated Map";
		if (transform.FindChild (holderName)) {
			DestroyImmediate(transform.FindChild(holderName).gameObject);
		}

		Transform mapHolder = new GameObject (holderName).transform;
		mapHolder.parent = transform;

		for (int x = 0; x < mapSize.x; x ++) {
			for (int y = 0; y < mapSize.y; y ++) {
				Vector3 tilePosition = CoordToPosition(x, y);
				Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right*90)) as Transform;
				newTile.localScale = Vector3.one * (1-outlinePercent);
				newTile.parent = mapHolder;
			}
		}

        //int obstacleCount = 10;
        int mapArea = (int)(mapSize.x * mapSize.y);
        int obstacleCount = (int) System.Math.Floor(mapArea * 0.06);

		for (int i = 0; i < obstacleCount; i++){
			Coord randomCoord = getRandCoord();
			Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
            obstaclePositions.Add(obstaclePosition);
			Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition + Vector3.up * .5f, Quaternion.identity) as Transform;
			newObstacle.parent = mapHolder;
		}

	}

	Vector3 CoordToPosition(int x, int y){
		return new Vector3(-mapSize.x/2 +0.5f + x, 0, -mapSize.y/2 + 0.5f + y);
	}

	public Coord getRandCoord(){
		Coord randomCoord = shuffledTileCoords.Dequeue();
		shuffledTileCoords.Enqueue (randomCoord);

		return randomCoord;
	}

	public struct Coord {
		public int x;
		public int y;

		public Coord(int _x, int _y){
			x = _x;
			y = _y;
		}
	}
}