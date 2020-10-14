using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plant_Growth", menuName = "ScriptableObjects/Plant_Growth_Stages", order = 4)]
public class PlantGrowthCycle : ScriptableObject
{
    public int growthStages;
    public string plantName;

    public List<Mesh> plantView = new List<Mesh>();
    public List<Material> plantTex = new List<Material>();

    public List<int> plantGrowthSpeedGroundType_normal = new List<int>();
    public List<int> plantGrowthSpeedGroundType_stoned = new List<int>();
    public List<int> plantGrowthSpeedGroundType_watered = new List<int>();

    // Here place links to another scriptable object, where we describe planted goods
    // What products this plant will give after growth
    public List<ProductCommon> productsCanGrow;
    public List<int> productsCanGrowVols;
    public List<float> productsCanGrowProbabs;
}
