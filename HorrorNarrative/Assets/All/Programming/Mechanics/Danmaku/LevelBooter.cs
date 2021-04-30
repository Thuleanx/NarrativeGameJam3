using UnityEngine;

namespace Thuleanx.Mechanics.Danmaku { 
	public class LevelBooter : MonoBehaviour {
		[SerializeField, FMODUnity.EventRef] string SongName;
		FMOD_Thuleanx.AudioTrack track;
		public bool setMainTrack = true;

		void Start() {
			track = FMOD_Thuleanx.AudioManager.Instance.GetTrack(SongName);
			BulletInstructionParser.StartParsing(track);
			track.Play();
			if (setMainTrack) FMOD_Thuleanx.AudioManager.Instance.SetMainTrack(track);
		}

		void OnDisable() {
			BulletInstructionParser.StopParsing(track);
			track.Stop();
			// track.Dispose();
		}
	}
}