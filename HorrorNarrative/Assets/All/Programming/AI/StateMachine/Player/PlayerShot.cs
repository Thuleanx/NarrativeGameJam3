using UnityEngine;
using Thuleanx.Optimization;
using Thuleanx.Mechanics.Danmaku;
using Thuleanx.Math;

namespace Thuleanx.AI {
	[CreateAssetMenu(fileName = "PlayerShot", menuName = "~/StateMachine/Player/PlayerShot", order = 0)]
	public class PlayerShot : PlayerState {
		[SerializeField] float KnockbackAmount = 3f;
		[SerializeField] BubblePool BulletPool;
		[SerializeField] float DistanceFromBody = 1f;
		[SerializeField] float BulletSpeed = 1f;

		public override State ShouldTransitionTo() {
			if (AnimationFinish)
				return StateMachine.FindStateOfType(typeof(PlayerGrounded));
			return null;
		}

		public override void OnEnter() {
			base.OnEnter();
			Agent.PhysicsBody.Knockback(KnockbackAmount, Vector2.right * (Agent.LocalContext.RightFacing ? -1 : 1));
			PlayerLocalContext.GunLoaded = false;
			if (BulletPool != null) SpawnBullet();
		}

		public override void OnExit() {
			base.OnExit();
		}

		public void SpawnBullet() {
			Vector2 spanwPos = (Vector2) Agent.LocalContext.Position + DistanceFromBody 
				* (Vector2.right) * (Agent.LocalContext.RightFacing ? 1 : -1);

			float dir = Random.Range(-PlayerLocalContext.aimArc/2, PlayerLocalContext.aimArc/2);

			GameObject bulletObj = BulletPool.Borrow(spanwPos, Quaternion.Euler(0f, 0f, dir));
			
			Projectile projectile = bulletObj.GetComponent<Projectile>();
			projectile.Velocity = Calc.Rotate(PlayerAgent.LocalContext.RightFacing ? Vector2.right : Vector2.left, 
				Mathf.Deg2Rad * dir) * BulletSpeed;

		}

		public override bool CanEnter() => PlayerLocalContext.Equipment == PlayerEquipment.Blunderbuss
			&& PlayerLocalContext.GunLoaded;

		public override State Clone() => Clone(CreateInstance<PlayerShot>());
		public override State Clone(State state) {
			((PlayerShot) state).KnockbackAmount = KnockbackAmount;
			((PlayerShot) state).BulletPool = BulletPool;
			((PlayerShot) state).BulletSpeed = BulletSpeed;
			((PlayerShot) state).DistanceFromBody= DistanceFromBody;
			return base.Clone(state);
		}
	}
}