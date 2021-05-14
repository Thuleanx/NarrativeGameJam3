using UnityEngine;
using Thuleanx.Controls;
using Thuleanx.AI.Context;
using Thuleanx.Master.Global;
using Yarn.Unity;

namespace Thuleanx.AI {
	public class Player : Agent {
		public bool CanControl {get; private set; }

		public static Player Instance;

		public override void Awake() {
			base.Awake();
			CanControl = true;
			Instance = this;
			LocalContext = new Context.PlayerLocalContext(this);
		}

		public void ForcePlayerState(PlayerState State) {
			if (State != null)
				Machine.Value.Transition(State);
		}

		public void UnHalt() {
			Machine.Value.Reset();
		}

		public void ResetToDefaultPose() {
			Machine.Value.Reset();
		}

		public void Halt() => ForcePlayerState((PlayerState) Machine.Value.FindStateOfType(typeof(PlayerHalt)));

		public override void Update() {
			base.Update();

			if (Machine.Enabled && ((PlayerState) Machine.Value.Current).CanFlip)
				if (App.Instance._InputManager.Movement.x != 0)
					LocalContext.RightFacing = App.Instance._InputManager.Movement.x > 0;
		}

		public override void OnDeath() {
			base.OnDeath();
			ForcePlayerState(Machine.Value.FindStateOfType(typeof(PlayerDead)) as PlayerState);
			((StoryMode) App.Instance._GameModeManager._current_mode).Respawn();
		}

		public bool IsAiming() => typeof(PlayerAiming).IsInstanceOfType(Machine.Value.Current);
		public float AimingArc() => ((PlayerLocalContext) LocalContext).aimArc;

		[YarnCommand("Equip")]
		public void Equip(string Equipment) {
			if (Equipment == "Blunderbuss") {
				((PlayerLocalContext) LocalContext).Equipment = PlayerEquipment.Blunderbuss;
			} else if (Equipment == "NONE") {
				((PlayerLocalContext) LocalContext).Equipment = PlayerEquipment.NONE;
			}
		}
	}
}