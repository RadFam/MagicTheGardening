using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameControllers
{
    public enum TypeOfInteraction { GroundPlow, GrowndWater, GroundFertilize, GroundImprove, GroundGetProduct, GroundPutProduct, PutItem, GetItem, TakeQuest, MakeQuest, Trade, AttackMagic, AttackSword, GetDamage};
    public enum Actions { NoAction, Plow, Water, PutSeeds, PutProduct, PutImprovement, PutFertilizer, Attack };
}