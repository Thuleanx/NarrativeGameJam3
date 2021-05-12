using UnityEngine;
using Yarn_Thuleanx;

namespace Thuleanx.Dialogue {
	public class DialogueSetter : MonoBehaviour {
		[SerializeField] YarnVariable Var;

		public void Set() => App.Instance._DialogueManager.Storage.SetValue(Var.name, YarnVariable.Evaluate(Var));
	}
}