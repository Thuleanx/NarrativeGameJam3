using UnityEngine;
using Thuleanx.Utility;

namespace Thuleanx.Dialogue {
	public class DialogueInitializer : MonoBehaviour {
		[SerializeField] Optional<string> Node;

		void Awake() {
			if (!Node.Enabled) Node = new Optional<string>(GetComponentInParent<Speaker>().name);
		}

		public void Play() {
			App.Instance._DialogueManager.StartDialogue(Node.Value);
		}
	}
}