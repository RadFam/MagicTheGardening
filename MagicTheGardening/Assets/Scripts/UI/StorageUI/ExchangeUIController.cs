using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameElement;
using GameControllers;

public class ExchangeUIController : MonoBehaviour {

    public Camera cam;

    public PlayerStorageUIController playerPrefab;
    public ChestStorageUIController chestPrefab;
    public SalesmanStorageUIController salesPrefab;
    public Button exitButton;

    StorageScript SSc_obj;
    StorageScript SSc_subj;
    
    // Use this for initialization
	void Start () {
        cam = Camera.main;
	}

    public void ShowPlayerChest(ref StorageScript ssc_1, ref StorageScript ssc_2)
    {
        SSc_obj = ssc_1;
        SSc_subj = ssc_2;

        // Instantiate storage panels by the placement of objects
        Vector3 obj_pos = cam.WorldToScreenPoint(ssc_1.gameObject.transform.position);
        Vector3 subj_pos = cam.WorldToScreenPoint(ssc_2.gameObject.transform.position);


    }

    public void ShowPlayerSales()
    {

    }
}
