using UnityEngine;

namespace Thuleanx.Dialogue {
	public class Speaker : MonoBehaviour {
		[SerializeField] public Vector2 SpeechBubbleDisplacement = Vector2.zero;

		private void OnEnable() {
			SpeakerAuthority.Instance.Add(this);
		}
		private void OnDisable() {
			SpeakerAuthority.Instance.Remove(this);
		}
	}
}