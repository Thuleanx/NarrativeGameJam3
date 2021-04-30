using UnityEngine;
using Thuleanx.Utility;
using Thuleanx.AI;
using Thuleanx.Math;

namespace Thuleanx.Mechanics.Combat {
	public class Attack : MonoBehaviour, IHitboxResponder {
		[SerializeField] int Damage;
		[SerializeField] float KnockbackForce;
		[SerializeField] Vector2 Direction = Vector2.right;
		public Optional<Hitbox[]> Hitboxes;

		private void Awake() {
			foreach (Hitbox hitbox in Hitboxes.Value)
				hitbox.useResponder(this);
		}

		public void collisionWith(Hurtbox other) {
			PhysicsObject physicsObj = other.Agent.Value.PhysicsBody;
			if (physicsObj != null)
				physicsObj.Knockback(KnockbackForce, Calc.Rotate(Direction, transform.eulerAngles.z*Mathf.Deg2Rad));
			other.Agent.Value.TakeHit(Damage);
		}

	}
}