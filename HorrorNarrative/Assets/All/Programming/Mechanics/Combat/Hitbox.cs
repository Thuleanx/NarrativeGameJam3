using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Thuleanx.Utility;
using Thuleanx.AI;
using Thuleanx.Math;

namespace Thuleanx.Mechanics.Combat {
	[RequireComponent(typeof(BoxCollider2D))]
	public class Hitbox : MonoBehaviour {
		enum ColliderState {
			Open,
			Closed,
			Colliding
		}

		public int MaxHurtboxes = 50;

		public Optional<Agent> Agent;
		BoxCollider2D Box;

		public bool DefaultActive;

		[SerializeField] LayerMask Hurtmask;
		[SerializeField, Min(0f)] float DamageFrequency;

		[SerializeField] Color inactiveColor;
		[SerializeField] Color collisionOpenColor;
		[SerializeField] Color collidingColor;

		Dictionary<long, Timer> HurtboxIds = new Dictionary<long, Timer>();
		ColliderState _state;

		IHitboxResponder _responder = null;	

		void Awake() {
			Box = GetComponent<BoxCollider2D>();
			Reset();

			if (DefaultActive) startCheckingCollision();
			else stopCheckingCollision();
		}

		public void Reset() {
			HurtboxIds.Clear();
		}

		public void startCheckingCollision() {
			_state = ColliderState.Open;
		}

		public void stopCheckingCollision() {
			_state = ColliderState.Closed;
		}

		void checkGizmosColor() {
			switch (_state) {
				case ColliderState.Closed:
					Gizmos.color = inactiveColor;
					break;
				case ColliderState.Open:
					Gizmos.color = collisionOpenColor;
					break;
				case ColliderState.Colliding:
					Gizmos.color = collidingColor;
					break;
			}
		}

		public void useResponder(IHitboxResponder responder) {
			_responder = responder;
		}

		List<Hurtbox> GetOverlappingHurtboxes() {
			List<Hurtbox> results = new List<Hurtbox>();		
			Collider2D[] receiver = new Collider2D[MaxHurtboxes];

			ContactFilter2D filter = new ContactFilter2D();
			filter.layerMask = Hurtmask;
			filter.useLayerMask = true;

			int count = Box.OverlapCollider(filter, receiver);

			for (int i = 0; i < count; i++) {
				Hurtbox Hurtbox = receiver[i].GetComponent<Hurtbox>();
				results.Add(Hurtbox);
			}
		
			return results;
		}

		void Update() {
			if (_state == ColliderState.Closed) { return; }
			List<Hurtbox> Hurtboxes = GetOverlappingHurtboxes();

			foreach (Hurtbox Hurtbox in Hurtboxes) {
				if (!HurtboxIds.ContainsKey(Hurtbox.ID) || (!HurtboxIds[Hurtbox.ID] && DamageFrequency > 0) ) {
					if (!HurtboxIds.ContainsKey(Hurtbox.ID))
						HurtboxIds.Add(Hurtbox.ID, new Timer(DamageFrequency > 0?1f/ DamageFrequency : 1f));
					HurtboxIds[Hurtbox.ID].Start();

					// Deal with hitbox hitting
					_responder?.collisionWith(Hurtbox);
				}
			}
			if (Hurtboxes.Count > 0) {
				_state = ColliderState.Colliding;
			} else {
				_state = ColliderState.Open;
			}
			_state = Hurtboxes.Count>0?ColliderState.Colliding:ColliderState.Open;
		}

		void OnDrawGizmos() {
			checkGizmosColor();
			Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
			if (Box == null) Box = GetComponent<BoxCollider2D>();
			Gizmos.DrawCube(Vector3.zero, new Vector3(Box.size.x, Box.size.y, 1f));
		}
	}
}