using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Yarn;
using Yarn.Unity;

namespace Yarn_Thuleanx {
	public class Thuleanx_InMemoryVariableStorage : Yarn.Unity.VariableStorageBehaviour {
		public static Thuleanx_InMemoryVariableStorage Instance { get; private set; }

		private Dictionary<string, Yarn.Value> variables = new Dictionary<string, Yarn.Value>();

		public YarnVariable[] DefaultValues;
		[SerializeField]
		internal UnityEngine.UI.Text debugTextView = null;

		public override void SetValue(string variableName, Value value) {
			variables[variableName] = value;
		}
		public override Value GetValue(string variableName) {
			if (variables.ContainsKey(variableName) == false)
				return Yarn.Value.NULL;

			return variables[variableName];
		}


		public override void ResetToDefaults() {
			Clear();

			foreach (YarnVariable variable in DefaultValues)
				SetValue("$" + variable.name, YarnVariable.Evaluate(variable));
		}

		public override void Clear() {

			variables.Clear();
		}
		public static string NodeFormat(string name) => $"visited_{name}";

		void Awake() {
			Instance = this;
			ResetToDefaults();

			Thuleanx_YarnFunction.RegisterFunction("visited", 1, Params => {
				return GetValue(NodeFormat(Params[0].AsString)).AsBool;
			});
		}

		public void RegisterNode(string name) {
			SetValue(NodeFormat(name), true);
		}

		/// If we have a debug view, show the list of all variables in it
		internal void Update() {
			if (debugTextView != null) {
				var stringBuilder = new System.Text.StringBuilder();
				foreach (KeyValuePair<string, Yarn.Value> item in variables) {
					string debugDescription;
					switch (item.Value.type) {
						case Value.Type.Bool:
							debugDescription = item.Value.AsBool.ToString();
							break;
						case Value.Type.Null:
							debugDescription = "null";
							break;
						case Value.Type.Number:
							debugDescription = item.Value.AsNumber.ToString();
							break;
						case Value.Type.String:
							debugDescription = $@"""{item.Value.AsString}""";
							break;
						default:
							debugDescription = "<unknown>";
							break;

					}
					stringBuilder.AppendLine(string.Format("{0} = {1}",
															item.Key,
										 debugDescription));
				}
				debugTextView.text = stringBuilder.ToString();
				debugTextView.SetAllDirty();
			}
		}
	}
}