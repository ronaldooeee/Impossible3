//Help from: http://stackoverflow.com/questions/730268/unique-random-string-generation
//Help from: http://stackoverflow.com/questions/3975290/produce-a-random-number-in-a-range-using-c-sharp

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
        this.minionID = GuidString;
    }

    public static void removeMinion(object minion)
    {
        UnityEngine.GameObject.Destroy((GameObject) minion);
    }

    public static int mitigateAttack(int attackStrength, int armorValue)
    {
        System.Random r = new System.Random();
        int armorDeduct = r.Next(0, armorValue);
        attackStrength -= armorDeduct;
        return attackStrength;
    }

    public int getArmorValue()
    {
        return this.armorValue;
    }

    public int getMovementTime()
    {
        return this.movementTime;
    }

    public int getMovementDistance()
    {
        return this.movementDistance;
    }

    public int getAttackStrength()
    {
        return this.attackStrength;
    }

    public int getAttackTime()
    {
        return this.attackTime;
    }

    public void attack(BaseMinion minion)
    {
        int attack = BaseMinion.mitigateAttack(this.attackStrength, minion.getArmorValue());
        minion.health -= attack;
        if (minion.health <= 0)
        {
            BaseMinion.removeMinion(minion);
        }
    }
}
