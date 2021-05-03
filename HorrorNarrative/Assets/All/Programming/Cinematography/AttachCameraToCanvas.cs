using UnityEngine;

namespace Thuleanx.Cinematography {
	[RequireComponent(typeof(Canvas))]
	public class AttachCameraToCanvas : MonoBehaviour {
		Canvas canvas;

		private void Awake() {
			canvas = GetComponent<Canvas>();
		}

		private void Update() {
			if (canvas.worldCamera == null)
				canvas.worldCamera = Camera.main;
		}
	}
}