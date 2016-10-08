using UnityEngine;
using System.Collections;

public class TestGUI : MonoBehaviour {
	private CharacterClasses class1 = new BaseMageClass ();
	private CharacterClasses class2 = new BaseWarriorClass ();
	private CharacterClasses class3 = new BaseRangerClass ();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI(){
		GUILayout.Label (class1.CharacterClassName);
		GUILayout.Label (class1.CharacterClassDescription);
		GUILayout.Label (class2.CharacterClassName);
		GUILayout.Label (class2.CharacterClassDescription);
		GUILayout.Label (class3.CharacterClassName);
		GUILayout.Label (class3.CharacterClassDescription);
	}
}
