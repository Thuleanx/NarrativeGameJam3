using UnityEngine;
using Thuleanx.Utility;

namespace Thuleanx.Mechanics.Danmaku {

	public class Bullet : MonoBehaviour {
		public int framesPerCheck = 10;

		public float fleetSpeed = .5f;
		[HideInInspector] public Vector2 velocity;
		[SerializeField] string DeactivateTrigger;
		public Collider2D Collider {get; private set; }
		public Animator Anim {get; private set; }

		int frames = 0;

		void Awake() {
			Collider = GetComponent<Collider2D>();
			Anim = GetComponent<Animator>();
		}

		void OnEnable() {
			Collider.enabled = true;
		}

		public void Deactivate() {
			Collider.enabled = false;
			velocity = velocity == Vector2.zero ? velocity : velocity.normalized * fleetSpeed;
			Anim?.SetTrigger(DeactivateTrigger);
		}

		public void Expire() {
			gameObject.SetActive(false);
		}

		public virtual void UpdateVelocity() {}

		public virtual void Update() {
			transform.Translate(velocity*Time.deltaTime);
			// every 10 frames
			if (frames++ == framesPerCheck) {
				if (!General.InCamera(transform.position)) Expire();
				frames = 0;
			}
		}
	}
}