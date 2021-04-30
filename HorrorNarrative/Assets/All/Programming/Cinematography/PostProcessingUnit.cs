using UnityEngine;
using Thuleanx.Utility;
using Thuleanx.Math;

namespace Thuleanx.Cinematography {
	public class PostProcessingUnit : MonoBehaviour {
		public static PostProcessingUnit Instance;

		[SerializeField] Material PostProcessingMaterial;
		[SerializeField] float ChromaOffsetLambda = 4f;
		[SerializeField] float glitchStrength = .02f;

		float currentChromaOffset = 0f;

		[SerializeField] string _blank_active = "_active";
		[SerializeField] string _focal_point = "_focal_point";
		[SerializeField] string _time_offset = "_time_offset";
		[SerializeField] string _glitch_strength = "_glitch_strength";
		[SerializeField] string _chroma_offset = "_chroma_offset";

		void Awake() {
			PostProcessingMaterial.SetFloat(_blank_active, 0f);
			PostProcessingMaterial.SetFloat(_glitch_strength, 0f);
			PostProcessingMaterial.SetFloat(_chroma_offset, 0f);
			Instance = this;
		}

		public void StartShockwave(Vector2 position) {
			PostProcessingMaterial.SetFloat(_blank_active, 1f);
			PostProcessingMaterial.SetFloat(_time_offset, Time.time);
			PostProcessingMaterial.SetVector(_focal_point, General.ToViewportSpace(position));
		}
		public void StartGlitch() {
			PostProcessingMaterial.SetFloat(_glitch_strength, glitchStrength);
		}
		public void StopGlitch() {
			PostProcessingMaterial.SetFloat(_glitch_strength, 0f);
		}
		public void ChromaOffset(float amt) {
			currentChromaOffset = amt;
		}
		void Update() {
			PostProcessingMaterial.SetFloat(_chroma_offset, currentChromaOffset);
			if (currentChromaOffset !=  0) currentChromaOffset = Calc.Damp(currentChromaOffset, 0f, ChromaOffsetLambda, Time.deltaTime);
		}
	}
}