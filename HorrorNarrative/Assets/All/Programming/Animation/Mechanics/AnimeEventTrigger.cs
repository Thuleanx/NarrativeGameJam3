using UnityEngine;
using UnityEngine.Events;

namespace Thuleanx.Animation {
	public class AnimeEventTrigger : MonoBehaviour {
		public UnityEvent triggerOnCall;

		public void TriggerEvent() {
			triggerOnCall?.Invoke();
		}
	}
}