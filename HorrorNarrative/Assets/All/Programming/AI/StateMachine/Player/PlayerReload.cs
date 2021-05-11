using UnityEngine;
using Thuleanx.Controls;
using Thuleanx.Math;

namespace Thuleanx.AI {
	[CreateAssetMenu(fileName = "PlayerReload", menuName = "~/StateMachine/Player/PlayerReload", order = 0)]
	public class PlayerReload : PlayerState {
		[SerializeField, Tooltip("Exponential acceleration. Higher means reaching target faster. ")]
		float AccelerationLambda=8f;

		public override State ShouldTransitionTo() {
			if (AnimationFinish) return StateMachine.FindStateOfType(typeof(PlayerGrounded));
			return null;
		}

		public override void OnEnter() {
			base.OnEnter();
			PlayerLocalContext.GunLoaded = false;
			PlayerLocalContext.Velocity = Vector2.zero;
		}

		public override void OnExit() {
			if (AnimationFinish) {
				PlayerLocalContext.GunLoaded = true;
				PlayerLocalContext.BulletsLeft--;
			}
			base.OnExit();
		}

		public override void OnUpdate() {
			base.OnUpdate();

			if (PlayerAgent.CanControl) {
				Vector2 idealMovement = App.Instance._InputManager.Movement * PlayerContext.AimingMoveSpeed;
				PlayerAgent.LocalContext.Velocity = Calc.Damp(PlayerAgent.LocalContext.Velocity, idealMovement, AccelerationLambda, Time.deltaTime);
			}
		}

		public override bool CanEnter() => 
			!PlayerLocalContext.GunLoaded && PlayerLocalContext.BulletsLeft > 0 &&
			PlayerLocalContext.Equipment == PlayerEquipment.Blunderbuss;
	}
}