using UnityEngine;
using System;

public class Kobucha : BaseMinion
{
    private int minionID;

    private string minionType = "Kobucha!";
    private string minionDescription = "From the dark underbelly of your parents' garage.";

    private int health = 10;
    private int attackTime;
    private int attackStrength = 10;

    private int movementTime;
    private int movementDistance;

    private int armorValue = 10;

    private int attackDistance; //...?
}
