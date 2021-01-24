using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameControllers;

public class PortalMainScript : MonoBehaviour {

	// Use this for initialization
	public PortalNames myPortalTag;
	public PortalNames myDestinationTag;
	GameObject playerSpawnPlace;
	//Transform playerInitPosition;

	void Awake ()
	{
		//Debug.Log("My portal tag: " + myPortalTag + " is loading");
		playerSpawnPlace = transform.GetChild(0).gameObject;
		//playerInitPosition = playerSpawnPlace.transform;
	}

	public Transform PlayerInitSpawn()
	{
		//Debug.Log("playerSpawnPlace: " + playerSpawnPlace);
		return playerSpawnPlace.transform;
	}
}
