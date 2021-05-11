using UnityEngine;
using UnityEngine.Events;

namespace Thuleanx.Mechanics.Corpse
{
	public class Corpse : MonoBehaviour {
		public Vector2 Dir;
		[HideInInspector]
		public bool RightFacing => Dir.x >= 0;
		public Rigidbody2D RigidBody;

		public UnityEvent OnSpawn;

		void Awake() {
			RigidBody = GetComponent<Rigidbody2D>();
		}

		public virtual void OnEnable() {
			OnSpawn?.Invoke();
		}
	}
}