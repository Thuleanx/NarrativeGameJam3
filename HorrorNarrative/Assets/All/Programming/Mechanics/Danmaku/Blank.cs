using UnityEngine;
using Thuleanx.Utility;
using Thuleanx.Math;
using Thuleanx.Cinematography;
using UnityEngine.Events;

namespace Thuleanx.Mechanics.Danmaku {
	public class Blank : MonoBehaviour {
		[SerializeField] LayerMask bulletMask;
		[SerializeField] float BlankStart = 3f;
		[SerializeField] float BlankSpeed = 3f;
		[SerializeField] float Duration = 1f;

		Timer stillAlive;
		float startTime;
		float blankProgress;

		public bool Active {get; private set;}

		public void Activate() {
			Active = true;
			startTime = Time.time;

			PostProcessingUnit.Instance.StartShockwave(transform.position);

			stillAlive = new Timer(Duration);
			stillAlive.Start();
		}

		public void Deactivate() {
			stillAlive.Stop();
			Active = false;
			gameObject.SetActive(false);
		}

		void Update() {
			if (Active) {
				if (!stillAlive) Deactivate();
				else {
					// do smth, not really sure yet
					float progress = BlankStart + BlankSpeed * (Time.time - startTime);
					foreach (var collider in Physics2D.OverlapCircleAll(transform.position, progress, bulletMask)) {
						Bullet bullet = collider.gameObject.GetComponent<Bullet>();
						bullet.Deactivate();
					}
				}
			}
		}
	}
}