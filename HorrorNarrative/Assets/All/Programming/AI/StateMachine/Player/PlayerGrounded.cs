using UnityEngine;
using Thuleanx.Controls;
using Thuleanx.Math;

namespace Thuleanx.AI {
	
	[CreateAssetMenu(fileName = "PlayerGrounded", menuName = "~/StateMachine/Player/PlayerGrounded", order = 0)]
	public class PlayerGrounded : PlayerState {
		[SerializeField, Tooltip("Exponential acceleration. Higher means reaching target faster. ")]
		float AccelerationLambda=8f;

		public override State ShouldTransitionTo() {
			if (InputController.Instance.Aiming) 
				return StateMachine.FindStateOfType(typeof(PlayerAiming));
			if (InputController.Instance.Reload) {
				InputController.Instance.UseReloadInput();
				if (!PlayerLocalContext.GunLoaded && PlayerLocalContext.BulletsLeft > 0)
					return StateMachine.FindStateOfType(typeof(PlayerReload));
				return null;
			}
			return null;
		}

		public override void OnUpdate() {
			base.OnUpdate();

			if (PlayerAgent.CanControl) {
				Vector2 idealMovement = InputController.Instance.Movement * PlayerAgent.Context.MoveVelocity;
				PlayerAgent.LocalContext.Velocity = Calc.Damp(PlayerAgent.LocalContext.Velocity, idealMovement, AccelerationLambda, Time.deltaTime);
			}
		}
	}
}