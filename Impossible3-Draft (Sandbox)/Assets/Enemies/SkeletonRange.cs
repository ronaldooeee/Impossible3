using UnityEngine;
using System;

public class SkeletonRange : BaseMinion
{
    private int minionID;

    private string minionType = "Skeleton Archer";
    private string minionDescription = "A veritable rain of arrows.";

    private int health = 35;
    private int attackTime;
    private int attackStrength = 10;

    private int movementTime;
    private int movementDistance = 1;

    private int armorValue = 10;

    private int attackDistance; //...?
}
