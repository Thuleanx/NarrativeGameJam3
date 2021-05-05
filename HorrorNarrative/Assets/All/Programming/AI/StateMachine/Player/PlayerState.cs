using UnityEngine;
using Thuleanx.Utility;

namespace Thuleanx.AI {
	public class PlayerState : State {
		public bool CanFlip;
		public Optional<string> AnimationState;
		protected Player PlayerAgent { get { return (Player) Agent; } }

		public override void OnEnter() {
			base.OnEnter();
			if (AnimationState.Enabled) Agent.Anim.SetBool(AnimationState.Value, true);
		}

		public override void OnExit() {
			if (AnimationState.Enabled) Agent.Anim.SetBool(AnimationState.Value, false);
			base.OnExit();
		}
	}
}