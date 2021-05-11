using UnityEngine;
using Thuleanx.Utility;
using Thuleanx.Animation;
using Thuleanx.AI.Context;

namespace Thuleanx.AI {
	public class PlayerAnimatorInfoTracker : MonoBehaviour {
		Agent Agent;

		void Awake() {
			Agent = GetComponentInParent<Agent>();
		}

		void Update() {
			Agent.Anim.SetFloat("VelocityX", Agent.LocalContext.Velocity.x);
			Agent.Anim.SetFloat("VelocityY", Agent.LocalContext.Velocity.y);
			Agent.Anim.SetFloat("Equipment", (float) ((PlayerLocalContext) Agent.LocalContext).Equipment);
			Agent.Anim.SetFloat("Speed", Agent.LocalContext.Velocity.magnitude);
			Agent.Anim.SetFloat("Loaded", ((Context.PlayerLocalContext) Agent.LocalContext).GunLoaded ? 1 : 0);
		}
	}
}