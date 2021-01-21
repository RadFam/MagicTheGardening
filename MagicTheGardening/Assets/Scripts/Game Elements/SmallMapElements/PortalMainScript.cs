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

	void Start ()
	{
		playerSpawnPlace = transform.GetChild(0).gameObject;
		//playerInitPosition = playerSpawnPlace.transform;
	}

	public Transform PlayerInitSpawn()
	{
		return playerSpawnPlace.transform;;
	}
}
