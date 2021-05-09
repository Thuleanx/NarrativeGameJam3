using UnityEngine;
using Thuleanx.Utility;
using Thuleanx.AI.Context;

namespace Thuleanx.AI {
	public class MonsterState : State {
		[HideInInspector] public bool AnimationFinish;
		public bool CanFlip;
		public Optional<string> AnimationState;

		protected Mob MobAgent {get {return (Mob) Agent; }}

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