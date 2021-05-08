using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using ThirdParty.Utility;

namespace Thuleanx.Mechanics.Mapping {
	[CreateAssetMenu(fileName = "SceneHandler", menuName = "~/Mapping/SceneHandler", order = 0)]
	public class SceneHandler : ScriptableObject {
		public SceneReference SceneReference;
		[HideInInspector]
		public List<Passage> Passages = new List<Passage>();
	}
}