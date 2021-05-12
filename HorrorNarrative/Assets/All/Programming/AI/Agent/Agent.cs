
using UnityEngine;
using UnityEngine.Events;
using Thuleanx.AI.Context;
using Thuleanx.Utility;
using System.Collections.Generic;
using Thuleanx.Animation;
using FMOD_Thuleanx;

namespace Thuleanx.AI {
	public class Agent : MonoBehaviour {
		public AgentContext Context;
		public Optional<StateMachineCore> MachineCore;
		[HideInInspector]
		public Optional<StateMachine> Machine;
		public UnityEvent OnDeathEvent;
		public UnityEvent OnDamageTakenEvent;
		public UnityEvent OnRespawnEvent;

		[HideInInspector]
		public AgentLocalContext LocalContext;
		[HideInInspector]
		public PhysicsObject PhysicsBody;

		public Animator Anim {get; private set; }


		public virtual void Awake() {
			if (MachineCore.Enabled) {
				Machine = new Optional<StateMachine>(new StateMachine(MachineCore.Value));
				Machine.Value.Agent = this;
			}
			LocalContext = new AgentLocalContext(this);
			PhysicsBody = GetComponent<PhysicsObject>();
			Anim = GetComponent<Animator>();
		}

		public virtual void Start() {
			if (Machine.Enabled) Machine.Value.Init();
		}

		public virtual void Update() {
			if (Machine.Enabled) {
				State NxtState;
				while ((NxtState = Machine.Value.Current.ShouldTransitionTo()) != null)
					Machine.Value.Transition(NxtState);
				Machine.Value.OnUpdate();
			}
		}

		public void GiveIFrame(float duration) {
			if (!LocalContext.IFrame || duration > LocalContext.IFrame.TimeLeft) {
				LocalContext.IFrame = new Math.Timer(duration);
				LocalContext.IFrame.Start();
			}
		}

		public KeyValuePair<bool,int> TakeHit(int damage) {
			if (IsDead()) return new KeyValuePair<bool, int>(false, 0);
			if (damage < 0) Debug.LogWarning("Damage taken by " + this + " is negative");

			if (!LocalContext.IFrame && damage >= 0) {
				int taken = Mathf.Min(LocalContext.Health, damage);
				LocalContext.TakeDamage(taken);
				// LocalContext.IFrame = new Math.Timer(Context.IFrameAfterHit);
				// LocalContext.IFrame.Start();
				// LocalContext.Damaged= new Math.Timer(Context.IFrameAfterHit);
				// LocalContext.Damaged.Start();

				OnDamageTaken();
				if (LocalContext.Health == 0) OnDeath();

				return new KeyValuePair<bool, int>(true, taken);
			}
			return new KeyValuePair<bool,int>(false, 0);
		}

		public void Heal(int value) {
			LocalContext.Heal(value);
		}

		public bool IsDead() => LocalContext.Health == 0;

		public virtual void OnRespawn() {
			OnRespawnEvent?.Invoke();
		}

		public virtual void OnDeath() {
			OnDeathEvent?.Invoke();
		}

		public virtual void OnDamageTaken() {
			OnDamageTakenEvent?.Invoke();
		}
	}
}