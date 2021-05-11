using UnityEngine;
using Yarn.Unity;

namespace Thuleanx.Dialogue {
	public class DialogueManager : MonoBehaviour {
		[SerializeField] Yarn_Thuleanx.Thuleanx_InMemoryVariableStorage Storage;
		[SerializeField] Yarn_Thuleanx.Thuleanx_Dialogue_UI Dialogue_UI;
		[SerializeField] DialogueRunner runner;

		public void StartDialogue(string NodeName) => runner.StartDialogue(NodeName);
		public void NextDialogue() => Dialogue_UI.MarkLineComplete();
	}
}