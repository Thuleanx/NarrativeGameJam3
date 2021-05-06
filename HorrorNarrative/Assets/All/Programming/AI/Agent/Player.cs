using UnityEngine;

namespace Thuleanx.AI {
	public class Player : Agent {
		public bool CanControl {get; private set; }

		public static Player Instance;

		public override void Awake() {
			base.Awake();
			CanControl = true;
			Instance = this;
		}

		public void ForcePlayerState(PlayerState State) {
			if (State != null)
				Machine.Value.Transition(State);
		}

		public void UnHalt() {
			Machine.Value.Reset();
		}

		public void Halt() => ForcePlayerState((PlayerState) Machine.Value.FindStateOfType(typeof(PlayerHalt)));
	}
}