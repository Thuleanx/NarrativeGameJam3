using UnityEngine;
using Yarn.Unity;

namespace Thuleanx.Dialogue {
	public class DialogueManager : MonoBehaviour {
		[SerializeField] public Yarn_Thuleanx.Thuleanx_InMemoryVariableStorage Storage;
		[SerializeField] public Yarn_Thuleanx.Thuleanx_Dialogue_UI Dialogue_UI;
		[SerializeField] public DialogueRunner runner;
		[SerializeField] public DialogueRunner runner_async;

		public void StartDialogue(string NodeName) => runner.StartDialogue(NodeName);
		public void StartDialogue_Async(string NodeName) => runner_async.StartDialogue(NodeName);
		public void NextDialogue() => Dialogue_UI.MarkLineComplete();
	}
}