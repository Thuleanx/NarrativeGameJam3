using UnityEngine;

namespace Thuleanx.AI {
	public class PlayerState : State {
		protected Player PlayerAgent { get { return (Player) Agent; } }
	}
}