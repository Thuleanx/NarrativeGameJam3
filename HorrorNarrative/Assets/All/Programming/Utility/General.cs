using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Thuleanx.Utility
{
    public class General
    {
		public static bool DisableSelfIfDuplicate(MonoBehaviour Instance, MonoBehaviour This) {
			if (Instance != null) {
				Debug.Log("Multiple components of type " + Instance.GetType().Name + " found. Deleting the later one.");
				GameObject.Destroy(This.gameObject);
			}
			return Instance != null;
		}

		public static bool DisableDuplicate(MonoBehaviour Instance) {
			if (Instance != null) {
				Debug.Log("Multiple components of type " + Instance.GetType().Name + " found. Deleting the earlier one.");
				Debug.Log(Instance);
				GameObject.Destroy(Instance.gameObject);
			}
			return Instance != null;
		}

		public static bool FitMask(GameObject obj, LayerMask mask) => (mask>>obj.layer&1) != 0;

		public static Vector2 ToViewportSpace(Vector2 position, Camera camera = null) {
			if (camera == null)
				camera = Camera.main;
			return camera.WorldToViewportPoint(position);
		}

		public static bool InCamera(Vector2 position, Camera camera = null) {
			if (camera == null)
				camera = Camera.main;
			Vector2 pos = ToViewportSpace(position, camera);
			return pos.x >= 0 && pos.x <= 1 && pos.y >= 0 && pos.y <= 1;
		}

		public static IEnumerator _InvokeNextFrame(Action action) {
			yield return null;
			action?.Invoke();
		}
    }
}