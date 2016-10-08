//Help from: http://stackoverflow.com/questions/730268/unique-random-string-generation

using UnityEngine;
using System;

public class BaseMinion
{
    private string minionID;

    private string minionType;
    private string minionDescription;

    private int health;
    private int attackTime;
    private int attackStrength;

    private int movementTime;
    private int movementDistance;

    private int armorValue;

    public BaseMinion()
    {
        Guid g = Guid.NewGuid();
        string GuidString = Convert.ToBase64String(g.ToByteArray());
        GuidString = GuidString.Replace("=", "");
        GuidString = GuidString.Replace("+", "");
        minionID = GuidString;
    }
}
