using UnityEngine;
using UnityEngine.SceneManagement;

namespace Thuleanx.Mechanics.Mapping {
	public class SceneTransitionManager {
		public static void TransitionSingle(Passage passage) {
			SceneManager.LoadScene(passage.target_scene.SceneReference.SceneName, LoadSceneMode.Single);
		}
	}
}