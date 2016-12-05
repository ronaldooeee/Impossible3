using UnityEngine;
using System.Collections;

public class Direction : MonoBehaviour {

	public enum direction { NORTH, SOUTH, WEST, EAST };

	public Coord toCoord(direction point) {
		if (point == direction.NORTH) {
			return new Coord(0, 1);
		}
		if (point == direction.SOUTH) {
			return new Coord(0, -1);
		}
		if (point == direction.WEST) {
			return new Coord(-1, 0);
		}
		if (point == direction.EAST) {
			return new Coord(1, 0);
		}
		return new Coord(0, 0);
	}
}
