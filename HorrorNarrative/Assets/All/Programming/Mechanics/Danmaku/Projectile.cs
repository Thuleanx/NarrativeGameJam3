using UnityEngine;
using Thuleanx.Optimization;

namespace Thuleanx.Mechanics.Danmaku {
	[RequireComponent(typeof(Rigidbody2D))]
	public class Projectile : MonoBehaviour {
		public Rigidbody2D Body;
		[SerializeField] BubblePool OnDeathBubble;
		public Vector2 Velocity;
		[SerializeField, FMODUnity.EventRef] string DeathSound;

		void Awake() {
			if (Body == null) Body = GetComponent<Rigidbody2D>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			// hit wall
			OnDeathBubble?.Borrow(transform.position, transform.rotation);
			App.Instance._AudioManager.PlayOneShot3D(DeathSound, transform.position);
			gameObject.SetActive(false);
		}

		void Update() {
			if (Body.velocity != Velocity) Body.velocity = Velocity;
		}
	}
}