using UnityEngine;

namespace Thuleanx.Dialogue {
	public class Speaker : MonoBehaviour {
		[SerializeField] public Vector2 SpeechBubbleDisplacement = Vector2.zero;




		public static Speaker GetSpeaker(string name) {
			foreach (Speaker speaker in GameObject.FindObjectsOfType<Speaker>()) if (speaker.name == name) {
				return speaker;
			}
			return null;
		}
	}
}