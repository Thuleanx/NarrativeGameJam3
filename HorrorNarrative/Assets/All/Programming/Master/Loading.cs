using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;

namespace Thuleanx.Master {
	public class Loading : MonoBehaviour
	{
		void Update()
		{
			try {
				if (FMODUnity.RuntimeManager.HasBanksLoaded)
				{
					Debug.Log("Master Bank Loaded");
					App.Instance._GameModeManager.Boot();
				} else {
					Debug.Log("Master Bank Not Yet Loaded " + FMODUnity.RuntimeManager.AnyBankLoading());
				}
			} catch (Exception err) {
				Debug.Log(err);
			}

		}
	}
}
