using UnityEngine;
using Thuleanx.AI.Context;

namespace Thuleanx.AI {
	[System.Serializable]
	public class State : ScriptableObject {
		[HideInInspector]
		public StateMachine StateMachine;

		public virtual void OnEnter() {}
		public virtual void OnExit() {}
		public virtual void OnUpdate() {}
		public virtual void OnContextUpdate() {}

		public virtual State ShouldTransitionTo() => null;
		public virtual bool CanEnter() => true;
		
		public virtual void OnFinishTrigger() {}
		public virtual void OnAnimationFinishTrigger(int action) {}


		public Agent Agent => StateMachine.Agent;
	}
}