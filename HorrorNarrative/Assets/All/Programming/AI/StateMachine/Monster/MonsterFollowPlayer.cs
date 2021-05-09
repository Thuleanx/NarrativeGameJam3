using UnityEngine;
using Thuleanx;
using Thuleanx.Math;

namespace Thuleanx.AI {
	[CreateAssetMenu(fileName = "MonsterFollowPlayer", menuName = "~/StateMachine/Monster/MonsterFollowPlayer", order = 0)]
	public class MonsterFollowPlayer : MonsterState {
		[SerializeField, Tooltip("Exponential acceleration. Higher means reaching target faster. ")]
		float AccelerationLambda=8f;

		public override void OnUpdate() {
			base.OnUpdate();
			Player player = App.LocalInstance._ContextManager.Player;

			Vector2 targetVelocity = (player.LocalContext.Position - Agent.LocalContext.Position).normalized 
				* Agent.Context.MoveVelocity;
			
			Agent.LocalContext.Velocity = Calc.Damp(Agent.LocalContext.Velocity, targetVelocity, 
				AccelerationLambda, Time.deltaTime);
		}
	}
}