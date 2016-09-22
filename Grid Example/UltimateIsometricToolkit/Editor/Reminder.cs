using UnityEngine;
using UnityEditor;
using System;

public class Reminder : EditorWindow {

	[MenuItem("Tools/UIT/Reminder")]
	public static void ShowWindow()
	{
		GetWindow<Reminder>("Reminder").ShowTab();
	}

	void OnGUI()
	{
		EditorGUILayout.TextArea("Thanks for purchasing my asset.\n  \n Please leave a comment and rating in the asset store. Thanks");
		if (GUILayout.Button(new GUIContent("Comment & Rate", "Opens browser")))
		{
			Help.BrowseURL("https://www.assetstore.unity3d.com/en/#!/content/33032");

			EditorPrefs.SetBool("reminded", true);
			EditorPrefs.SetString("lastReminded", DateTime.Now.ToString("hh.mm.ss"));
		}
	}
}
