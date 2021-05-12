using UnityEngine;

namespace Thuleanx.AI.Context {
	public class PlayerLocalContext : AgentLocalContext {
		public float aimArc;
		public bool GunLoaded;
		public int BulletsLeft;
		public PlayerEquipment Equipment;

		public PlayerLocalContext(Agent agent) : base(agent) {
			GunLoaded = true;
			BulletsLeft = ((PlayerContext) agent.Context).DefaultBulletCount;
			Equipment = ((PlayerContext) agent.Context).DefaultEquipment;
		 }
	}
}