using UnityEngine;
using Thuleanx.Utility;

namespace Thuleanx.Dialogue {
	public class DialogueInitializer : MonoBehaviour {
		[SerializeField] bool async = false;
		[SerializeField] Optional<string> Node;

		void Awake() {
			if (!Node.Enabled) Node = new Optional<string>(GetComponentInParent<Speaker>().name);
		}

		public void Play() {
			Debug.Log("TRYING TO START: " + Node.Value);
			if (async) App.Instance._DialogueManager.StartDialogue_Async(Node.Value);
			else App.Instance._DialogueManager.StartDialogue(Node.Value);
		}
	}
}