using UnityEngine;
using Thuleanx.Math;	

namespace Thuleanx.Mechanics.Knockback {
	public class KnockbackRay : KnockbackNormalProvider {
		public override Vector2 Normal(Vector2 position) {
			float rotation = transform.rotation.eulerAngles.z;
			Vector2 perp = Calc.Rotate(Vector2.right, (rotation + 90f) * Mathf.Deg2Rad);
			float amt = Vector2.Dot(perp, position - (Vector2) transform.position);
			if (amt == 0) amt = 1;
			return (perp*amt).normalized;
		}
	}
}