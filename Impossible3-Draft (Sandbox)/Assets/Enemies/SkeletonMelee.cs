using UnityEngine;
using System;

public class SkeletonMelee : BaseMinion
{
    private int minionID;

    private string minionType = "Skeleton Warrior";
    private string minionDescription = "BRAWL BRAWL BRAWL!";

    public new int health = 35;
    private int attackTime;
    private int attackStrength = 20;

    private int movementTime;
    private int movementDistance = 1;

    private int armorValue = 10;
}
