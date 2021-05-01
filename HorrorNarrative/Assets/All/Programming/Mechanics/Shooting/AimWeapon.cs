using UnityEngine;
using Thuleanx.Controls;
using Thuleanx.Utility;
using Thuleanx.AI;

namespace Thuleanx.Mechanics.Shooting {
	public class AimWeapon : MonoBehaviour {
		public Optional<PhysicsObject> PhysicsBody;

		[Tooltip("Base of the gun, which will be used as pivot to rotate gun to mouse position. If not specified, we assume the current object is the pivot")]
		public Optional<Transform> GunBase;

		public float KnockbackAmount = 3f;

		private void Awake() {
			if (!GunBase.Enabled) GunBase = new Optional<Transform>(transform);
			if (!PhysicsBody.Enabled) PhysicsBody = new Optional<PhysicsObject>(GetComponentInParent<PhysicsObject>());
		}

		void Update() {
			Vector2 target = InputController.Instance.MouseWorldPos;
			Vector2 displacement = target - (Vector2) transform.position;
			float angleDeg = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;

			GunBase.Value.rotation = Quaternion.Euler(0f, 0f, angleDeg);

			if (InputController.Instance.Attack) {
				TempShoot(target);
				InputController.Instance.UseAttackInput();
			}
		}

		void TempShoot(Vector2 position) {
			PhysicsBody.Value.Knockback(KnockbackAmount, ((Vector2) transform.position) - position);
		}
	}
}