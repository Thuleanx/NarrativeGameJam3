using UnityEngine;
using Thuleanx.Utility;
using Thuleanx.Math;

namespace Thuleanx.AI.Context {
	public class AgentLocalContext {
		public Agent Agent {get; private set; }

		public Vector2 Position;
		public Vector2 Velocity;

		public int Health;
		public Timer IFrame;
		public Timer Damaged;

		public AgentLocalContext(Agent agent) {
			Agent = agent;
			Health = agent.Context.MaxHealth;
		}
	}
}