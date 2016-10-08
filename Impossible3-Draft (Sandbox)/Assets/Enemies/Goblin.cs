using UnityEngine;
using System;

public class Goblin : BaseMinion
{
    private int minionID;

    private string minionType = "Goblin";
    private string minionDescription = "So many of them, this place crawls!";

    private int health = 20;
    private int attackTime;
    private int attackStrength = 20;

    private int movementTime;
    private int movementDistance = 1;

    private int armorValue = 5;
}
