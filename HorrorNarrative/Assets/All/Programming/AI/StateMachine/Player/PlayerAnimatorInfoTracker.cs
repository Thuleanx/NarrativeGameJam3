using UnityEngine;
using Thuleanx.Utility;
using Thuleanx.Animation;

namespace Thuleanx.AI {
	public class PlayerAnimatorInfoTracker : MonoBehaviour {
		Agent Agent;
		FlipWithPlayer[] toFlips;

		void Awake() {
			Agent = GetComponentInParent<Agent>();
			toFlips = GetComponentsInChildren<FlipWithPlayer>();
		}

		void Update() {
			Agent.Anim.SetFloat("VelocityX", Agent.LocalContext.Velocity.x);
			Agent.Anim.SetFloat("VelocityY", Agent.LocalContext.Velocity.y);
			Agent.Anim.SetFloat("Speed", Agent.LocalContext.Velocity.magnitude);
			Agent.Anim.SetFloat("Loaded", ((Context.PlayerLocalContext) Agent.LocalContext).GunLoaded ? 1 : 0);

			foreach (FlipWithPlayer candidate in toFlips)
				candidate.Flip(Agent.LocalContext.RightFacing);
		}
	}
}