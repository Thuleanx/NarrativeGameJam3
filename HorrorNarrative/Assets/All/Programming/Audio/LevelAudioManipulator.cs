using UnityEngine;

namespace FMOD_Thuleanx {
	public class LevelAudioManipulator : MonoBehaviour {
		public void OnDeath() {
			GameObject.FindObjectOfType<LevelAudioManager>()?.OnDeath();
		}
		public void OnRespawn() {
			GameObject.FindObjectOfType<LevelAudioManager>()?.OnRespawn();
		}
	}
}