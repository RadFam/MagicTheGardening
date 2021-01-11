using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GameControllers
{
    public class SavingEntity : MonoBehaviour
    {
        [SerializeField]
        string uniqueIdentifier = "";

        static Dictionary<string, SavingEntity> globalLookup = new Dictionary<string, SavingEntity>();

        private void Awake()
        {
            /*
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");

            if (string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }

            globalLookup[property.stringValue] = this;
            */
        }
        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        private bool IsUnique(string candidate)
        {
            if (!globalLookup.ContainsKey(candidate)) return true;

            if (globalLookup[candidate] == this) return true;

            if (globalLookup[candidate] == null)
            {
                globalLookup.Remove(candidate);
                return true;
            }

            if (globalLookup[candidate].GetUniqueIdentifier() != candidate)
            {
                globalLookup.Remove(candidate);
                return true;
            }

            return false;
        }

        public object CaptureState()
        {
            Dictionary<string, object> returnState = new Dictionary<string, object>();
            foreach(ISaveable iSav in GetComponents<ISaveable>())
            {
                returnState[iSav.GetType().ToString()] = iSav.CaptureObject();
            }
            return returnState;
        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
            foreach (ISaveable iSav in GetComponents<ISaveable>())
            {
                string typeStr = iSav.GetType().ToString();
                if (stateDict.ContainsKey(typeStr))
                {
                    iSav.RestoreObject(stateDict[typeStr]);
                }
            }
        }
    }
}