using UnityEngine;

namespace Thuleanx.AI {
	[CreateAssetMenu(fileName = "StateMachineCore", menuName = "~/StateMachine/StateMachineCore", order = 0)]
	public class StateMachineCore : ScriptableObject {
		public State[] States;
	}
}