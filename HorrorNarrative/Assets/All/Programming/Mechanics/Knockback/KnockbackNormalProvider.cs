using UnityEngine;

namespace Thuleanx.Mechanics.Knockback {
	public class KnockbackNormalProvider : MonoBehaviour {
		public virtual Vector2 Normal(Vector2 position) {
			Vector2 knockback = (position - (Vector2) transform.position);
			if (knockback == Vector2.zero) knockback = Vector2.right;
			return knockback.normalized;
		}
	}
}