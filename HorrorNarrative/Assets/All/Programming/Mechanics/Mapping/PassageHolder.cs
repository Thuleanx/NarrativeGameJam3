using UnityEngine;

namespace Thuleanx.Mechanics.Mapping {
	public class PassageHolder : MonoBehaviour {
		[SerializeField]
		Passage passage;

		public void Transition() {
			if (passage != null)
				SceneTransitionManager.TransitionSingle(passage);
		}
	}
}