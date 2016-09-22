using UnityEngine;
using System.Collections;

public class b : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			transform.Translate (0, 0, 1);
		}
	}
}
