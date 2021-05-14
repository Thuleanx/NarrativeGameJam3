using UnityEngine;
using UnityEngine.Events;

namespace Thuleanx.Animation {
	public class OnEnableEvent : MonoBehaviour {
		[SerializeField]
		UnityEvent Event;

		void OnEnable() {
			Event?.Invoke();
		}
	}
}