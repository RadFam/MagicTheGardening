using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameControllers;
using Improvements;

namespace GameElement
{
    public class GroundStateControl : MonoBehaviour, ISaveable
    {
        [SerializeField]
        GameObject groundObject;
        [SerializeField]
        GameObject fertilizerObject;
        [SerializeField]
        GameObject plantObject;

        GroundAllViews groundGraphics;
        Dictionary<int, BaseImprovement> groundImproves;
        BaseImprovement groundFertilizer;

        [SerializeField]
        PlantController growthPlant;

        public int groundType;

        int unwateredMax = 3; // Maybe this parameter is worth to introduce into plant characteristics (!)
        int unwateredDays = 0;

        bool isPloweed; // To ISaveable
        bool isWatered; // To ISaveable
        bool isPlanted; // To ISaveable
        bool isFertilized; // To ISaveable
        bool isImproved_1; // To ISaveable
        bool isImproved_2; // To ISaveable

        int[] plowedChar = { 0, 0, 0 }; // 0 - curr vol, 1 - decrease speed, 2 - max value;  // To ISaveable
        int[] wateredChar = { 0, 0, 0 };  // To ISaveable
        int[] fertilizedChar = { 0, 0, 0 };  // To ISaveable
        int[] improvedOneChar = { 0, 0, 0 };  // To ISaveable
        int[] improvedTwoChar = { 0, 0, 0 };  // To ISaveable

        string soPlantName;
        string soFertilizerName;
        string soImproverOneName;
        string soImproverTwoName;

        // Start is called before the first frame update
        void Start()
        {
            groundGraphics = Resources.Load<GroundAllViews>("ScriptableObjects/Ground_GFX");
            groundImproves = new Dictionary<int, BaseImprovement>();

            // Define characteristics speed
            plowedChar[1] = (int)groundGraphics.unplowedSpeed_Max[groundType].x;
            plowedChar[2] = (int)groundGraphics.unplowedSpeed_Max[groundType].y;
            wateredChar[1] = (int)groundGraphics.unwateredSpeed_Max[groundType].x;
            wateredChar[2] = (int)groundGraphics.unwateredSpeed_Max[groundType].y;
            
            //
            fertilizedChar[1] = (int)groundGraphics.unfertilizedSpeed_Max[groundType].x;
            fertilizedChar[2] = (int)groundGraphics.unfertilizedSpeed_Max[groundType].y;
            improvedOneChar[1] = (int)groundGraphics.unimproveOneSpeed_Max[groundType].x;
            improvedOneChar[2] = (int)groundGraphics.unimproveOneSpeed_Max[groundType].y;
            improvedTwoChar[1] = (int)groundGraphics.unimproveTwoSpeed_Max[groundType].x;
            improvedTwoChar[2] = (int)groundGraphics.unimproveTwoSpeed_Max[groundType].y;
            //

            groundObject.GetComponent<Renderer>().material = groundGraphics.clearGroundMat[groundType];

            isPloweed = false;
            isWatered = false;
            isFertilized = false;
            isImproved_1 = false;
            isImproved_2 = false;
            isPlanted = false;

            soPlantName = "";
            soFertilizerName = "";
            soImproverOneName = "";
            soImproverTwoName = "";
        }

        public void TimeFlowReact()
        {
            if (isPloweed && !isWatered)
            {
                plowedChar[0] -= Mathf.Min(plowedChar[0], plowedChar[1]);
                if (plowedChar[0] == 0)
                {
                    ClearizeGround();
                }
            }

            if (isWatered)
            {
                wateredChar[0] -= Mathf.Min(wateredChar[0], wateredChar[1]);
                if (wateredChar[0] == 0)
                {
                    DewaterizeGround();
                }
            }

            if (isFertilized)
            {
                fertilizedChar[0] -= Mathf.Min(fertilizedChar[0], fertilizedChar[1]);
                if (fertilizedChar[0] == 0)
                {
                    DefertilizeGround();
                }
            }

            if (isImproved_1)
            {
                improvedOneChar[0] -= Mathf.Min(improvedOneChar[0], improvedOneChar[1]);
                if (improvedOneChar[0] == 0)
                {
                    DeimproveOneGround();
                }
            }

            if (isImproved_2)
            {
                improvedTwoChar[0] -= Mathf.Min(improvedTwoChar[0], improvedTwoChar[1]);
                if (improvedTwoChar[0] == 0)
                {
                    DeimproveTwoGround();
                }
            }
        }

        void ClearizeGround()
        {
            isPloweed = false;
            isWatered = false;

            DewaterizeGround();
            DefertilizeGround();
            DeimproveOneGround();
            DeimproveTwoGround();

            //Material mat = groundObject.GetComponent<Renderer>().material;
            //mat = groundGraphics.clearGroundMat[groundType];

            groundObject.GetComponent<Renderer>().material = groundGraphics.clearGroundMat[groundType];

            // And delete the plant(!!!)
            // DeletePlant();
            DeplantGround();
        }

        void DewaterizeGround()
        {
            isWatered = false;

            if (isPloweed)
            {
                //Material mat = groundObject.GetComponent<Renderer>().material;
                //mat = groundGraphics.dryGroundMat[groundType];
                groundObject.GetComponent<Renderer>().material = groundGraphics.dryGroundMat[groundType];
            }
            else
            {
                //Material mat = groundObject.GetComponent<Renderer>().material;
                //mat = groundGraphics.clearGroundMat[groundType];
                groundObject.GetComponent<Renderer>().material = groundGraphics.clearGroundMat[groundType];
            }
            // DeletePlant();
            DeplantGround();
        }

        void DefertilizeGround()
        {
            isFertilized = false;

            fertilizedChar[0] = 0;
            fertilizedChar[1] = 0;
            fertilizedChar[2] = 0;

            groundFertilizer.DeactivateImprovement();
            groundFertilizer = null;

            Destroy(fertilizerObject.GetComponent<MeshFilter>()); // Delete mesh of fertilizer
            Destroy(fertilizerObject.GetComponent<MeshRenderer>()); // Delete material of fertilizer

            soFertilizerName = "";
        }

        void DeimproveOneGround()
        {
            isImproved_1 = false;

            improvedOneChar[0] = 0;
            improvedOneChar[1] = 0;
            improvedOneChar[2] = 0;

            groundImproves[1].DeactivateImprovement();
            groundImproves.Remove(1);

            soImproverOneName = "";
        }

        void DeimproveTwoGround()
        {
            isImproved_2 = false;

            improvedTwoChar[0] = 0;
            improvedTwoChar[1] = 0;
            improvedTwoChar[2] = 0;

            groundImproves[2].DeactivateImprovement();
            groundImproves.Remove(2);

            soImproverTwoName = "";
        }

        // Remove Plant from ground
        void DeplantGround()
        {
            isPlanted = false;
            if (isFertilized)
            {
                groundFertilizer.DeactivateImprovement();
                //DefertilizeGround();
            }
            if (isImproved_1)
            {
                groundImproves[1].DeactivateImprovement();
                //DeimproveOneGround();
            }
            if (isImproved_2)
            {
                groundImproves[2].DeactivateImprovement();
                //DeimproveTwoGround();
            }

            Destroy(plantObject.transform.GetChild(0).gameObject);

            soPlantName = "";
        }

        public void TakePlantGround(ref StorageScript SSc)
        {
            if (isPlanted)
            {
                if (plantObject.transform.GetChild(0).gameObject.GetComponent<PlantController>().GetProducts(ref SSc))
                {
                    DeplantGround();
                }
            }
        }

        // On Time Change Function

        public void OnTictocMoment()
        {
            // Send growth info to plant
            if (isPlanted)
            {
                // Check, if ground is watered
                if (!isWatered)
                {
                    unwateredDays++;
                    if (unwateredDays > unwateredMax)
                    {
                        unwateredDays = 0;
                        DeplantGround();
                        return;
                    }
                }

                // Check for Insect and Molerat Damages
                BaseDamagesControl baseDC = GetComponent<BaseDamagesControl>();
                bool res = false;

                if (baseDC != null)
                {
                    res = baseDC.EvalInsectDamage();
                    res = baseDC.EvalMoleratDamage();
                }

                if (res)
                {
                    DeplantGround();
                }
                else if (isPloweed && isWatered)
                {
                    plantObject.transform.GetChild(0).gameObject.GetComponent<PlantController>().OnTictocTimer(); // here we need to check, if we have a plant
                }
            }

            TimeFlowReact();
        }

        public bool AddPlowing()
        {
            isPloweed = true;
            plowedChar[0] = plowedChar[2];

            if (isWatered)
            {
                //Material mat = groundObject.GetComponent<Renderer>().material;
                //mat = groundGraphics.wateredGroundMat[groundType];
                groundObject.GetComponent<Renderer>().material = groundGraphics.wateredGroundMat[groundType];
            }
            else
            {
                //Material mat = groundObject.GetComponent<Renderer>().material;
                //mat = groundGraphics.dryGroundMat[groundType];
                groundObject.GetComponent<Renderer>().material = groundGraphics.dryGroundMat[groundType];
            }

            // Check if here is the plant - we must destroy plant
            if (isPlanted)
            {
                DeplantGround();
            }

            return true;
        }

        public bool AddWaterizing()
        {
            isWatered = true;
            wateredChar[0] = wateredChar[2];

            Debug.Log("Water parameters: " + wateredChar[0].ToString() + " " + wateredChar[1] + " " + wateredChar[2]);

            if (isPloweed)
            {
                //Material mat = groundObject.GetComponent<Renderer>().material;
                //mat = groundGraphics.wateredGroundMat[groundType];
                groundObject.GetComponent<Renderer>().material = groundGraphics.wateredGroundMat[groundType];
            }
            else
            {
                //Material mat = groundObject.GetComponent<Renderer>().material;
                //mat = groundGraphics.clearGroundMat[groundType];
                groundObject.GetComponent<Renderer>().material = groundGraphics.clearGroundMat[groundType];
            }

            return true;
        }

        public bool AddImprovement(BaseImprovement bI, int value, int speed, int maxVal)
        {
            // Put improvements into ground if it was plowed or it was watered
            if (isPloweed || isWatered)
            {
                if (improvedOneChar[0] == 0)
                {
                    isImproved_1 = true;
                    improvedOneChar[0] = value;
                    improvedOneChar[1] = speed;
                    improvedOneChar[2] = maxVal;

                    groundImproves.Add(1, bI);
                    soImproverOneName = bI.improveSC.soLoadName;
                    return true;   
                }

                if (improvedTwoChar[0] == 0)
                {
                    isImproved_2 = true;
                    improvedTwoChar[0] = value;
                    improvedTwoChar[1] = speed;
                    improvedTwoChar[2] = maxVal;

                    groundImproves.Add(2, bI);
                    soImproverTwoName = bI.improveSC.soLoadName;
                    return true;
                }

                // Maybe we need so.InitializeImprovement method start(???)
            }
            return false;
        }

        public bool AddFertilizer(BaseImprovement bI, int value, int speed, int maxVal)
        {
            if (fertilizedChar[0] == 0)
            {
                isFertilized = true;

                fertilizedChar[0] = value;
                fertilizedChar[1] = speed;
                fertilizedChar[2] = maxVal;

                groundFertilizer = bI;

                SpeedFertilizer sf = bI as SpeedFertilizer;

                MeshFilter mf = fertilizerObject.AddComponent<MeshFilter>();
                mf.sharedMesh = sf.GetMesh();
                MeshRenderer mr = fertilizerObject.AddComponent<MeshRenderer>();
                mr.material = sf.GetMaterial();

                soFertilizerName = bI.improveSC.soLoadName;
                return true;
            }
            return false;
        }

        public bool AddPlant(PlantGrowthCycle pgc)
        {
            //if (isPloweed && !isPlanted)
            if (!isPlanted)
            {
                isPlanted = true;

                var plant = Instantiate(growthPlant, plantObject.transform) as PlantController;
                plant.plantGrowthData = pgc;
                plant.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                plant.LoadData(groundType);

                if (isFertilized)
                {
                    groundFertilizer.ActivateImprovement();
                }
                if (isImproved_1)
                {
                    groundImproves[1].ActivateImprovement();
                }
                if (isImproved_2)
                {
                    groundImproves[2].ActivateImprovement();
                }

                soPlantName = pgc.soLoadName;
            }

            return false;
        }

        public GameObject GetPlantObject() // Can it return null (???)
        {
            return plantObject.transform.GetChild(0).gameObject;
        }


        // Define functions later
        public object CaptureObject()
        {
            //Start making great saving file
            Dictionary<string, object> myActualBoilerContent = new Dictionary<string, object>();

            myActualBoilerContent.Add("isPloweed", (object)isPloweed);
            myActualBoilerContent.Add("isWatered", (object)isWatered);
            myActualBoilerContent.Add("isPlanted", (object)isPlanted);
            myActualBoilerContent.Add("isFertilized", (object)isFertilized);
            myActualBoilerContent.Add("isImproved_1", (object)isImproved_1);
            myActualBoilerContent.Add("isImproved_2", (object)isImproved_2);

            myActualBoilerContent.Add("plowedChar", (object)plowedChar);
            myActualBoilerContent.Add("wateredChar", (object)wateredChar);
            myActualBoilerContent.Add("fertilizedChar", (object)fertilizedChar);
            myActualBoilerContent.Add("improvedOneChar", (object)improvedOneChar);
            myActualBoilerContent.Add("improvedTwoChar", (object)improvedTwoChar);

            //myActualBoilerContent.Add("soPlantName", (object)soPlantName);
            if (plantObject.transform.GetChild(0).gameObject == null)
            {
                myActualBoilerContent.Add("soPlantName", (object)null);
            }
            else
            {
                myActualBoilerContent.Add("soPlantName", plantObject.transform.GetChild(0).gameObject.GetComponent<PlantController>().GetDataToSave());
            }
            myActualBoilerContent.Add("soFertilizerName", (object)soFertilizerName);
            myActualBoilerContent.Add("soImproverOneName", (object)soImproverOneName);
            myActualBoilerContent.Add("soImproverTwoName", (object)soImproverTwoName);

            return myActualBoilerContent;
        }

        public void RestoreObject(object obj)
        {
            //.........
            Dictionary<string, object> loadedData = obj as Dictionary<string, object>;

            isPloweed = (bool)loadedData["isPloweed"];
            isWatered = (bool)loadedData["isWatered"];
            isPlanted = (bool)loadedData["isPlanted"];
            isFertilized = (bool)loadedData["isFertilized"];
            isImproved_1 = (bool)loadedData["isImproved_1"];
            isImproved_2 = (bool)loadedData["isImproved_2"];

            plowedChar = loadedData["plowedChar"] as int[];
            wateredChar = loadedData["wateredChar"] as int[];
            fertilizedChar = loadedData["fertilizedChar"] as int[];
            improvedOneChar = loadedData["improvedOneChar"] as int[];
            improvedTwoChar = loadedData["improvedTwoChar"] as int[];

            soFertilizerName = (string)loadedData["soFertilizerName"];
            soImproverOneName = (string)loadedData["soImproverOneName"];
            soImproverTwoName = (string)loadedData["soImproverTwoName"];

            groundImproves[0] = null;
            groundImproves[1] = null;
            groundFertilizer = null;

            if (soFertilizerName != "")
            {
                // load Fertilizer
                soFertilizerName = "ScriptableObjects/" + soFertilizerName;

                ScriptableImprovement si = Resources.Load<ScriptableImprovement>(soFertilizerName);
                BaseImprovement bi = si.InitializeImprovement(gameObject);
                groundFertilizer = bi;
                bi.ApplyImprovemenetEffect();

                SpeedFertilizer sf = bi as SpeedFertilizer;

                MeshFilter mf = fertilizerObject.AddComponent<MeshFilter>();
                mf.sharedMesh = sf.GetMesh();
                MeshRenderer mr = fertilizerObject.AddComponent<MeshRenderer>();
                mr.material = sf.GetMaterial();

                soFertilizerName = (string)loadedData["soFertilizerName"];
            }

            if (soImproverOneName != "")
            {
                // load Improvement Two
                soImproverOneName = "ScriptableObjects/" + soImproverOneName;

                ScriptableImprovement si = Resources.Load<ScriptableImprovement>(soImproverOneName);
                BaseImprovement bi = si.InitializeImprovement(gameObject);
                groundImproves[0] = bi;
                bi.ApplyImprovemenetEffect();

                soImproverOneName = (string)loadedData["soImproverOneName"];
            }

            if (soImproverTwoName != "")
            {
                // load Improvement Two
                soImproverTwoName = "ScriptableObjects/" + soImproverTwoName;

                ScriptableImprovement si = Resources.Load<ScriptableImprovement>(soImproverTwoName);
                BaseImprovement bi = si.InitializeImprovement(gameObject);
                groundImproves[1] = bi;
                bi.ApplyImprovemenetEffect();

                soImproverTwoName = (string)loadedData["soImproverTwoName"];
            }

            if (loadedData["soPlantName"] != null)
            {
                // Create growing plant
                Destroy(plantObject.transform.GetChild(0).gameObject);
                var plant = Instantiate(growthPlant, plantObject.transform) as PlantController;
                plant.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                plant.SetDataToLoad((object)loadedData["soPlantName"]);
            }

            return;
        }
    }
}
