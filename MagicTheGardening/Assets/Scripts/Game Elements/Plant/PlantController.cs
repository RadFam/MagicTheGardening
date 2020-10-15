using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameControllers;
using Improvements;

namespace GameElement
{
    public class PlantController : MonoBehaviour, IPlantImprovement
    {
        public PlantGrowthCycle plantGrowthData;

        int groundType;
        int plantStages;
        int currentStage;
        int growthTimer;
        List<int> plantStageGrowthSpeed;

        MeshFilter meshFilter;
        MeshRenderer meshRendered;

        // Use this for initialization
        void Start()
        {
            groundType = 0;
            growthTimer = 0;
            //LoadData(0);
        }

        public void LoadData(int ground)
        {
            growthTimer = 0;
            currentStage = 0;
            groundType = ground;
            plantStages = plantGrowthData.growthStages;

            meshFilter = gameObject.AddComponent<MeshFilter>(); Debug.Log("Plant mesh filter is: " + meshFilter);
            meshFilter.sharedMesh = plantGrowthData.plantView[currentStage];
            meshRendered = gameObject.AddComponent<MeshRenderer>();
            meshRendered.material = plantGrowthData.plantTex[currentStage];

            SetPlantGrowthSpeeds();
        }

        void SetPlantGrowthSpeeds()
        {
            if (groundType == 0)
            {
                plantStageGrowthSpeed = plantGrowthData.plantGrowthSpeedGroundType_normal;
            }

            if (groundType == 1)
            {
                plantStageGrowthSpeed = plantGrowthData.plantGrowthSpeedGroundType_stoned;
            }

            if (groundType == 2)
            {
                plantStageGrowthSpeed = plantGrowthData.plantGrowthSpeedGroundType_watered;
            }
        }

        void UpdatePlantView()
        {
            if (currentStage < plantStages)
            {
                meshFilter.sharedMesh = plantGrowthData.plantView[currentStage];
                meshRendered.material = plantGrowthData.plantTex[currentStage];
            }
            else
            {
                currentStage = 0;
                Destroy(meshFilter);
                Destroy(meshRendered);
            }
        }

        public void OnTictocTimer()
        {
            growthTimer++;
            int tmpStage = 0;
            int daysCounter = 0;
            for (int i = 0; i < plantStageGrowthSpeed.Count; ++i )
            {
                if (growthTimer > daysCounter && growthTimer <= daysCounter+plantStageGrowthSpeed[i])
                {
                    tmpStage = i;
                }
                daysCounter += plantStageGrowthSpeed[i];
            }

            if (tmpStage > currentStage)
            {
                currentStage = tmpStage;
                UpdatePlantView();
            }

            if (growthTimer > daysCounter) // растение "перезрело"
            {
                // Need to send appropriate message to parenting ground
                // ....................................................
                Destroy(gameObject);
            }
        }

        public bool GetProducts()
        {
            if (currentStage == plantStages - 1)
            {
                // Возвращаем плоды
                // ................
                //Destroy(gameObject);
                return true;
            }

            return false;
        }

        // IPlantImprovement Interface methods realization
        public void SetGrowthAccelerationRate(List<float> rateParams)
        {
            int lengo = Mathf.Min(rateParams.Count, plantStageGrowthSpeed.Count);
            for (int i = 0; i < lengo; ++i)
            {
                plantStageGrowthSpeed[i] = Mathf.Min(1, (int)(rateParams[i] * plantStageGrowthSpeed[i]));
            }
        }
        public void RemoveGrowthAccelerationRate()
        {
            SetPlantGrowthSpeeds();
        }
        public void GrowthAcceleration() // Maybe this function is unnecessary
        {

        }

        public void SetFetusIncreaseRate(List<float> rateParams)
        {

        }
        public void RemoveFetusIncreaseRate()
        {

        }
        public void FetusIncrease()
        {

        }

        public void SetFetusQualityRate(List<float> rateParams)
        {

        }
        public void RemoveFetusQualityRate()
        {

        }
        public void FetusQuality()
        {

        }
    }
}