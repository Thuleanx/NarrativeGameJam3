using UnityEngine;
using Thuleanx;

namespace FMOD_Thuleanx {
	public class AudioEventEmiter : MonoBehaviour {
		[FMODUnity.EventRef] public string Event;

		public void PlayOneShot() {
			App.Instance._AudioManager.PlayOneShot(Event);
		}
	}
}