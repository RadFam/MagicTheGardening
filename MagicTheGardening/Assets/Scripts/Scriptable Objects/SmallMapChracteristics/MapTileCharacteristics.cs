using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapTile_Char", menuName = "ScriptableObjects/SmallWorldMap/MapTile_Chars", order = 0)]
public class MapTileCharacteristics : ScriptableObject
{
	[SerializeField]
	float walkingSpeed;
	[SerializeField]
	float eventCheckInterval;
	[SerializeField]
	float eventCheckProbability;

	public float GetWalkingSpeed
	{
		get {return walkingSpeed;}
		set {walkingSpeed = value;}
	}	

	public float GetEventInterval
	{
		get {return eventCheckInterval;}
		set {eventCheckInterval = value;}
	}

	public float GetEventProbability
	{
		get {return eventCheckProbability;}
		set {eventCheckProbability = value;}
	}
}
