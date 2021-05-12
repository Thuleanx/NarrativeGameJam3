using UnityEngine;
using System;
using UnityEngine.Assertions;

namespace Thuleanx.AI {
	public class StateMachine {
		[HideInInspector]
		public Agent Agent;
		// Useful only for the OnExit function
		public State Next {get; private set; }
		public State Current {get; private set; }
		public State Prev {get; private set;}

		[SerializeField] 
		State[] States;

		public StateMachine(StateMachineCore Core) {
			States = new State[Core.States.Length];
			for (int i = 0; i < Core.States.Length; i++)
				States[i] = Core.States[i].Clone();
		}

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
			Next = state;
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