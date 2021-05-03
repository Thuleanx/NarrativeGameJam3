using UnityEngine;

namespace Thuleanx.Behaviour {
	public class FollowPlayer : MonoBehaviour {
		[SerializeField] Vector2 displacement = new Vector2(0, 2f);
		GameObject Player;

		void Update() {
			if (Player == null) Player = GameObject.FindWithTag("Player");
			if (Player != null) {
				transform.position = (Vector2) Player.transform.position + displacement;
			}
		}
	}
}