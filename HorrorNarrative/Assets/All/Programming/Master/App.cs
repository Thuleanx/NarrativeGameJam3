using UnityEngine;
using System;
using System.Collections.Generic;
using Thuleanx.Master.Global;
using Thuleanx.Controls;
using FMOD_Thuleanx;
using Thuleanx.Master.Local;
using UnityEngine.SceneManagement;	
using Thuleanx.Dialogue;
using Thuleanx.Optimization;
using Thuleanx.Cinematography;

namespace Thuleanx {
	public class App : MonoBehaviour {
		public static bool IsEditor = false;

		public static App Instance;
		public static LocalApp LocalInstance;

		public GameModeManager _GameModeManager;
		public InputManager _InputManager;
		public AudioManager _AudioManager;
		public DialogueManager _DialogueManager;
		public TransitionManager _TransitionManager;
		public CinematographyManager _CinematographyManager;

		[HideInInspector]
		public List<BubblePool> activePools = new List<BubblePool>();

		void Awake() {
			Instance = this;
			SceneManager.sceneLoaded += OnNewScene;
			App.LocalInstance = GameObject.FindObjectOfType<LocalApp>();
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void Bootstrap() {
			var app = UnityEngine.Object.Instantiate(Resources.Load("App")) as GameObject;
			if (app == null) throw new ApplicationException();
			UnityEngine.Object.DontDestroyOnLoad(app);
		}

		public void OnNewScene(Scene scene, LoadSceneMode mode) {
			App.LocalInstance = GameObject.FindObjectOfType<LocalApp>();
		}
	}
}