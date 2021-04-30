using UnityEngine;
using UnityEngine.Events;

namespace Thuleanx.Animation {
	public class OnFinishTrigger : MonoBehaviour {
		public UnityEvent FinishAction;

		public void OnFinish() {
			FinishAction?.Invoke();
		}
	}
}