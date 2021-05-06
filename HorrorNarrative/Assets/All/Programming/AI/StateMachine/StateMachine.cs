using UnityEngine;
using System;
using UnityEngine.Assertions;

namespace Thuleanx.AI {
	[CreateAssetMenu(fileName = "StateMachine", menuName = "~/StateMachine/StateMachine", order = 0)]
	public class StateMachine : ScriptableObject {
		[HideInInspector]
		public Agent Agent;
		public State Current  {get; private set; }
		public State Prev {get; private set;}

		[SerializeField] 
		State[] States;

		public StateMachine() {}

		public void Init() {
			Assert.IsTrue(States != null);
			foreach (State state in States)
				state.StateMachine = this;
			Reset();
		}
		public void Reset() { 
			Assert.IsTrue(States != null && States.Length > 0);
			Transition(States[0]); 
		}
		public void Transition(State state) {
			Current?.OnExit();
			Prev = Current;
			Current = state;
			Current.OnEnter();
		}

		public void OnUpdate() {
			Current?.OnUpdate();
		}

		public State FindStateOfType(Type type) {
			foreach (State state in States) {
				if (type.IsInstanceOfType(state)) 
					return state;
			}
			return null;
		}
	}
}