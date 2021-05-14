using UnityEngine;
using Thuleanx.Utility;

namespace Thuleanx.Cinematography {
	public class ObjectStayOnScreen : MonoBehaviour {
		RectTransform rectTransform;

		void Awake() {
			rectTransform = GetComponent<RectTransform>();
		}

		void LateUpdate() {
			Vector2 botLeft = General.ViewportToScreenPoint(Vector2.zero);
			Vector2 topRight = General.ViewportToScreenPoint(new Vector2(1, 1));

			Vector2 LeftDown = -rectTransform.rect.min;
			Vector2 UpRight= rectTransform.rect.max;

			rectTransform.position = new Vector2(
				Mathf.Clamp(rectTransform.position.x, botLeft.x + LeftDown.x, topRight.x - UpRight.x),
				Mathf.Clamp(rectTransform.position.y, botLeft.y + LeftDown.y, topRight.y - UpRight.y)
			);
		}
	}
}