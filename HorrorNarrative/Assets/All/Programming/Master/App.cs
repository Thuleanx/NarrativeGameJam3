using UnityEngine;
using System;
using Thuleanx.Master.Global;
using Thuleanx.Controls;
using FMOD_Thuleanx;

namespace Thuleanx {
	public class App : MonoBehaviour {
		public static bool IsEditor = false;

		public static App Instance;

		public GameModeManager _GameModeManager;
		public InputManager _InputManager;
		public AudioManager _AudioManager;

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