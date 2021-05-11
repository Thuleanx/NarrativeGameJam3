using UnityEngine;
using Yarn;
using Yarn.Unity;

namespace Thuleanx.Mechanics.Mapping {
	public class PassageHolder : MonoBehaviour {
		[SerializeField] Passage passage;

		[YarnCommand("Transition")]
		public void Transition() {
			if (passage != null) {
				App.Instance.StartCoroutine(
					App.Instance._GameModeManager._current_mode.TransitionThroughPassage(passage));
			}
		}
	}
}