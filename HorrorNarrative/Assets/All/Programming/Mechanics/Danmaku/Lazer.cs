using UnityEngine;

namespace Thuleanx.Mechanics.Danmaku {
	public class Lazer : MonoBehaviour {
		public string LazerTrigger = "Fire";
		public Animator Anim {get; private set; }

		void Awake() {
			Anim = GetComponent<Animator>();
		}

		public void Detonate() {
			Anim?.SetTrigger(LazerTrigger);
		}
	}
}