using UnityEngine;

namespace Thuleanx.Mechanics.Mapping {
	public class Passage : ScriptableObject {
		public string passage_name;
		public SceneHandler target_scene;
		public Passage target_passage;
	}
}