using UnityEngine;
using Thuleanx.Utility;
using Thuleanx.Animation;

namespace Thuleanx.AI {
	public class PlayerAnimatorInfoTracker : MonoBehaviour {
		Agent Agent;
		FlipWithPlayer[] toFlips;
		float lastX = 1;

		void Awake() {
			Agent = GetComponentInParent<Agent>();
			toFlips = GetComponentsInChildren<FlipWithPlayer>();
		}

		void Update() {
			Agent.Anim.SetFloat("VelocityX", Agent.LocalContext.Velocity.x);
			Agent.Anim.SetFloat("VelocityY", Agent.LocalContext.Velocity.y);
			Agent.Anim.SetFloat("Speed", Agent.LocalContext.Velocity.magnitude);

			if (Agent.Machine.Enabled && ((PlayerState) Agent.Machine.Value.Current).CanFlip) {
				foreach (FlipWithPlayer candidate in toFlips) {
					candidate.Flip((Agent.LocalContext.Velocity.x != 0 ? Agent.LocalContext.Velocity.x : lastX) > 0);
				}
			}

			if (Agent.LocalContext.Velocity.x != 0)
				lastX = Agent.LocalContext.Velocity.x;
		}
	}
}