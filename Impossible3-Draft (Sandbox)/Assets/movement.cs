//Inspired by http://answers.unity3d.com/questions/185226/c-movement-script-basic.html

using UnityEngine;
using System.Collections;

public class movement : CharacterClasses {

	public float speed = 6.0f;
	private Vector3 moveDirection = Vector3.zero;
	enum directions {left=1, right=2, up=3, down=4};
	directions direction;

	// Use this for initialization
	void Start () {

		direction = directions.left;
	
	}
	
	// Update is called once per frame
	void Update () {
		print (direction);

		if (direction == directions.left) {
		
			moveDirection = new Vector3 (-0.01f, 0, 0);

			transform.Translate (moveDirection);
			print (transform.position);

			if (transform.position.x > 4.0f) {
			
				direction = directions.right;

			}

		} else if (direction == directions.right) {

			moveDirection = new Vector3 (0, 0, -0.01f);

			transform.Translate (moveDirection);
			print (transform.position);

			if (transform.position.z < -4.0f) {

				direction = directions.up;

			}

		} else if (direction == directions.up) {

			moveDirection = new Vector3 (0.01f, 0, 0);

			transform.Translate (moveDirection);
			print (transform.position);

			if (transform.position.x > 4.0f) {

				direction = directions.left;

			}

		} else {

			//Pass.

		}
	}
}
