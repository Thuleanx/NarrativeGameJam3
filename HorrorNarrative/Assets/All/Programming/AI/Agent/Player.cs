using UnityEngine;

namespace Thuleanx.AI {
	public class Player : Agent {
		public bool CanControl {get; private set; }

		public override void Awake() {
			base.Awake();
			CanControl = true;
		}
	}
}