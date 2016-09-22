//SpriteInstantiator.cs - Place inside and "Editor" folder.
//Roman Issa (Roland1234) - 2015/2/2

using System.Collections.Generic;
using System.Linq;

using UnityEditor;

using UnityEngine;

public class SpriteInstantiator : EditorWindow
{
    [MenuItem("Window/Sprite Instantiator")]
    public static void ShowWindow()
    {
        GetWindow<SpriteInstantiator>("Sprite Instantiator").ShowTab();
    }

    private List<Sprite> _sprites = new List<Sprite>();
    private Transform _parent;

    private void OnEnable()
    {
        UpdateSelection();
    }

    private void OnDisable()
    {
        _sprites.Clear();
        _parent = null;
    }

    private void OnSelectionChange()
    {
        UpdateSelection();
    }

    private void OnInspectorUpdate()
    {
        Repaint();
    }

    private void OnGUI()
    {
        var labelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 80.0f;
        
        var enabled = GUI.enabled;
        GUI.enabled = _sprites.Count > 0;
        if(GUILayout.Button(string.Format("Instantiate Sprites[{0}]", _sprites.Count), GUILayout.ExpandWidth(true)))
        {
            Selection.objects = _sprites.Select(s =>
            {
                var newSprite = new GameObject(s.name ?? "sprite", typeof(SpriteRenderer)).GetComponent<SpriteRenderer>();
                newSprite.sprite = s;
                newSprite.transform.position = GetInstantiatePosition();
                newSprite.transform.parent = _parent;
                return newSprite.gameObject;
            }).ToArray();
        }
        GUI.enabled = enabled;

        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("x", GUILayout.Width(20.0f)))
        {
            _parent = null;
        }
        _parent = (Transform)EditorGUILayout.ObjectField("Parent", _parent, typeof(Transform), true);
        EditorGUILayout.EndHorizontal();

        EditorGUIUtility.labelWidth = labelWidth;
    }

    private Vector3 GetInstantiatePosition()
    {
        if(SceneView.currentDrawingSceneView != null && SceneView.currentDrawingSceneView.camera != null)
        {
            var pos = SceneView.currentDrawingSceneView.camera.transform.position;
            pos.z = 0.0f;
            return pos;
        }

        return Vector3.zero;
    }

    private void UpdateSelection()
    {
        if(Selection.activeTransform != null)
        {
            _parent = Selection.activeTransform;
        }
        _sprites = Selection.objects.OfType<Sprite>().ToList();
    }
}
