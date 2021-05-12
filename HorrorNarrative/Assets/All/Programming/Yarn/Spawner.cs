using UnityEngine;
using Yarn.Unity;
using Thuleanx.Optimization;

namespace Yarn_Thuleanx {
	public class Spawner : MonoBehaviour {
		[SerializeField] BubblePool Pool;

		[YarnCommand("Spawn")]
		public virtual void Spawn() {
			if (Pool != null) {
				GameObject obj = Pool.Borrow(transform.position, Quaternion.identity);
				obj.name = Pool.prefab.name;
			}
		}
	}
}