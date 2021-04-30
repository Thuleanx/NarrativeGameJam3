using UnityEngine;
using UnityEngine.Events;

namespace Thuleanx.Animation {
	public class OnActionTrigger : MonoBehaviour {
		public UnityEvent Action;

		public void OnFinish() {
			Action?.Invoke();
		}
	}
}