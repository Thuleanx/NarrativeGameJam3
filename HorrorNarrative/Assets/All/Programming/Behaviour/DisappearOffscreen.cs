using UnityEngine;
using Thuleanx.Utility;
using Thuleanx.Math;
using UnityEngine.Events;

namespace Thuleanx.Behaviour {
	public class DisappearOffscreen : MonoBehaviour {
		public int framesPerCheck = 10;
		public float Delay = 0f;
		int frames=0;

		bool Disappearing;
		Timer DelayTillDisable;
		public UnityEvent OnOffscreen;

		void OnEnable() {
			Disappearing = false;
		}

		void Update() {
			if (!Disappearing && frames++ == framesPerCheck) {
				if (!General.InCamera(transform.position)) {
					Disappearing = true;
					DelayTillDisable = new Timer(Delay);
					DelayTillDisable.Start();
					OnOffscreen?.Invoke();
				}
				frames=0;
			}
			if (Disappearing && !DelayTillDisable)
				gameObject.SetActive(false);
		}
	}
}