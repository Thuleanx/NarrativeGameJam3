using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Thuleanx.Animation {
	public class AnimeEventTriggerMultiple : MonoBehaviour {
		public List<UnityEvent<int>> list = new List<UnityEvent<int>>();

		public void TriggerEvent(int num) {
			if (list.Count > num)
				list[num]?.Invoke(num);
		}
	}
}