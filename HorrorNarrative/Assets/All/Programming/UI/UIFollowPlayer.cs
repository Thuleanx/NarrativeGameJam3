using UnityEngine;

namespace Thuleanx.AI {
	public class UIFollowPlayer : MonoBehaviour {
		[SerializeField] Vector2 displacement = new Vector2(0, 2f);
		GameObject Player;
		RectTransform rectTransform;

		void Awake() {
			rectTransform = GetComponent<RectTransform>();
		}

		void Update() {
			if (Player == null) Player = GameObject.FindWithTag("Player");
			if (Player != null) {
				rectTransform.position = (Vector2) Utility.General.ToScreenSpace(Player.transform.position) 
					+ displacement * 
					(Utility.General.ToScreenSpace(Vector2.right + Vector2.up) - Utility.General.ToScreenSpace(Vector2.zero));
			}
		}
	}
}