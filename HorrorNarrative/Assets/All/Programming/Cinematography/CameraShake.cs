using UnityEngine;
using Thuleanx.Math;
using Thuleanx.Animation;

namespace Thuleanx.Cinematography {
	public class CameraShake : Shake {
		public static CameraShake Instance {get; private set; }

		public override void Awake() {
			base.Awake();
			Instance = this;
		}
	}
}