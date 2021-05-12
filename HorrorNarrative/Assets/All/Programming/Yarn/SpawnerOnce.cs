using UnityEngine;
using Thuleanx;

namespace Yarn_Thuleanx {
	public class SpawnerOnce : Spawner {
		string HasSpawnedMemoryName() => "*spawned_" + gameObject.GetInstanceID();

		void OnEnable() {
			if (App.Instance._DialogueManager.Storage.GetValue(HasSpawnedMemoryName()).AsBool)
				gameObject.SetActive(false);
		}

		public override void Spawn() {
			base.Spawn();
			App.Instance._DialogueManager.Storage.SetValue(HasSpawnedMemoryName(), true);
			gameObject.SetActive(false);
		}
	}
}