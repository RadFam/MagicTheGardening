using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ground_GFX", menuName = "ScriptableObjects/Ground_Graphics", order = 2)]
public class GroundAllViews : ScriptableObject
{
    public string grType; // normal, stoned, watered
    public int geTypeN; // 0, 1, 2
    public List<Material> clearGroundMat;
    public List<Material> dryGroundMat;
    public List<Material> wateredGroundMat;
    public List<Material> fertilizerMat;



    public List<Vector2> unplowedSpeed_Max;
    public List<Vector2> unwateredSpeed_Max;

    // Best to make them in other SO
    // Because it can be various types of fertilizers and improvements
    public List<Vector2> unfertilizedSpeed_Max = new List<Vector2>();
    public List<Vector2> unimproveOneSpeed_Max = new List<Vector2>();
    public List<Vector2> unimproveTwoSpeed_Max = new List<Vector2>();
}