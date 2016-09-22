using System.Collections.Generic;
using Assets.UltimateIsometricToolkit.Scripts.External;
using Assets.UltimateIsometricToolkit.Scripts.Utils;
using UnityEngine;

namespace Assets.UltimateIsometricToolkit.Scripts.Core {

	/// <summary>
	/// Isometric transform component
	/// </summary>
	[ExecuteInEditMode, DisallowMultipleComponent]
	public class IsoTransform : MonoBehaviour {
		/// <summary>
		/// Flag to toggle isometric bounds gizmos in scene gui
		/// </summary>
		public bool ShowBounds = true; 

		/// <summary>
		/// Child IsoTransform components
		/// </summary>
		private readonly List<IsoTransform> _children = new List<IsoTransform>();
		private int _lastChildCount;

		[SerializeField, HideInInspector] private Vector3 _position = Vector3.zero;
		[SerializeField, HideInInspector] private Vector3 _size = Vector3.one;

		/// <summary>
		/// Isometric position of this GameObject in worldspace
		/// </summary>
		[ExposeProperty]
		public Vector3 Position {
			get { return _position; }
			set {
				var delta = value - _position;
				_position = value;
				IsoSorting.Instance.Resolve(this);

				//apply delta to each child
				if (transform.childCount != _lastChildCount) //indicates hirarchy for this isoObj changed
					UpdateChildren();

				if (transform.childCount <= 0)
					return;
				for (var i = 0; i < _children.Count; i++) {
					_children[i]._position += delta;
					IsoSorting.Instance.Resolve(this);
				}
			}
		}
		/// <summary>
		/// Isometric size of this GameObject
		/// </summary>
		[ExposeProperty]
		public Vector3 Size {
			get { return _size; }
			set {
				_size = value;
			}
		}

		public Vector3 Min {
			get { return Position - Size/2; }
		}

		public Vector3 Max {
			get { return Position + Size/2; }
		}

		/// <summary>
		/// Isometric size of this GameObject
		/// </summary>
		public float Depth {
			get { return transform.position.z; }
			set {
				transform.position = new Vector3(transform.position.x, transform.position.y, value);
			}
		}

		/// <summary>
		/// Updates the internal list of child isoTransforms to be updated when this instance changes
		/// </summary>
		private void UpdateChildren() {
			_children.Clear();
			for (var i = 0; i < GetComponentsInChildren<IsoTransform>().Length; i++) {
				var child = GetComponentsInChildren<IsoTransform>()[i];
				if (child != this)
					_children.Add(child);
			}
			_lastChildCount = _children.Count;
		}


		void Start() {
			if (IsoSorting.Instance != null)
				IsoSorting.Instance.Resolve(this);
			UpdateChildren();
		}

		void OnDestroy() {
			if (IsoSorting.Instance != null)
				IsoSorting.Instance.Remove(this);
			transform.hideFlags = HideFlags.None;
		}

		#region Gizmos
		public void DrawScreenRect() {
			var screeRect = GetScreenRect();
			Gizmos.DrawLine(screeRect.min,screeRect.min + Vector2.right * screeRect.width); // lower bar
			Gizmos.DrawLine(screeRect.min + Vector2.up * screeRect.height, screeRect.max); // upper bar
			Gizmos.DrawLine(screeRect.min, screeRect.min + Vector2.up*screeRect.height); //left bar
			Gizmos.DrawLine(screeRect.min + Vector2.right * screeRect.width, screeRect.max); // right bar
		}

		public void Draw() {
				Gizmos.color = Color.white;
				GizmosExtension.DrawIsoWireCube(Position,Size);
		}

		

		void OnDrawGizmos() {
			if (!ShowBounds)
				return;
			Draw();
			//DrawScreenRect(); 
		}
#endregion
		/// <summary>
		/// Translates isotransform by a delta
		/// </summary>
		/// <param name="delta"></param>
		public void Translate(Vector3 delta) {
			if (delta == Vector3.zero)
				return;
			Position += delta;
		}

		/// <summary>
		/// Calculates the bounding box screen rect for this isoTransform
		/// </summary>
		/// <returns></returns>
		private Rect GetScreenRect() {
			var left = Isometric.IsoToScreen(Position + new Vector3(-Size.x / 2, -Size.y / 2, Size.z / 2)).x;
			var right = Isometric.IsoToScreen(Position + new Vector3(Size.x / 2, -Size.y / 2, -Size.z / 2)).x;
			var bottom = Isometric.IsoToScreen(Position - new Vector3(Size.x / 2, Size.y / 2, Size.z / 2)).y;
			var top = Isometric.IsoToScreen(Position + new Vector3(Size.x / 2, Size.y / 2, Size.z / 2)).y;
			return new Rect(left, bottom, right - left, top - bottom);
		}
	}
}