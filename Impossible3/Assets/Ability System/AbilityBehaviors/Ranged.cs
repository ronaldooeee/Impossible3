using UnityEngine;
using System.Collections;

public class Ranged : AbilityBehaviors {

	private const string objName = "Ranged";
	private const string objDescription = "A ranged attack!";
	private const BehaviorStartTimes startTime = BehaviorStartTimes.Beginning;
	//private const Sprite icon = Resources.Load();

	private float minDistance; 
	private float maxDistance; 


	public Ranged(float minDist, float maxDist)
		:base (new BasicObjectInformation(objName, objDescription), startTime)
	{

	}

	public float MinDistance{
		get { return minDistance; }
	}
	public float MaxDistance {
		get{ return maxDistance; }
	}
}
