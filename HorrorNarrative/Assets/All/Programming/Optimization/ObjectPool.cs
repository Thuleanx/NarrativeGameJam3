using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Thuleanx.Utility;

namespace Thuleanx.Optimization {
	public class ObjectPool : MonoBehaviour
	{
		[System.Serializable]
		public class Pool {
			public string tag;
			public GameObject prefab;
			public bool UI;
			[HideInInspector] public int defaultCount = 10;
		}

		public static ObjectPool Instance;

		[SerializeField] string gameCanvasTag = "DialogueCanvas";

		[SerializeField] int expansionConstant = 10;
		[SerializeField] List<Pool> pools = new List<Pool>();

		Dictionary<string, Queue<GameObject>> objectQueues = new Dictionary<string, Queue<GameObject>>();
		Dictionary<string, int> tagToPoolIndex = new Dictionary<string, int>();
		

		void Awake() {
			if (!General.DisableSelfIfDuplicate(Instance, this)) {
				Instance = this;
				for (int i = 0; i < pools.Count; i++) {
					tagToPoolIndex[pools[i].tag] = i;
					objectQueues[pools[i].tag] = new Queue<GameObject>();
					Expand(pools[i], pools[i].defaultCount);
				}
			}
		}

		void Expand(Pool pool, int count) {
			while (count --> 0) {
				GameObject obj = Instantiate(pool.prefab, Vector3.zero, Quaternion.identity);
				obj.SetActive(false);
				obj.AddComponent(typeof(ObjectReturnToPool));

				obj.transform.SetParent(transform);

				ObjectReturnToPool returnScript = obj.GetComponent<ObjectReturnToPool>();
				returnScript.sourceTag = pool.tag;

				objectQueues[pool.tag].Enqueue(obj);

			}
		}

		public GameObject Instantiate(string tag) {
			return Instantiate(tag, Vector3.zero, Quaternion.identity);
		}

		public GameObject Instantiate(string tag, Vector3 position, Quaternion rotation) {
			Assert.IsTrue(objectQueues.ContainsKey(tag));

			if (objectQueues[tag].Count == 0)
				Expand(pools[tagToPoolIndex[tag]], expansionConstant);

			GameObject obj = objectQueues[tag].Dequeue();
			obj.transform.position = position;
			obj.transform.rotation = rotation;
			if (pools[tagToPoolIndex[tag]].UI) 
				obj.transform.SetParent(GameObject.FindWithTag(gameCanvasTag).transform);

			obj.SetActive(true);

			return obj;
		}

		public void Return(string tag, GameObject obj) {
			Assert.IsTrue(objectQueues.ContainsKey(tag));
			objectQueues[tag].Enqueue(obj);
			obj.transform.SetParent(transform);
		}

		void OnDestroy() {
			if (Instance == this) Instance = null;
		}
	}
}