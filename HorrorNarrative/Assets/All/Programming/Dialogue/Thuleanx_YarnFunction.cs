using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Yarn;
using Yarn.Unity;

namespace Yarn_Thuleanx {
	public class Thuleanx_YarnFunction {
		public static void RegisterFunction(string name, int paramNumber, ReturningFunction function) {
			foreach (var runner in GameObject.FindObjectsOfType<DialogueRunner>())
				runner.Dialogue.library.RegisterFunction(name, paramNumber, function);
		}
	}
}