using UnityEngine;

namespace Thuleanx.AI {
	[CreateAssetMenu(fileName = "PlayerBlankShot", menuName = "~/StateMachine/Player/PlayerBlankShot", order = 0)]
	public class PlayerBlankShot : PlayerState {
		[SerializeField, FMODUnity.EventRef] string BlankShotSound;

		public override State ShouldTransitionTo() {
			if (AnimationFinish)
				return StateMachine.FindStateOfType(typeof(PlayerGrounded));
			return null;
		}

		public override void OnEnter() {
			base.OnEnter();
			App.Instance._AudioManager.PlayOneShot(BlankShotSound);
		}

		public override void OnExit() {
			base.OnExit();
		}

		public override bool CanEnter() => 
			PlayerLocalContext.Equipment == PlayerEquipment.Blunderbuss && !PlayerLocalContext.GunLoaded;

		public override State Clone() => Clone(CreateInstance<PlayerBlankShot>());
		public override State Clone(State state) {
			((PlayerBlankShot) state).BlankShotSound = BlankShotSound;
			return base.Clone(state);
		}
	}
}