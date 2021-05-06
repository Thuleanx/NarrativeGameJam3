using UnityEngine;
using Thuleanx.Controls;
using Thuleanx.Math;

namespace Thuleanx.AI {
	
	[CreateAssetMenu(fileName = "PlayerHalt", menuName = "~/StateMachine/Player/PlayerHalt", order = 0)]
	public class PlayerHalt : PlayerState {

		public override void OnEnter() {
			base.OnEnter();

			PlayerAgent.LocalContext.Velocity = Vector2.zero;
		}
	}
}