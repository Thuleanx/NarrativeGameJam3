using UnityEngine;

namespace Thuleanx.Mechanics.Danmaku { 
	public class LevelBooter : MonoBehaviour {
		[SerializeField, FMODUnity.EventRef] string SongName;
		FMOD_Thuleanx.AudioTrack track;
		public bool setMainTrack = true;

		void Start() {
			track = App.Instance._AudioManager.GetTrack(SongName);
			BulletInstructionParser.StartParsing(track);
			track.Play();
			if (setMainTrack) App.Instance._AudioManager.SetMainTrack(track);
		}

		void OnDisable() {
			BulletInstructionParser.StopParsing(track);
			track.Stop();
			// track.Dispose();
		}
	}
}