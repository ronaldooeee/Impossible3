using UnityEngine;

namespace Assets.UltimateIsometricToolkit.Scripts.Utils {
	/// <summary>
	/// Isometric helper class with utility functions
	/// </summary>
	public class Isometric {
		[SerializeField]
		private static Matrix4x4 _isoMatrix;
		[SerializeField]
		private static float _isoAngle;

		public static readonly Vector2 North = new Vector2(1,0); 
		public static readonly Vector2 South = North*-1;
		public static readonly Vector2 East = new Vector2(0,-1);
		public static readonly Vector2 West = East * -1;
		public static readonly Vector2 NorthEast = North + East;
		public static readonly Vector2 NorthWest = North + West;
		public static readonly Vector2 SouthEast = South + East;
		public static readonly Vector2 SouthWest = South + West;
		
		/// <summary>
		/// Returns a vector from isometric space to screenspace
		/// where xy are the screen coordinates in unity units
		/// z can be neglected.
		/// </summary>
		/// <param name="isoVector">Isometric Vector</param>
		/// <returns>Vector in screen coordinates</returns>
		public static Vector3 IsoToScreen(Vector3 isoVector) {
			var screenPos = getIsoMatrix(27).MultiplyPoint(isoVector);
			//screenPos = new Vector3(screenPos.x, screenPos.y, Camera.main.nearClipPlane);
			return screenPos;
		}
	
		/// <summary>
		/// returns the matrix that convers from isometric space to unity space
		/// </summary>
		/// <param name="isoAngle"></param>
		/// <returns></returns>
		public static Matrix4x4 getIsoMatrix(float isoAngle) {
			if (!(Mathf.Abs(isoAngle - _isoAngle) > Mathf.Epsilon))
				return _isoMatrix;
			_isoMatrix = Matrix4x4.identity;
			_isoAngle = isoAngle;
			var angleInRad = Mathf.Deg2Rad * isoAngle;
			_isoMatrix.m00 = Mathf.Cos(angleInRad);
			_isoMatrix.m02 = -Mathf.Cos(angleInRad);
			_isoMatrix.m01 = 0;
			_isoMatrix.m10 = Mathf.Sin(angleInRad);
			_isoMatrix.m12 = Mathf.Sin(angleInRad);
			_isoMatrix.m11 = 1;
			_isoMatrix.m20 = 1;
			_isoMatrix.m22 = 1;
			_isoMatrix.m21 = -1;
			return _isoMatrix;
		}

		/// <summary>
		/// Finds the isometric position using a screenspacePoint in Pixels and an offset on the x Axis
		/// </summary>
		/// <param name="screenSpacePoint"></param>
		/// <param name="xOffset"></param>
		/// <returns>The isometric position (xOffset,y,z), null instead</returns>
		public static Vector3? CreateXYZfromX(Vector2 screenSpacePoint, float xOffset) {
			return CreateXYZ(screenSpacePoint, new Vector3(-1, 0, 0), xOffset);
		}
		/// <summary>
		/// Finds the isometric position using a screenspacePoint in Pixels and an offset on the z Axis
		/// </summary>
		/// <param name="screenSpacePoint"></param>
		/// <param name="<Offset"></param>
		/// <returns>The isometric position (x,y,zOffset), null instead</returns>
		public static Vector3? CreateXYZfromZ(Vector2 screenSpacePoint, float zOffset) {
			return CreateXYZ(screenSpacePoint, new Vector3(0, 0, 1), zOffset);
		}

		/// <summary>
		/// Finds the isometric point given screenspacePoint in pixels and an offset on the yAxis
		/// Note: May use this to find an isometric position on the floor at screenspacePoint that is yOffset away from the camera on the y-Axis 
		/// </summary>
		/// <param name="screenSpacePoint"></param>
		/// <param name="zOffset"></param>
		/// <returns>The isometric position (x,yOffset,y), null instead</returns>
		public static Vector3? CreateXYZfromY(Vector2 screenSpacePoint, float yOffset) {
			return CreateXYZ(screenSpacePoint, new Vector3(0, -1, 0), yOffset);
		}

		private static Vector3? CreateXYZ(Vector2 screenSpacePoint, Vector3 planeNormalVector, float offset) {
			var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenSpacePoint.x, screenSpacePoint.y, Camera.main.nearClipPlane));

			var plane = new Plane(planeNormalVector, offset); //isometric plane that goes through Offset

			var matrixInverse = getIsoMatrix(_isoAngle).inverse;
			var isoRay = new Ray(matrixInverse.MultiplyPoint(worldPos), matrixInverse.MultiplyPoint(new Vector3(0, 0, 1))); // isometric ray at screenspacepoint

			float distance;
			if (plane.Raycast(isoRay, out distance)) {
				return isoRay.GetPoint(distance);
			}
			return null;
		}

		public Vector3 ScreenToIsoPoint(Vector2 screenSpacePoint, float zOffset = 0f) {
			return CreateXYZfromZ(screenSpacePoint, zOffset).Value;
		}
	}
}
