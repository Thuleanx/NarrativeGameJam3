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
		[SerializeField, FMODUnity.EventRef] string AimingSound;
		[SerializeField, FMODUnity.EventRef] string AimingSteadySound;
		FMOD.Studio.EventInstance AimingInstance;
		FMOD.Studio.EventInstance AimingSteadyInstance;
		bool steady = false;

		public override State ShouldTransitionTo() {
			if (App.Instance._InputManager.Attack) {
				App.Instance._InputManager.UseAttackInput();
				if (StateMachine.FindStateOfType(typeof(PlayerShot)).CanEnter())
					return StateMachine.FindStateOfType(typeof(PlayerShot));
				else if (StateMachine.FindStateOfType(typeof(PlayerBlankShot)).CanEnter())
					return StateMachine.FindStateOfType(typeof(PlayerBlankShot));
			}
			if (!App.Instance._InputManager.Aiming) {
				PlayerLocalContext.aimArc = MaxArc;
				return StateMachine.FindStateOfType(typeof(PlayerGrounded));
			}
			if (App.Instance._InputManager.Reload ) {
				App.Instance._InputManager.UseReloadInput();
				if (StateMachine.FindStateOfType(typeof(PlayerReload)).CanEnter())
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
			AimingInstance = App.Instance._AudioManager.GetInstance(AimingSound);
			AimingSteadyInstance = App.Instance._AudioManager.GetInstance(AimingSteadySound);
			AimingInstance.start();
			steady = false;
		}

		public override void OnUpdate() {
			base.OnUpdate();
			PlayerLocalContext.aimArc = AimDuration == 0 ? MinArc :  Mathf.Lerp(MaxArc, MinArc, Mathf.Clamp01((Time.time - aimTimeStart)/AimDuration));

			if (AimDuration < Time.time - aimTimeStart && !steady) {
				AimingInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
				AimingSteadyInstance.start();
				steady = true;
			}

			if (PlayerAgent.CanControl) {
				Vector2 idealMovement = App.Instance._InputManager.Movement * PlayerContext.AimingMoveSpeed;
				PlayerAgent.LocalContext.Velocity = Calc.Damp(PlayerAgent.LocalContext.Velocity, idealMovement, AccelerationLambda, Time.deltaTime);
			}
		}

		public override void OnExit() {
			AimingInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			AimingSteadyInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			base.OnExit();
		}

		public override bool CanEnter() => PlayerLocalContext.Equipment == PlayerEquipment.Blunderbuss;

		public override State Clone() => Clone(CreateInstance<PlayerAiming>());
		public override State Clone(State state) {
			((PlayerAiming) state).AccelerationLambda = AccelerationLambda;
			((PlayerAiming) state).MaxArc = MaxArc;
			((PlayerAiming) state).MinArc = MinArc;
			((PlayerAiming) state).AimDuration = AimDuration;
			((PlayerAiming) state).AimingSound = AimingSound;
			((PlayerAiming) state).AimingSteadySound = AimingSteadySound;
			return base.Clone(state);
		}
	}
}