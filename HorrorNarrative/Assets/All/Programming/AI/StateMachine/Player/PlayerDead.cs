using UnityEngine;
using Thuleanx.Controls;
using Thuleanx.Math;

namespace Thuleanx.AI {
	[CreateAssetMenu(fileName = "PlayerDead", menuName = "~/StateMachine/Player/PlayerDead", order = 0)]
	public class PlayerDead : PlayerState {
		[SerializeField, Tooltip("Exponential acceleration. Higher means reaching target faster. ")]
		float AccelerationLambda=4f;

		public override State ShouldTransitionTo() { return null; }

		public override void OnUpdate() {
			base.OnUpdate();

			PlayerAgent.LocalContext.Velocity = Calc.Damp(PlayerAgent.LocalContext.Velocity, Vector2.zero, 
				AccelerationLambda, Time.deltaTime);
		}
	}
}