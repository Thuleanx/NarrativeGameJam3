using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Thuleanx.Dialogue {
	public class ActiveOnlyIf : MonoBehaviour {
		[System.Serializable]
		public struct Clause {
			public string variable_name;
			public bool invert;
		}

		public List<Clause> clauses = new List<Clause>();

		void Update() {
			bool shouldActive = Value();			
			foreach (Transform child in transform)
				if (child.gameObject.activeSelf ^ shouldActive)
					child.gameObject.SetActive(shouldActive);
			
		}

		public bool Value() {
			bool value = true;
			foreach (Clause clause in clauses)
				value &= Yarn_Thuleanx.Thuleanx_InMemoryVariableStorage.Instance.GetValue(clause.variable_name).AsBool ^ clause.invert;
			return value;
		}
	}
}