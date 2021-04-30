using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thuleanx.Optimization {
	public class ObjectReturnToPool : MonoBehaviour
	{
		public string sourceTag;

		bool disabled;

		private void OnEnable() {
			disabled = false;
		}

		void OnDisable() {
			OnWillDisable();
		}

		public void OnWillDisable() {
			if (!disabled) {
				ObjectPool.Instance.Return(sourceTag, gameObject);
				disabled = true;
			}
		}
	}

}