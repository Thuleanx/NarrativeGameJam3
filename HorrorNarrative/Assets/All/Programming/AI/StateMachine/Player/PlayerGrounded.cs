using UnityEngine;
using Thuleanx.Controls;

namespace Thuleanx.AI {
	
	[CreateAssetMenu(fileName = "PlayerGrounded", menuName = "~/StateMachine/Player/PlayerGrounded", order = 0)]
	public class PlayerGrounded : PlayerState {
		public override void OnUpdate() {
			base.OnUpdate();

			if (PlayerAgent.CanControl) {
				Vector2 idealMovement = InputController.Instance.Movement * PlayerAgent.Context.MoveVelocity;
				PlayerAgent.LocalContext.Velocity = idealMovement;
			}
		}
	}
}