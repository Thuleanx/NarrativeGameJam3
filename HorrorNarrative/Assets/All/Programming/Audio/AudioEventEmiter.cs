using UnityEngine;

namespace FMOD_Thuleanx {
	public class AudioEventEmiter : MonoBehaviour {
		[FMODUnity.EventRef] public string Event;

		public void PlayOneShot() {
			AudioManager.Instance.PlayOneShot(Event);
		}
	}
}