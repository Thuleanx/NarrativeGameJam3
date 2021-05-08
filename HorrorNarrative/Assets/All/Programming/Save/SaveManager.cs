using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Thuleanx.SaveManagement {
	public class SaveManager : MonoBehaviour {

		public static bool Save(string saveName, object saveData) {
			BinaryFormatter Formatter = GetBinaryFormatter();

			string saveFolderPath = Application.persistentDataPath + "/saves";

			if (!Directory.Exists(saveFolderPath))
				Directory.CreateDirectory(saveFolderPath);

			string path = saveFolderPath + "/" + saveName + ".save";

			FileStream file = File.Create(path);
			Formatter.Serialize(file, saveData);

			file.Close();

			return false;
		}
		public static object LoadSave(string saveName) {
			string saveFolderPath = Application.persistentDataPath + "/saves";
			string path = saveFolderPath + "/" + saveName + ".save";
			return Load(path);
		}
		public static object Load(string path) {
			if (!File.Exists(path)) return null;

			BinaryFormatter formatter = GetBinaryFormatter();

			FileStream file = File.Open(path, FileMode.Open);

			try {
				object save = formatter.Deserialize(file);
				file.Close();
				return save;
			} catch {
				Debug.LogErrorFormat("Failed to load file at {0}", path);
				file.Close();
				return null;
			}
		}
		public static BinaryFormatter GetBinaryFormatter() {
			BinaryFormatter formatter = new BinaryFormatter();

			SurrogateSelector selector = new SurrogateSelector();

			selector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), 
				new Vector2Surrograte());
			
			formatter.SurrogateSelector = selector;
				
			return formatter;
		}

		public SaveData CurrentSave;
	}

}