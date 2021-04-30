using UnityEngine;
using Thuleanx.Math;

namespace Thuleanx.Behaviour {
	public class Transcience : MonoBehaviour {
		[SerializeField] float Duration = 4f;
		[SerializeField] Timer LifeLeft;

		void OnEnable() {
			LifeLeft = new Timer(Duration);
			LifeLeft.Start();
		}

		void Update() {
			if (!LifeLeft) gameObject.SetActive(false);
		}
	}
}