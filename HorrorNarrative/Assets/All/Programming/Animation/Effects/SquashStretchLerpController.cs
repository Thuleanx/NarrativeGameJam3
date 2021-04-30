using UnityEngine;
using Thuleanx.Math;

namespace Thuleanx.Animation.Effect {
	public class SquashStretchLerpController : MonoBehaviour {
		[SerializeField] SquashStretchLerp Device;

		public void SetScaleX(float value) => Device.SetScaleX(value);
		public void SetScaleY(float value) => Device.SetScaleY(value);
	}
}