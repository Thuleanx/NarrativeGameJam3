using UnityEngine;

namespace Thuleanx.Mechanics.Mapping {
	[RequireComponent(typeof(Collider2D))]
	public class Anchor : MonoBehaviour {
		public Passage passage;

		public void OnTriggerEnter2D(Collider2D other) {
			if (other.tag == "Player") {
				if (passage != null) {
					App.Instance.StartCoroutine(App.Instance._GameModeManager._current_mode.TransitionThroughPassage(passage));
				}
			}
		}
	}
}