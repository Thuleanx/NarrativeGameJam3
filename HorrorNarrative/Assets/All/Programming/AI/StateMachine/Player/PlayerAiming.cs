using UnityEngine;
using Thuleanx.Controls;
using Thuleanx.Math;

namespace Thuleanx.AI {
	[CreateAssetMenu(fileName = "PlayerAiming", menuName = "~/StateMachine/Player/PlayerAiming", order = 0)]
	public class PlayerAiming : PlayerState {
		[SerializeField, Tooltip("Exponential acceleration. Higher means reaching target faster. ")]
		float AccelerationLambda=8f;

		public float MaxArc = 0f;
		public float MinArc = 0f;
		public float AimDuration = 1f;
		float aimTimeStart;

		public override State ShouldTransitionTo() {
			if (InputController.Instance.Attack) {
				InputController.Instance.UseAttackInput();
				return PlayerLocalContext.GunLoaded ? StateMachine.FindStateOfType(typeof(PlayerShot))
					: StateMachine.FindStateOfType(typeof(PlayerBlankShot));
			}
			if (!InputController.Instance.Aiming) {
				PlayerLocalContext.aimArc = MaxArc;
				return StateMachine.FindStateOfType(typeof(PlayerGrounded));
			}
			if (InputController.Instance.Reload) {
				InputController.Instance.UseReloadInput();
				if (!PlayerLocalContext.GunLoaded && PlayerLocalContext.BulletsLeft > 0)
					return StateMachine.FindStateOfType(typeof(PlayerReload));
				else {
					// play some contextual sound
				}
			}
			return null;
		}

		public override void OnEnter() {
			base.OnEnter();
			PlayerLocalContext.Velocity = Vector2.zero;
			aimTimeStart = Time.time;
		}

		public override void OnUpdate() {
			base.OnUpdate();
			PlayerLocalContext.aimArc = AimDuration == 0 ? MinArc :  Mathf.Lerp(MaxArc, MinArc, Mathf.Clamp01((Time.time - aimTimeStart)/AimDuration));

			if (PlayerAgent.CanControl) {
				Vector2 idealMovement = InputController.Instance.Movement * PlayerContext.AimingMoveSpeed;
				PlayerAgent.LocalContext.Velocity = Calc.Damp(PlayerAgent.LocalContext.Velocity, idealMovement, AccelerationLambda, Time.deltaTime);
			}
		}
		

		public override void OnExit() {
			base.OnExit();
		}
	}
}