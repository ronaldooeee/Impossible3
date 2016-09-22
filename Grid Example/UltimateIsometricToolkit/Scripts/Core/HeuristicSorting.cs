using System.Collections.Generic;
using Assets.UltimateIsometricToolkit.Scripts.Utils;

namespace Assets.UltimateIsometricToolkit.Scripts.Core {
	/// <summary>
	/// Calculates a continuous depth value for each IsoTransform based on its position. Then sorts by depth
	/// </summary>
	public class HeuristicSorting : SortingStrategy {
		private HashSet<IsoTransform> _entities = new HashSet<IsoTransform>(); 
		
		public override void Resolve(IsoTransform isoTransform) {
			isoTransform.transform.position = Isometric.IsoToScreen(isoTransform.Position);
			_entities.Add(isoTransform);
		}

		public override IEnumerable<IsoTransform> Entities {
			get { throw new System.NotImplementedException(); }
		}

		public override void Remove(IsoTransform isoTransform) {
			_entities.Remove(isoTransform);
		}


		public override void Sort() {
			
		}
	}



}
