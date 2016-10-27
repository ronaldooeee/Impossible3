//Help from: http://stackoverflow.com/questions/730268/unique-random-string-generation

using UnityEngine;
using System;

public class BaseMinion : object
{
    private string minionID;

    private string minionType;
    private string minionDescription;

    public int health;
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

    public static void removeMinion(object minion)
    {
        UnityEngine.GameObject.Destroy((GameObject) minion);
    }

    //Does this work?
    private void attack(BaseMinion minion)
    {
        minion.health -= attackStrength;
        if (minion.health <= 0)
        {
            BaseMinion.removeMinion(minion);
        }
    }
}
