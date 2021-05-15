using UnityEngine;
using Yarn.Unity;
using UnityEngine.Events;

namespace Thuleanx.Cinematography {
	public class CinematographyManager : MonoBehaviour {
		[SerializeField] UnityEvent<float> Shock;

		[YarnCommand("CameraShake")]
		public void Shake(string value) {
			float val = float.Parse(value);
			Shock?.Invoke(val);
		}
	}
}