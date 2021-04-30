using UnityEngine;

namespace Thuleanx.Cinematography {
	public class PostProcessingTrigger : MonoBehaviour {
		[SerializeField] float ChromaOffsetStrength = 0.2f;
		public void StartGlitch() => PostProcessingUnit.Instance.StartGlitch();
		public void StopGlitch() => PostProcessingUnit.Instance.StopGlitch();
		public void StartChroma() => PostProcessingUnit.Instance.ChromaOffset(ChromaOffsetStrength);
	}
}