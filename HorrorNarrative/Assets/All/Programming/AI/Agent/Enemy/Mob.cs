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

		public override void Update() {
			base.Update();

			if (Machine.Enabled && ((MonsterState) Machine.Value.Current).CanFlip) {
				if (LocalContext.Velocity.x != 0)
					LocalContext.RightFacing = LocalContext.Velocity.x > 0;
			}
		}
	}
}