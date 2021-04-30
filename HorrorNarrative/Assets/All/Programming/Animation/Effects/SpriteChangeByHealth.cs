using UnityEngine;
using Thuleanx.Master;	
using Thuleanx.AI;

namespace Thuleanx.Animation {
	public class SpriteChangeByHealth : MonoBehaviour {
		public Agent Agent {get; private set; }
		public Animator Anim {get; private set; }
		public string HealthVariable = "Health";
		public string DamagedVariable = "Damaged";
		public string DeathTrigger = "Death";

		private void Awake() {
			Agent = GetComponentInParent<Agent>();
			Anim = GetComponent<Animator>();
		}

		void Update() {
			Anim.SetInteger(HealthVariable, Agent.LocalContext.Health);
			Anim.SetBool(DamagedVariable, Agent.LocalContext.Damaged);
		}

		public void OnDeath() {
			Anim.SetTrigger(DeathTrigger);
		}
	}
}