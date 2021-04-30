using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;
using Thuleanx.Mechanics.Danmaku;
using Thuleanx.Math;

namespace FMOD_Thuleanx {
	public class LevelAudioManager : MonoBehaviour {
		[SerializeField, FMODUnity.EventRef] string SongRef;
		[SerializeField, FMODUnity.EventRef] string AnnouncerRef;
		[SerializeField] TMP_Text Announcer;
		[SerializeField] float waitPerAnnouncement = 1f;
		[SerializeField] float waitForTrackTitle = 4f;
		public UnityEvent OnLevelFinish;
		AudioTrack track;
		public GameObject trackTitle;

		void Start() {
			track = FMOD_Thuleanx.AudioManager.Instance.GetTrack(SongRef);
			Announcer.gameObject.SetActive(false);
			trackTitle.SetActive(false);

			StartCoroutine(StartCountdown());
		}

		void Update() {
			if (track.GetTrackDuration() <= track.GetTrackTimeMS()) {
				// track has finished. You beat the level yay
				OnLevelFinish?.Invoke();
			}
		}

		IEnumerator StartCountdown() {
			Timer sleep = new Timer(waitPerAnnouncement);

			Announcer.gameObject.SetActive(true);
			sleep.Start();
			Announcer.text = "Ready";
			AudioManager.Instance.PlayOneShot(AnnouncerRef);
			while (sleep) yield return null;
			sleep.Start();
			Announcer.text = "GO";
			AudioManager.Instance.PlayOneShot(AnnouncerRef);
			while (sleep) yield return null;
			Announcer.gameObject.SetActive(false);

			Play();

			trackTitle.SetActive(true);
			sleep = new Timer(waitForTrackTitle);
			sleep.Start();
			while (sleep) yield return null;
			trackTitle.SetActive(false);
		}

		void Play() {
			track.Play();
			BulletInstructionParser.StartParsing(track);
			FMOD_Thuleanx.AudioManager.Instance.SetMainTrack(track);
		}
		void Stop() {
			track.Stop(); 
			BulletInstructionParser.StopParsing(track);
			FMOD_Thuleanx.AudioManager.Instance.RemoveMainTrack(track);
			foreach (var obj in FindObjectsOfType<OnSongStop>())
				obj.OnSongStopEvent?.Invoke();
		}

		public void OnDeath() => Stop();
		public void OnRespawn() => Play();
		void OnDisable() => Stop();
	}
}