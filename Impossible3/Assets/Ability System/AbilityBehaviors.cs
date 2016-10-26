using UnityEngine;
using System.Collections;

public class AbilityBehaviors : MonoBehaviour{

	private BasicObjectInformation objectInfo;
	private BehaviorStartTimes startTime;

	public AbilityBehaviors(){
		//empty constructor to fix bugs
	}
	public AbilityBehaviors(BasicObjectInformation basicInfo, BehaviorStartTimes sTime){
		objectInfo = basicInfo;
		startTime = sTime;
	}

	public enum BehaviorStartTimes{ //effects at the start, middle, and end 
		Beginning,
		Middle, 
		End
	}

	public virtual void PerformBehavior(){
	}

	public BasicObjectInformation AbilityBehaviorInfo{
		get{
			return objectInfo;
		}
	}
	public BehaviorStartTimes AbilityBehaviorStartTime{
		get { 
			return startTime;
		}
	}
}
