using UnityEngine;
using Thuleanx.Optimization;

namespace Thuleanx.Mechanics.Danmaku {
	[RequireComponent(typeof(Rigidbody2D))]
	public class Projectile : MonoBehaviour {
		public Rigidbody2D Body;
		[SerializeField] BubblePool OnDeathBubble;
		public Vector2 Velocity;


		void Awake() {
			if (Body == null) Body = GetComponent<Rigidbody2D>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			// hit wall
			OnDeathBubble?.Borrow(transform.position, transform.rotation);
			gameObject.SetActive(false);
		}

		void Update() {
			if (Body.velocity != Velocity) Body.velocity = Velocity;
		}
	}
}