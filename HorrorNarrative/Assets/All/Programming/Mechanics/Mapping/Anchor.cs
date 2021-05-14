using UnityEngine;

namespace Thuleanx.Mechanics.Mapping {
	public class Anchor : MonoBehaviour {
		public Passage passage;

		public void OnTriggerEnter2D(Collider2D other) {
			if (other.tag == "Player") {
				if (passage != null) {
					App.Instance.StartCoroutine(App.Instance._GameModeManager._current_mode.TransitionThroughPassage(passage));
				}
			}
		}

		public static Anchor FindCorrespondence(Passage passage) {
			Anchor targetAnchor = GameObject.FindObjectOfType<Anchor>();
			if (passage == null) return targetAnchor;
			foreach (Anchor potentialTarget in GameObject.FindObjectsOfType<Anchor>()) {
				if (potentialTarget.passage == passage.target_passage)
					targetAnchor = potentialTarget;
			}
			return targetAnchor;
		}
	}
}