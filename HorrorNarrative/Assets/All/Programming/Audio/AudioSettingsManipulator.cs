using UnityEngine;
using Thuleanx;

namespace FMOD_Thuleanx {
	public class AudioSettingsManipulator : MonoBehaviour {
		public void SetMasterVolume(float value) {
			App.Instance._AudioManager?.SetMasterVolume(value);
		}

		public void SetMusicVolume(float value) {
			App.Instance._AudioManager?.SetMusicVolume(value);
		}

		public void SetSFXVolume(float value) {
			App.Instance._AudioManager?.SetSFXVolume(value);
		}
	}
}