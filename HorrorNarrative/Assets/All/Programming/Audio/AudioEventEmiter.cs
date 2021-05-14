using UnityEngine;
using Thuleanx;

namespace FMOD_Thuleanx {
	public class AudioEventEmiter : MonoBehaviour {
		[FMODUnity.EventRef] public string Event;
		FMOD.Studio.EventInstance instance;

		public void PlayOneShot() {
			App.Instance._AudioManager.PlayOneShot(Event);
		}

		public void PlayOneShotAttached() {
			instance = App.Instance._AudioManager.GetInstance(Event);
			FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, transform, GetComponent<Rigidbody2D>());
			instance.start();
		}

		private void OnDisable() {
			instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		}
	}
}