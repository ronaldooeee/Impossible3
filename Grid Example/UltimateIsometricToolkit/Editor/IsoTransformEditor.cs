using System.Collections.Generic;
using System.Linq;
using Assets.UltimateIsometricToolkit.Scripts.Core;
using Assets.UltimateIsometricToolkit.Scripts.Utils;
using UltimateIsometricToolkit;
using UnityEditor;
using UnityEngine;

namespace Assets.UltimateIsometricToolkit.Editor {

	/// <summary>
	/// Custom editor for the IsoTransform component
	/// </summary>
	[CustomEditor(typeof(IsoTransform)), CanEditMultipleObjects]
	public class IsoTransformEditor : UnityEditor.Editor {
		private IsoTransform _instance;
		private PropertyField[] _instanceFields;
		private List<IsoTransform> _selectedObjects = new List<IsoTransform>();
		private Vector3 _isoHandlePos; 

		/// <summary>
		/// Updates the current selected IsoTransforms in scene gui and the center position for the custom handles
		/// </summary>
		private void UpdateHandlePos() {
			//filter selected isoObjects and select their IsoTransform component & position ind Dic<t,v>
			_selectedObjects = targets.Where(t => t as IsoTransform).Select(t => t as IsoTransform).ToList();

			//calcs the median position of all selected IsoTransform's positions
			_isoHandlePos = _selectedObjects.Aggregate(Vector3.zero,(runningSum, t) => runningSum + t.Position) / _selectedObjects.Count;
		}

		private void OnEnable() {
			UpdateHandlePos();
			_instance = target as IsoTransform;
			_instanceFields = ExposeProperties.GetProperties(_instance);
			if (_instance == null)
				return;
			_instance.GetComponent<Transform>().hideFlags = HideFlags.HideInInspector;//hides Transform component
			UnityEditorInternal.ComponentUtility.MoveComponentUp(_instance);
		}

		private void OnDisable() {
			if (!Tools.hidden)
				return;
			Tools.hidden = false;
			Tools.current = Tool.Move;
		}

		private void XAxisHandle() {
			Handles.color = Handles.xAxisColor;
			var screenHandlePos = Isometric.IsoToScreen(_isoHandlePos);
			var deltaMovement = Handles.Slider(screenHandlePos, Isometric.IsoToScreen(Vector3.right)) - screenHandlePos;
			if (!(Mathf.Abs(deltaMovement.y) > Mathf.Epsilon))
				return;
			ApplyXAxisChanges(deltaMovement.y);
			_isoHandlePos = _selectedObjects.Aggregate(Vector3.zero, (runningSum, t) => runningSum + t.Position) / _selectedObjects.Count;
		}

		private void YAxisHandle() {
			Handles.color = Handles.yAxisColor;
			var screenHandlePos = Isometric.IsoToScreen(_isoHandlePos);
			var deltaMovement = Handles.Slider(screenHandlePos, Isometric.IsoToScreen(Vector3.up)) - screenHandlePos;
			if (!(Mathf.Abs(deltaMovement.y) > Mathf.Epsilon))
				return;
			ApplyYAxisChanges(deltaMovement.y);
			_isoHandlePos = _selectedObjects.Aggregate(Vector3.zero, (runningSum, t) => runningSum + t.Position) / _selectedObjects.Count;
		}

		private void ZAxisHandle() {
			Handles.color = Handles.zAxisColor;
			var screenHandlePos = Isometric.IsoToScreen(_isoHandlePos);
			var deltaMovement = Handles.Slider(screenHandlePos,  Isometric.IsoToScreen(Vector3.forward)) - screenHandlePos;
			if (!(Mathf.Abs(deltaMovement.y) > Mathf.Epsilon))
				return;
			ApplyZAxisChanges(deltaMovement.y);
			_isoHandlePos = _selectedObjects.Aggregate(Vector3.zero, (runningSum, t) => runningSum + t.Position) / _selectedObjects.Count;
		}
	
		/// <summary>
		/// Applies deltaMovement to each selected Object
		/// </summary>
		/// <param name="delta"></param>
		private void ApplyZAxisChanges(float delta) {
			if (_selectedObjects == null)
				return;
			Undo.RecordObjects(_selectedObjects.ToArray(), "ChangePosition");
			foreach (var t in _selectedObjects) {
				var newZ = t.Position.z + delta;
				if (_instance != null && IsoSnapping.DoSnap)
					newZ = Mathf.RoundToInt(newZ);
				t.Position = new Vector3(t.Position.x, t.Position.y,newZ);
				EditorUtility.SetDirty(t);		
			}
		}

		private void ApplyXAxisChanges(float delta) {
			if (_selectedObjects == null)
				return;
			Undo.RecordObjects(_selectedObjects.ToArray(), "ChangePosition");
			foreach (var t in _selectedObjects) {
				var newX = t.Position.x + delta;
				if (_instance != null && IsoSnapping.DoSnap)
					newX = Mathf.RoundToInt(newX);
				t.Position = new Vector3(newX, t.Position.y, t.Position.z);
				EditorUtility.SetDirty(t);
			}
		}

		private void ApplyYAxisChanges(float delta) {
			if (_selectedObjects == null)
				return;
			Undo.RecordObjects(_selectedObjects.ToArray(), "ChangePosition");
			foreach (var t in _selectedObjects) {
				var newY = t.Position.y + delta;
				if (_instance != null && IsoSnapping.DoSnap)
					newY = Mathf.RoundToInt(newY);
				t.Position = new Vector3(t.Position.x, newY, t.Position.z);
				EditorUtility.SetDirty(t);
			}
		}

		private void OnSceneGUI() {
			if (Tools.current == Tool.Move) {
				Tools.hidden = true;
				ZAxisHandle();
				XAxisHandle();
				YAxisHandle();
				if (_instance != null && IsoSnapping.DoSnap)
					_instance.Position = IsoSnapping.Ceil(_instance.Position);
			} else {
				Tools.hidden = false;
			}
		}

		public override void OnInspectorGUI() {
			if (_instance == null)
				return;
			DrawDefaultInspector();
			UpdateHandlePos();
			ExposeProperties.Expose(_instanceFields);
		}
	}


}