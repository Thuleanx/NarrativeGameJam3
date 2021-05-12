using UnityEngine;

namespace Thuleanx.AI {
	[CreateAssetMenu(fileName = "PlayerBlankShot", menuName = "~/StateMachine/Player/PlayerBlankShot", order = 0)]
	public class PlayerBlankShot : PlayerState {
		public override State ShouldTransitionTo() {
			if (AnimationFinish)
				return StateMachine.FindStateOfType(typeof(PlayerGrounded));
			return null;
		}

		public override void OnEnter() {
			base.OnEnter();
		}

		public override void OnExit() {
			base.OnExit();
		}

		public override bool CanEnter() => 
			PlayerLocalContext.Equipment == PlayerEquipment.Blunderbuss && !PlayerLocalContext.GunLoaded;

		public override State Clone() => Clone(CreateInstance<PlayerBlankShot>());
		public override State Clone(State state) {
			return base.Clone(state);
		}
	}
}