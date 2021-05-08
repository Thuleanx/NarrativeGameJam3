using UnityEngine;

namespace Thuleanx.AI {
	[CreateAssetMenu(fileName = "PlayerShot", menuName = "~/StateMachine/Player/PlayerShot", order = 0)]
	public class PlayerShot : PlayerState {
		[SerializeField] float KnockbackAmount = 3f;

		public override State ShouldTransitionTo() {
			if (AnimationFinish)
				return StateMachine.FindStateOfType(typeof(PlayerGrounded));
			return null;
		}

		public override void OnEnter() {
			base.OnEnter();
			Agent.PhysicsBody.Knockback(KnockbackAmount, Vector2.right * (Agent.LocalContext.RightFacing ? -1 : 1));
			PlayerLocalContext.GunLoaded = false;
		}

		public override void OnExit() {
			base.OnExit();
		}
	}
}