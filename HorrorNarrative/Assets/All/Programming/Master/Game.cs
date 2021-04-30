using UnityEngine;
using System;
using System.Collections;
using Thuleanx.AI;
using Thuleanx.AI.Context;
using Thuleanx.Math;
using TMPro;
using FMOD_Thuleanx;

namespace Thuleanx.Master {
	public class Game : MonoBehaviour {
		public static Game Instance = null;

		[SerializeField] public GlobalContext Context;
		// [SerializeField] public int Death_Countdown_Seconds = 3;
		// [SerializeField] TMP_Text AnnouncerText;
		// [SerializeField, FMODUnity.EventRef] string AnnouncerSound;

		void Awake() {
			Instance = this;
			// AnnouncerText.gameObject.SetActive(false);
		}
		public void Sleep(int miliseconds) {
			StartCoroutine(SleepCoro(miliseconds));
		}
		IEnumerator SleepCoro(int miliseconds) {
			Time.timeScale = 0;

			TimerUnchained sleeping = new TimerUnchained(miliseconds / 1000f);
			sleeping.Start();
			while (sleeping) {
				yield return null;
			}
			Time.timeScale = 1;
		}
		public void StartRepsawn() {
			// StartCoroutine(Respawn());
		}
		// IEnumerator Respawn() {
		// 	int SecondsLeft = Death_Countdown_Seconds;

		// 	AnnouncerText.text = "";
		// 	AnnouncerText.gameObject.SetActive(true);

		// 	while (SecondsLeft --> 0) {
		// 		// Display Number
		// 		AnnouncerText.text = (SecondsLeft+1).ToString();
		// 		AudioManager.Instance.PlayOneShot(AnnouncerSound);

		// 		Timer sleeping = new Timer(1f);
		// 		sleeping.Start();
		// 		while (sleeping)
		// 			yield return null;
		// 	}

		// 	AnnouncerText.text = "";
		// 	AnnouncerText.gameObject.SetActive(false);

		// 	// Respawn
		// 	GameObject.FindWithTag("Player")?.GetComponent<Agent>()?.OnRespawn();
		// }
	}
}