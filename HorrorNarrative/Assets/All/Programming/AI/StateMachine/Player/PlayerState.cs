using UnityEngine;
using Thuleanx.Utility;
using Thuleanx.AI.Context;

namespace Thuleanx.AI {
	public class PlayerState : State {
		[HideInInspector]
		public bool AnimationFinish;
		public bool CanFlip;
		public Optional<string> AnimationState;
		protected Player PlayerAgent { get { return (Player) Agent; } }
		protected PlayerLocalContext PlayerLocalContext { get { return (PlayerLocalContext) PlayerAgent.LocalContext; } }
		protected PlayerContext PlayerContext { get { return (PlayerContext) PlayerAgent.Context; } }

		public override void OnEnter() {
			base.OnEnter();
			if (AnimationState.Enabled) Agent.Anim.SetBool(AnimationState.Value, true);
			AnimationFinish = false;
		}

		public override void OnExit() {
			if (AnimationState.Enabled) Agent.Anim.SetBool(AnimationState.Value, false);
			base.OnExit();
		}
	}
}