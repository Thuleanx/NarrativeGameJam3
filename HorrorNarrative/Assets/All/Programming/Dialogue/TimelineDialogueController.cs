using UnityEngine;

namespace Thuleanx.Dialogue {
	public class TimelineDialogueController : MonoBehaviour {
		public void StartDialogue(string NodeName) => App.Instance._DialogueManager.StartDialogue(NodeName);
		public void NextDialogue() => App.Instance._DialogueManager.NextDialogue();
	}
}