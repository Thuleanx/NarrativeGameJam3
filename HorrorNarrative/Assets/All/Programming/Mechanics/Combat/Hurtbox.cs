using UnityEngine;
using Thuleanx.AI;
using Thuleanx.Utility;
using System.Collections;
using System.Collections.Generic;


namespace Thuleanx.Mechanics.Combat {
	[RequireComponent(typeof(BoxCollider2D))]
	public class Hurtbox : MonoBehaviour {
		public static long NextID = 0;

		BoxCollider2D Box;

		[HideInInspector]
		public long ID;
		public Optional<Agent> Agent;

		[SerializeField] Color color;

		void Awake() {
			Box = GetComponent<BoxCollider2D>();
			if (!Agent.Enabled) Agent = new Optional<Agent>(GetComponentInParent<Agent>());
			ID = NextID++;
		}

		void OnDrawGizmos() {
			Gizmos.color = color;
			Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
			if (Box == null) Box = GetComponent<BoxCollider2D>();
			Gizmos.DrawCube(Vector2.zero, new Vector3(Box.size.x, Box.size.y, 1f));
		}
	}
}