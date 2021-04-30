using UnityEngine;

namespace Thuleanx.AI.Context {

	[CreateAssetMenu(fileName = "AgentContext", menuName = "~/Context/AgentContext", order = 0)]
	public class AgentContext : ScriptableObject {
		public float MoveVelocity;
		public int MaxHealth;
	}
}