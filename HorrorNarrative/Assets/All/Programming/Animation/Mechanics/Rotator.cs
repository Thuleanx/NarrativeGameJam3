using UnityEngine;

namespace Thuleanx.Animation {
	public class Rotator : MonoBehaviour {
		public float RotateRate = 0;
		float original;

		private void Awake() {
			original = transform.localRotation.eulerAngles.z;
		}

		public void SetRotationRate(float Rate) {
			RotateRate = Rate;
		}

		public void SetRotation(float Rotation) {
			transform.localRotation = Quaternion.Euler(0f, 0f, Rotation);
		}

		public void StopRotating() => SetRotationRate(0f);
		public void ResetRotation() => SetRotation(original);

		void Update() {
			if (RotateRate != 0) {
				transform.Rotate(new Vector3(0f, 0f, RotateRate * Time.deltaTime), Space.Self);
			}
		}
	}
}