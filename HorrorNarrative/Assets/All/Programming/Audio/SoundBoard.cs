using UnityEngine;
using FMOD_Thuleanx;
using System.Collections.Generic;

namespace Thuleanx.Audio {
	public class SoundBoard : MonoBehaviour {
		[FMODUnity.EventRef] public List<string> Event = new List<string>();

		public void PlaySound(int id) {
			App.Instance._AudioManager.PlayOneShot(Event[id]);
		}

		public void PlaySound3D(int id) {
			App.Instance._AudioManager.PlayOneShot3D(Event[id], transform.position);
		}
	}
}