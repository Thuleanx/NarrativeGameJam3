using UnityEngine;
using Thuleanx.Controls;
using Thuleanx.Math;

namespace Thuleanx.AI {
	[CreateAssetMenu(fileName = "PlayerDead", menuName = "~/StateMachine/Player/PlayerDead", order = 0)]
	public class PlayerDead : PlayerState {
		[SerializeField, Tooltip("Exponential acceleration. Higher means reaching target faster. ")]
		float AccelerationLambda=4f;
		[SerializeField, FMODUnity.EventRef] string DeadSound;

		public override State ShouldTransitionTo() { return null; }

		public override void OnUpdate() {
			base.OnUpdate();

			PlayerAgent.LocalContext.Velocity = Calc.Damp(PlayerAgent.LocalContext.Velocity, Vector2.zero, 
				AccelerationLambda, Time.deltaTime);
			App.Instance._AudioManager.PlayOneShot(DeadSound);
		}

		public override State Clone() => Clone(CreateInstance<PlayerDead>());
		public override State Clone(State state) {
			((PlayerDead) state).AccelerationLambda = AccelerationLambda;
			((PlayerDead) state).DeadSound = DeadSound;
			return base.Clone(state);
		}
	}
}