using UnityEngine;
using Thuleanx.Master;

namespace Thuleanx.Dialogue {
	public class DialogueSkipper : MonoBehaviour {
		private void Update() {
			if (App.Instance._InputManager.SkipDialogue) {
				App.Instance._InputManager.UseSkipDialogueInput();
				App.Instance._DialogueManager.NextDialogue();
			}
		}
	}
}