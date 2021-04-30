using UnityEngine;
using Thuleanx.AI;
using Thuleanx.Math;

class RigidBody1D
{
	public float Position = 0;
	public float Velocity = 0;
	public float Acceleration = 0;

	public RigidBody1D(float pos) {
		Position = pos;
	}

	public void SetForce(float force)
	{
		Acceleration = force;
	}

	public void SetVelocity(float velocity) {
		this.Velocity = velocity;
	}

	public void Update() {
		Velocity += Acceleration * Time.deltaTime;
		Position += Velocity * Time.deltaTime;
	}
}

namespace Thuleanx.Animation {
	public class SquashStretchByVelocity : MonoBehaviour {
		public Agent Agent {get; private set; }

		[Header("Spring Mass Damper")]
		public float springConstant = 50;
		public float dampingConstant = 0.5f;
		public float distanceCapMin = 0.5f, distanceCapMax = 1.5f;
		public float capVelocityWhenOutBounds = .25f;

		[Header("Squash And Stretch")]
		public float strength = .1f;
		public float convergenceTime = .3f;

		RigidBody1D stretch = new RigidBody1D(1f);
		Vector2 original;

		void Awake() {
			Agent = GetComponentInParent<Agent>();
			original = transform.localScale;
		}

		void Update() {
			// rotate to face velocity
			Vector2 lookRotate = Agent.LocalContext.Velocity;
			if (lookRotate == Vector2.zero) lookRotate = Vector2.right;
			lookRotate.Normalize();
			float angle = Mathf.Atan2(lookRotate.y,lookRotate.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, angle);

			var velocity = Agent.LocalContext.Velocity.magnitude;
			if (velocity < .001f) velocity = 0;

			var force = -springConstant * (stretch.Position - 1) - dampingConstant * stretch.Velocity;

			if (!Mathf.Approximately(velocity, 0)) {
				var amount = velocity * strength + 1;
				stretch.Position = amount;
				force = 0;
			}

			stretch.SetForce(force);
			stretch.Update();

			// cap distance
			float clampedValue = Mathf.Clamp(stretch.Position, distanceCapMin, distanceCapMax);
			if (Mathf.Approximately(clampedValue, distanceCapMin) || Mathf.Approximately(clampedValue, distanceCapMax))
				stretch.SetVelocity(Mathf.Clamp(stretch.Velocity, -capVelocityWhenOutBounds, capVelocityWhenOutBounds));
			stretch.Position = clampedValue;

			var inverseAmount = (1 / stretch.Position);

			transform.localScale = new Vector3(
				stretch.Position * original.x, 
				inverseAmount * original.y, 
				1f
			);
		}
	}
}