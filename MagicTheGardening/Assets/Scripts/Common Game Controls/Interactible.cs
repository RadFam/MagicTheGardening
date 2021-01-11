using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameControllers
{
    public class Interactible : MonoBehaviour
    {

        [SerializeField]
        List<TypeOfInteraction> toi;
        [SerializeField]
        List<Interaction> inter;

        public Dictionary<TypeOfInteraction, Interaction> ReactionsList = new Dictionary<TypeOfInteraction, Interaction>();

        void Start()
        {
            for (int i = 0; i < toi.Count; ++i)
            {
                ReactionsList.Add(toi[i], inter[i]);
            }

            foreach (KeyValuePair<TypeOfInteraction, Interaction> kvp in ReactionsList)
            {
                kvp.Value.parentObj = this.gameObject;
                kvp.Value.SetParent(this.gameObject);
                kvp.Value.Init(this.gameObject.name);
            }
        }

        public void Connect(TypeOfInteraction interact, GameObject go)
        {
            foreach (KeyValuePair<TypeOfInteraction, Interaction> kvp in ReactionsList)
            {
                if (interact == kvp.Key)
                {
                    kvp.Value.clientObj = go;
                    kvp.Value.React(this.gameObject.name);
                    return;
                }
            }
        }
    }
}