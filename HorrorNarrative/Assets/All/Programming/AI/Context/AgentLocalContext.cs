using UnityEngine;
using Thuleanx.Utility;
using Thuleanx.Math;

namespace Thuleanx.AI.Context {
	public class AgentLocalContext {
		public Agent Agent {get; private set; }

		public Vector2 Position;
		public Vector2 Velocity;

		public int Health {get; private set; }
		public Timer IFrame;
		public Timer Damaged;
		public bool RightFacing;
		public PlayerEquipment Equipment;

		public AgentLocalContext(Agent agent) {
			Agent = agent;
			Health = agent.Context.MaxHealth;
			RightFacing = true;
			Equipment = PlayerEquipment.NONE;
		}

		public void TakeDamage(int amt) { Health = Mathf.Clamp(Health - amt, 0, Agent.Context.MaxHealth); }
		public void Heal(int amt) { Health = Mathf.Clamp(Health + amt, 0, Agent.Context.MaxHealth); }
	}
}