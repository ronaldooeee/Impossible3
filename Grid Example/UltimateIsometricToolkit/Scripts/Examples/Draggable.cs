using System.Linq;
using Assets.UltimateIsometricToolkit.Scripts.Core;
using Assets.UltimateIsometricToolkit.Scripts.Utils;
using UnityEngine;

namespace UltimateIsometricToolkit.examples {
	public class Draggable : MonoBehaviour {
		IsoTransform isoObject;
		void Awake() {
			isoObject = this.GetOrAddComponent<IsoTransform>();
			gameObject.AddComponent<BoxCollider2D>(); //this will detect out touch/click
		}

		void OnMouseDrag() {
			if (!Input.GetMouseButton(0)) return;
			//Press delete or backspace, then delete
			if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace))
				Destroy(gameObject);

			var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			//avoids raycasting to ourself
			disableColldiers();

			var hit = Physics2D.Raycast(mouseWorldPos,Vector2.zero);
			if (hit.collider != null) {
				//did we hit a IsoObject
				var hitIsoObject = hit.collider.GetComponent<IsoTransform>();
				if (hitIsoObject != null) {
					//calc the new height were we want to put our object at
					var newHeight = hitIsoObject.Position.y + hitIsoObject.Size.y/ 2f + isoObject.Size.y/ 2f;
						
					//calc the final position
					var pos = Isometric.CreateXYZfromY(Input.mousePosition, newHeight);
					if (pos != null) {
						//pos = Ceil(pos);
						if (FindObjectsOfType<IsoTransform>().All(c => c.Position != pos))
							isoObject.Position = pos.Value;
					}
				}

					
			} else {
				//we didn't hit anything, keep the current height
				var pos = Isometric.CreateXYZfromY(Input.mousePosition, isoObject.Position.y + isoObject.Size.y / 2f );
				//pos = Ceil(pos);
				if(pos != null)
					isoObject.Position = pos.Value;
			}
			//turn colliders back on
			enableColldiers();
		}

		private int tmpLayer;
		/// <summary>
		/// Disables all colliders attached to this gameObject
		/// </summary>
		private void disableColldiers() {
			//TODO avoid GetComponents call
			tmpLayer = gameObject.layer;
			gameObject.layer = 2;
		}

		/// <summary>
		/// Enables all colliders attached to this gameObject
		/// </summary>
		private void enableColldiers() {
			//TODO avoid GetComponents call
			gameObject.layer = tmpLayer;
		}

		public static Vector3 Ceil(Vector3 input) {
			var x = Mathf.Ceil((input.x));
			var y = Mathf.Ceil((input.y));
			var z = input.z;

			return new Vector3(x, y, z);
		}
	}
}

	