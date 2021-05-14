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

			rectTransform.position = new Vector2(
				Mathf.Clamp(rectTransform.position.x, botLeft.x + rectTransform.rect.width/2, topRight.x - rectTransform.rect.width/2),
				Mathf.Clamp(rectTransform.position.y, botLeft.y + rectTransform.rect.height/2, topRight.y - rectTransform.rect.height/2)
			);
		}
	}
}