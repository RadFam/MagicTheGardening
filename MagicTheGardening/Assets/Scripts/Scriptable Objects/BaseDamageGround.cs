using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ground_Damage", menuName = "ScriptableObjects/Ground_Damages_Probs", order = 3)]
public class BaseDamageGround : ScriptableObject {

	public List<float> insectDamageProbs; // Three types of grounds
    public List<float> moleratDamageProbs; // Three types of grounds
}
