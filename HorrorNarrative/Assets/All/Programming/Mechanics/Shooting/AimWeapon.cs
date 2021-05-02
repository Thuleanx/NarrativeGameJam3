using UnityEngine;
using Thuleanx.Controls;
using Thuleanx.Utility;
using Thuleanx.AI;

namespace Thuleanx.Mechanics.Shooting {
	public class AimWeapon : MonoBehaviour {
		[Tooltip("Custom Physics Object of the Agent carrying the gun")]
		public Optional<PhysicsObject> PhysicsBody;
		[Tooltip("Base of the gun, which will be used as pivot to rotate gun to mouse position. If not specified, we assume the current object is the pivot")]
		public Optional<Transform> GunBase;
		public float KnockbackAmount = 3f;

		[Tooltip("Point / barrell of gun, where the bullet is spawned. If not specified, we assume the current object is the gunpoint")]
		public Optional<Transform> GunPoint;

		[Tooltip("Lines for indicating the shooting arc")]
		public Optional<LineRenderer> BottomLine, TopLine;
		public Optional<float> IndicatorRange;

		public float MaxArc = 0f;
		public float MinArc = 0f;
		public float AimDuration = 1f;
		float aimTime;

		private void Awake() {
			if (!GunBase.Enabled) GunBase = new Optional<Transform>(transform);
			if (!GunPoint.Enabled) GunPoint = new Optional<Transform>(transform);
			if (!PhysicsBody.Enabled) PhysicsBody = new Optional<PhysicsObject>(GetComponentInParent<PhysicsObject>());
		}

		void Update() {
			Vector2 target = InputController.Instance.MouseWorldPos;
			Vector2 displacement = target - (Vector2) transform.position;
			float angleDeg = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;

			GunBase.Value.rotation = Quaternion.Euler(0f, 0f, angleDeg);

			if (InputController.Instance.Aiming) {
				float arc = AimDuration == 0 ? MinArc :  Mathf.Lerp(MaxArc, MinArc, Mathf.Clamp01(aimTime/AimDuration));

				if (BottomLine.Enabled && TopLine.Enabled && IndicatorRange.Enabled) {
					BottomLine.Value.gameObject.SetActive(true);
					TopLine.Value.gameObject.SetActive(true);

					Vector2 top = new Vector2(Mathf.Cos(arc/2 * Mathf.Deg2Rad), Mathf.Sin(arc/2 * Mathf.Deg2Rad)) * IndicatorRange.Value;
					Vector2 bot = new Vector2(Mathf.Cos(arc/2 * Mathf.Deg2Rad), -Mathf.Sin(arc/2 * Mathf.Deg2Rad)) * IndicatorRange.Value;

					TopLine.Value.SetPositions(new Vector3[]{Vector2.zero, top});
					BottomLine.Value.SetPositions(new Vector3[]{Vector2.zero, bot});
				}

				aimTime += Time.deltaTime;
			} else {
				if (BottomLine.Enabled) 
					BottomLine.Value.gameObject.SetActive(false);
				if (TopLine.Enabled) 
					TopLine.Value.gameObject.SetActive(false);
				aimTime = 0f;
			}

			if (InputController.Instance.Attack) {
				TempShoot(target);
				InputController.Instance.UseAttackInput();
			}
		}

		void TempShoot(Vector2 position) {
			PhysicsBody.Value.Knockback(KnockbackAmount, ((Vector2) transform.position) - position);

			// reset aim time
			aimTime = 0f;
		}
	}
}