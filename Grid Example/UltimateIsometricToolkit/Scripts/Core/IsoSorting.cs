using Assets.UltimateIsometricToolkit.Scripts.External;
using UnityEngine;

namespace Assets.UltimateIsometricToolkit.Scripts.Core {
	/// <summary>
	/// Wrapper class for the current sorting strategy
	/// </summary>
	[ExecuteInEditMode]
	public class IsoSorting : Singleton<IsoSorting> {
		[SerializeField] private SortingStrategy _sortingStrategy;
		[SerializeField, HideInInspector] private static float _isometricAngle = 25.112f; //isometric angle
		[HideInInspector]public bool Dirty = true;

		[ExposeProperty]
		public static float IsometricAngle {
			get { return _isometricAngle; }
			set { _isometricAngle = value; }
		}

	
		public void Resolve(IsoTransform isoTransform) {
			Dirty = true;
			if (_sortingStrategy != null)
				_sortingStrategy.Resolve(isoTransform);
			else
				Debug.LogError("Missing SortingStrategy on IsoSorting component");
		}

		public void Update() {
			if (!Dirty)
				return;
			if (_sortingStrategy != null)
				_sortingStrategy.Sort();
			else
				Debug.LogError("Missing SortingStrategy on IsoSorting component");
			Dirty = false;
		}

		public void Remove(IsoTransform isoTransform) {
			if (_sortingStrategy != null)
				_sortingStrategy.Remove(isoTransform);
			else
				Debug.LogError("Missing SortingStrategy on IsoSorting component");
		}
	}
}
