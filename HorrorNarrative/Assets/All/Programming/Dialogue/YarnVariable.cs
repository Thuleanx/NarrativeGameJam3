using Yarn;
using UnityEngine;

namespace Yarn_Thuleanx {
	[System.Serializable]
	public struct YarnVariable {
		public string name;
		public string value;
		public Value.Type type;

		public static Yarn.Value Evaluate(YarnVariable variable) {
			object value;

			switch (variable.type) {
				case Yarn.Value.Type.Number:
					float f = 0.0f;
					float.TryParse(variable.value, out f);
					value = f;
					break;

				case Yarn.Value.Type.String:
					value = variable.value;
					break;

				case Yarn.Value.Type.Bool:
					bool b = false;
					bool.TryParse(variable.value, out b);
					value = b;
					break;

				case Yarn.Value.Type.Variable:
					// We don't support assigning default variables from
					// other variables yet
					value = null;
					Debug.LogErrorFormat("Can't set variable {0} to {1}: You can't " +
						"set a default variable to be another variable, because it " +
						"may not have been initialised yet.", variable.name, variable.value);
					break;

				case Yarn.Value.Type.Null:
					value = null;
					break;

				default:
					throw new System.ArgumentOutOfRangeException();

			}

			var v = new Yarn.Value(value);

			return v;
		}
	}
}