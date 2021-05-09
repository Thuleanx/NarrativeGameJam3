using Thuleanx.AI;
using UnityEngine;

namespace Thuleanx.Animation {
	public class FlipWithAgent : MonoBehaviour {
		[SerializeField] bool flipX = false;

		Agent Agent;
		SpriteRenderer Sprite;
		float originLocalPos;

		public virtual void Awake() {
			Sprite = GetComponent<SpriteRenderer>();
			originLocalPos = transform.localPosition.x;
			Agent = GetComponentInParent<Agent>();
		}

		// direction is either 0 or 1
		public virtual void Flip(bool right) {
			if (Sprite != null) {
				Sprite.flipX = right^flipX^true;
			}
			else transform.localScale = new Vector3(
				right^flipX ? 1 : -1,
				1f,
				1f
			);
			transform.localPosition  = new Vector3(
				originLocalPos * (right^flipX ? 1 : -1), 
				transform.localPosition.y, 
				transform.localPosition.z
			);
		} 

		void Update() {
			Flip(Agent.LocalContext.RightFacing);
		}
	}
}