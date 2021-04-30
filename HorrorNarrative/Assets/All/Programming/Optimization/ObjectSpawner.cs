using UnityEngine;

namespace Thuleanx.Optimization {
	public class ObjectSpawner : MonoBehaviour {
		[SerializeField] string SpawnTag;
		[SerializeField] GameObject At;
		[SerializeField] bool InheritRotation;

		public virtual void Spawn() {
			if (At != null)
				ObjectPool.Instance.Instantiate(SpawnTag, At.transform.position, InheritRotation ? 
					At.transform.rotation : Quaternion.identity);
			else 
				ObjectPool.Instance.Instantiate(SpawnTag);
		}
	}
}