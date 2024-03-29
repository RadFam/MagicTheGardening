﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameControllers
{
    public enum TypeOfInteraction { GroundPlow, GrowndWater, GroundFertilize, GroundImprove, GroundGetProduct, GroundPutProduct, ExchangeItem, ExtradeItem, TakeQuest, MakeQuest, AttackMagic, AttackSword, GetDamage};
    public enum Actions { NoAction, Plow, Water, PutSeeds, PutProduct, PutImprovement, PutFertilizer, Attack, Trade };
    public enum StorageTypes { Player, Chest, Boiler, Salesman };
    public enum PortalNames {FarmInnerPortal, FarmOuterPortal, CityOneInnerPortal, CityOneOuterPortal, CityTwoInnerPortal, CityTwoOuterPortal};
}