using UnityEngine;
using Thuleanx.Optimization;
using Thuleanx.Mechanics.Corpse;

namespace Thuleanx.AI {
	public class Mob : Agent {
		public BubblePool CorpsePool;

		public override void OnDeath() {
			if (CorpsePool != null) {
				GameObject Obj = CorpsePool.Borrow(transform.position, Quaternion.identity);
				if (PhysicsBody != null) {
					Corpse corpse = Obj.GetComponent<Corpse>();
					corpse.Dir = PhysicsBody.KnockbackDir;
				}
			}
			base.OnDeath();
		}
	}
}