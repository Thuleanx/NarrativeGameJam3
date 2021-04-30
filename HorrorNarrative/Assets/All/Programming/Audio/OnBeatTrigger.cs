using UnityEngine;
using UnityEngine.Events;

namespace FMOD_Thuleanx {
	public class OnBeatTrigger : MonoBehaviour {
		[SerializeField] UnityEvent trigger;

		public virtual void OnBeat() { trigger?.Invoke(); }
	}
}