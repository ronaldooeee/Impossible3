using UnityEngine;
using System;

public class Skeleton : BaseMinion
{
    private int minionID;

    private string minionType = "Skeleton";
    private string minionDescription = "Buried underground, coming up for air, MUST, EAT, FLESH.";

    public new int health = 35;
    private int attackTime;
    private int attackStrength;

    private int movementTime;
    private int movementDistance = 1;

    private int armorValue;
}
