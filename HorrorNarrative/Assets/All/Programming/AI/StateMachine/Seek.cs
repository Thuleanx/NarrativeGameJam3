using UnityEngine;
using Thuleanx.Master;

namespace Thuleanx.AI {
	[CreateAssetMenu(fileName = "Seek", menuName = "~/Behaviour/Steering/Seek", order = 0)]
	public class Seek : State {
		[SerializeField] float steeringForce;

		public override void OnUpdate() {
			Vector2 Desired_Velocity = (Game.Instance.Context.PlayerPosition - 
				Agent.LocalContext.Position).normalized * Agent.Context.MoveVelocity;

			Vector2 Steering = Vector2.ClampMagnitude(Desired_Velocity - Agent.LocalContext.Velocity, steeringForce);

			Agent.LocalContext.Velocity = Vector2.ClampMagnitude(Steering * Time.deltaTime + Agent.LocalContext.Velocity, 
				Agent.Context.MoveVelocity);
		}
	}
}