using UnityEngine;
using TMPro;
using Yarn;
using Thuleanx.Optimization;
using UnityEngine.UI;

namespace Thuleanx.Dialogue {
	public class DialogueText : MonoBehaviour {
		TMP_Text textMesh;
		public Speaker speaker {get; private set; }
		RectTransform rectTransform;

		private void Awake() {
			textMesh = GetComponentInChildren<TMP_Text>();
		}

		public void SetText(string text) {
			textMesh.text = text;
		}

		public void SetText(Yarn.Value text) {
			LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
			textMesh.text = text.AsString;
		}

		public void SetSpeaker(Speaker speaker) {
			this.speaker = speaker;
			Reposition();
		}

		void Update() {
			Reposition();
		}

		public void Reposition() {
			if (speaker != null) {
				transform.localPosition = (Vector2) speaker.transform.position + speaker.SpeechBubbleDisplacement;
			}
		}

		public void Disable() {
			speaker = null;
			gameObject.SetActive(false);
		}

		void OnDisable() {
			speaker = null;
		}
	}
}