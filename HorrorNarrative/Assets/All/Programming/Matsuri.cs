using UnityEngine;
using System;

namespace Thuleanx {
	public class Matsuri {
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void Bootstrap() {
			var app = UnityEngine.Object.Instantiate(Resources.Load("App")) as GameObject;
			if (app == null) throw new ApplicationException();
			UnityEngine.Object.DontDestroyOnLoad(app);
		}
	}
}