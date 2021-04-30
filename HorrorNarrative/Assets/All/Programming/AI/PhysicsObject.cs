using UnityEngine;
using Thuleanx.AI.Context;
using Thuleanx.Math;

namespace Thuleanx.AI {
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Collider2D))]
	public class PhysicsObject : MonoBehaviour {
		// Body is forced to be kinematics. Please
		public Rigidbody2D Body {get; private set; }
		public Collider2D Collider {get; private set; }
		public Agent Agent {get; private set; }

		// Describe Body
		[SerializeField] float KnockbackLamda;

		public float KnockbackAmt {get; private set; }
		public Vector2 KnockbackDir {get; private set; }

		void Awake() {
			Body = GetComponent<Rigidbody2D>();
			Collider = GetComponent<Collider2D>();
			Agent = GetComponent<Agent>();
		}

		void FixedUpdate() {
			Agent.LocalContext.Position = Body.position;

			Vector2 Move = Agent.LocalContext.Velocity * Time.fixedDeltaTime;
			if (!Calc.Approximately(KnockbackAmt, 0)) {
				Move += KnockbackDir * KnockbackAmt;
				KnockbackAmt = Calc.Damp(KnockbackAmt, 0, KnockbackLamda, Time.fixedDeltaTime);
			}
			Body.MovePosition((Vector2) Body.position + Move);
		}

		public void Knockback(float amt) {
			if (amt > 0) {
				float r = Random.Range(0, 2*Mathf.PI);
				KnockbackDir = new Vector2(Mathf.Cos(r), Mathf.Sin(r));
				KnockbackAmt = amt;
			}
		}

		public void Knockback(float amt, Vector2 dir) {
			if (dir != Vector2.zero) {
				KnockbackDir = dir.normalized;
				KnockbackAmt = amt;
			}
		}

		public void Stop() {
			Agent.LocalContext.Velocity = Vector2.zero;
			KnockbackAmt = 0f;
		}
	}
}