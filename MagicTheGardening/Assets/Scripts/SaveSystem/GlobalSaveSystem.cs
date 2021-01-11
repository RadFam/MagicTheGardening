using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameControllers
{
    public class GlobalSaveSystem : MonoBehaviour
    {
        // Load Scene coroutine(...make it later)
        //

        public void Save(string fileName)
        {
            Dictionary<string, object> state = LoadFile(fileName);
            CaptureGlobalState(state);
            SaveFile(fileName, state);
        }

        public void Load(string fileName)
        {
            //Debug.Log("Load from " + fileName);
            RestoreGlobalState(LoadFile(fileName));
        }

        public void Delete(string fileName)
        {
            File.Delete(GetFullFilename(fileName));
        }

        public void SaveFile(string saveFile, object state)
        {
            string pathFull = GetFullFilename(saveFile);
            //Debug.Log("Save in " + pathFull);
            using (FileStream stream = File.Open(pathFull, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        public Dictionary<string, object> LoadFile(string fileName)
        {
            string pathFull = GetFullFilename(fileName);
            if (!File.Exists(pathFull))
            {
                return new Dictionary<string, object>();
            }

            using (FileStream stream = File.Open(pathFull, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        public void CaptureGlobalState(Dictionary<string, object> state)
        {
            foreach (SavingEntity saving in FindObjectsOfType<SavingEntity>())
            {
                state[saving.GetUniqueIdentifier()] = saving.CaptureState();
            }
        }

        public void RestoreGlobalState(Dictionary<string, object> state)
        {
            foreach (SavingEntity saving in FindObjectsOfType<SavingEntity>())
            {
                string id = saving.GetUniqueIdentifier();
                if (state.ContainsKey(id))
                {
                    saving.RestoreState(state[id]);
                }
            }
        }

        private string GetFullFilename(string fileName)
        {
            return Path.Combine(Application.persistentDataPath, fileName + ".smtg");
        }
    }
}