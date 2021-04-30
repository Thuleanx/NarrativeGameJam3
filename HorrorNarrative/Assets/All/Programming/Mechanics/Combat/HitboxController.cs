using UnityEngine;
using Thuleanx.Utility;

namespace Thuleanx.Mechanics.Combat {
	public class HitboxController : MonoBehaviour {
		public Optional<Hitbox[]> Hitboxes;

		public void hitboxesStartCollide() {
			if (Hitboxes.Enabled)
				foreach (Hitbox hitbox in Hitboxes.Value)
					hitbox.startCheckingCollision();
		}

		public void hitboxesEndCollide() {
			if (Hitboxes.Enabled)
				foreach (Hitbox hitbox in Hitboxes.Value)
					hitbox.stopCheckingCollision();
		}

		public void hitboxesResetCollision() {
			if (Hitboxes.Enabled)
				foreach (Hitbox hitbox in Hitboxes.Value)
					hitbox.Reset();
		}

		public void hitboxesEndAndResetCollision() {
			if (Hitboxes.Enabled)
				foreach (Hitbox hitbox in Hitboxes.Value) {
					hitbox.stopCheckingCollision();
					hitbox.Reset();
				}
		}
	}
}