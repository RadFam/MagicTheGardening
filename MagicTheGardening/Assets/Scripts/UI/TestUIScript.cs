using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestUIScript : MonoBehaviour {

    [SerializeField]
    string BtnName;

    public Text btnText;

	// Use this for initialization
	void Start ()
    {
        btnText.text = BtnName;
	}
}
