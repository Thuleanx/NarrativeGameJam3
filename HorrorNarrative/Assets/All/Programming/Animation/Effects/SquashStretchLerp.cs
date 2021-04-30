using UnityEngine;
using Thuleanx.Math;
using Thuleanx.Utility;

namespace Thuleanx.Animation.Effect {
	public class SquashStretchLerp : MonoBehaviour {
		public float Lambda = 4;
		public Optional<Vector2> DefaultScale;

		void Awake() {
			if (!DefaultScale.Enabled)
				DefaultScale = new Optional<Vector2>(transform.localScale);
		}

		void OnEnable() {
			// if (DefaultScale.Enabled)
			// 	transform.localScale = (Vector3) DefaultScale.Value + Vector3.forward * transform.localScale.z;
		}

		void Update() {
			if ((Vector2) transform.localScale != DefaultScale.Value) {
				transform.localScale = 
					(Vector3) Calc.Damp((Vector2) transform.localScale, DefaultScale.Value, Lambda, Time.deltaTime) + 
					Vector3.forward * transform.localScale.z;
			}
		}

		public void SetScaleX(float value) {
			Vector3 scale = transform.localScale;
			scale.x = value;
			transform.localScale = scale;
		}

		public void SetScaleY(float value) {
			Vector3 scale = transform.localScale;
			scale.y = value;
			transform.localScale = scale;
		}
	}
}