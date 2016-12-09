using UnityEngine;
using System.Collections;

public class Abilities : MonoBehaviour
{

    public static bool[] unlockedAbilities = new bool[6] { false, false, false, false, false, false };
    public static bool[] displayed = new bool[6] { false, false, false, false, false, false };

    public virtual void RegAttack(Unit selectedUnit, Unit selectedTarget) { }

    public virtual void Ability1(Unit selectedUnit, Unit selectedTarget) { }

    public virtual void Ability2(Unit selectedUnit, Unit selectedTarget) { }

    public virtual void Ability3(Unit selectedUnit, Unit selectedTarget) { }

    public virtual void Ability4(Unit selectedUnit, Unit selectedTarget) { }

    public virtual void Ability5(Unit selectedUnit, Unit selectedTarget) { }

    public virtual void Ability6(Unit selectedUnit, Unit selectedTarget) { }

    public void OverlaySelect(Unit selectedUnit, int highlight, int straightrange, int diagrange, int circrange) //attack = 0 -> ability, attack=1 -> attack
    {
        int[] tempRanges = { selectedUnit.straightAttackRange, selectedUnit.diagAttackRange, selectedUnit.circAttackRange };
        BoardHighlights.Instance.Hidehighlights();
        bool[,] moves;
        selectedUnit.straightAttackRange = straightrange;
        selectedUnit.diagAttackRange = diagrange;
        selectedUnit.circAttackRange = circrange;
        if (highlight == 0)
        {
            moves = selectedUnit.PossibleAbility();
            BoardHighlights.Instance.HighlightAllowedAbilities(moves);
        }
        if (highlight == 1)
        {
            moves = selectedUnit.PossibleAttack();
            BoardHighlights.Instance.HighlightAllowedAttacks(moves);
        }
        selectedUnit.straightAttackRange = tempRanges[0];
        selectedUnit.diagAttackRange = tempRanges[1];
        selectedUnit.circAttackRange = tempRanges[2];
    }

}
