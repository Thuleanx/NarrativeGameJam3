using UnityEngine;

namespace Thuleanx.AI.Context {
	public class PlayerLocalContext : AgentLocalContext {
		public float aimArc;
		public bool GunLoaded;
		public int BulletsLeft;

		public PlayerLocalContext(Agent agent) : base(agent) {
			GunLoaded = true;
			BulletsLeft = 1;
		 }
	}
}