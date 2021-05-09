using UnityEngine;
using Thuleanx.Controls;
using Thuleanx.Math;

namespace Thuleanx.AI {
	[CreateAssetMenu(fileName = "PlayerDead", menuName = "~/StateMachine/Player/PlayerDead", order = 0)]
	public class PlayerDead : PlayerState {
		public override State ShouldTransitionTo() { return null; }

		public override void OnUpdate() {
			base.OnUpdate();
		}
	}
}