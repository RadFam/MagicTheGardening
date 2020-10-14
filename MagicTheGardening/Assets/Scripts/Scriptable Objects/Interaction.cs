using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Interaction", menuName = "ScriptableObjects/Interactions/...", order = 5)]
public abstract class Interaction : ScriptableObject {

    public GameObject parentObj; // GameObject that own the Interaction object
    public GameObject clientObj; // Object that turns to the parentObj

    public abstract void Init();
    public abstract void React();
}
