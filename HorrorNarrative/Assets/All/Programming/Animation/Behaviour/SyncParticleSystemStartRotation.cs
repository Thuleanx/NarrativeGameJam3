using UnityEngine;

namespace Thuleanx.Animation {
	public class SyncParticleSystemStartRotation : MonoBehaviour {
		ParticleSystem System;

		private void Awake() {
			System = GetComponent<ParticleSystem>();
		}

		private void Update() {
			var main = System.main;
			main.startRotation = - transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
		}
	}
}