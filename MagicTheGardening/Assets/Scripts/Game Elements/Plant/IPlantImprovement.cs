using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IPlantImprovement {
    void SetGrowthAccelerationRate(List<float> rateParams);
    void RemoveGrowthAccelerationRate();
    void GrowthAcceleration();

    void SetFetusIncreaseRate(List<float> rateParams);
    void RemoveFetusIncreaseRate();
    void FetusIncrease();

    void SetFetusQualityRate(List<float> rateParams);
    void RemoveFetusQualityRate();
    void FetusQuality();
}
