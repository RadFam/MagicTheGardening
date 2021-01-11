using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Interaction", menuName = "ScriptableObjects/Interactions/...", order = 5)]
public abstract class Interaction : ScriptableObject {

    public GameObject parentObj; // GameObject that own the Interaction object
    public GameObject clientObj; // Object that turns to the parentObj

    public abstract void Init(string name);
    public abstract void React(string name);

    public abstract void SetParent(GameObject go);
    public abstract void SetClient(GameObject go);
    public abstract GameObject GetParent(string name);
    public abstract GameObject GetClient(string name);
}
