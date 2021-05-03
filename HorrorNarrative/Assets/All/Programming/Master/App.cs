using UnityEngine;
using System;

namespace Thuleanx {
	public class App : MonoBehaviour {
		public static App Instance;

		void Awake() {
			Instance = this;
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void Bootstrap() {
			var app = UnityEngine.Object.Instantiate(Resources.Load("App")) as GameObject;
			if (app == null) throw new ApplicationException();
			UnityEngine.Object.DontDestroyOnLoad(app);
		}
	}
}