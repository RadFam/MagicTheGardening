using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ground_GFX", menuName = "ScriptableObjects/Ground_Graphics", order = 2)]
public class GroundAllViews : ScriptableObject
{
    public string grType; // normal, stoned, watered
    public int geTypeN; // 0, 1, 2
    public List<Material> dryGroundMat;
    public List<Material> wateredGroundMat;
    public List<Material> fertilizerMat;
}