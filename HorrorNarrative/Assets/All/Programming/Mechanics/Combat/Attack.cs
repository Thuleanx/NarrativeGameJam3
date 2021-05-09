using UnityEngine;
using Thuleanx.Utility;
using Thuleanx.AI;
using Thuleanx.Math;
using UnityEngine.Events;

namespace Thuleanx.Mechanics.Combat {
	public class Attack : MonoBehaviour, IHitboxResponder {
		[SerializeField] int Damage;
		[SerializeField] float KnockbackForce;
		[SerializeField] Optional<Vector2> Direction;
		public Optional<Hitbox[]> Hitboxes;

		private void Awake() {
			foreach (Hitbox hitbox in Hitboxes.Value)
				hitbox.useResponder(this);
		}

		public void collisionWith(Hurtbox other) {
			PhysicsObject physicsObj = other.Agent.Value.PhysicsBody;
			if (physicsObj != null) {
				Vector2 Dir = Direction.Enabled ? Direction.Value : other.Agent.Value.LocalContext.Position - (Vector2) transform.position;
				physicsObj.Knockback(KnockbackForce, Calc.Rotate(Dir, transform.eulerAngles.z*Mathf.Deg2Rad));
			}
			other.Agent.Value.TakeHit(Damage);
		}

	}
}