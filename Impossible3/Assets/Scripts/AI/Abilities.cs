using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Abilities : MonoBehaviour {

    public bool[] unlockedAbilities;

    public virtual void RegAttack(Unit selectedUnit, Unit selectedTarget) { }

    public virtual void Ability1(Unit selectedUnit, Unit selectedTarget) { }

    public virtual void Ability2(Unit selectedUnit, Unit selectedTarget) { }

    public virtual void Ability3(Unit selectedUnit, Unit selectedTarget) { }

    public virtual void Ability4(Unit selectedUnit, Unit selectedTarget) { }

    public virtual void Ability5(Unit selectedUnit, Unit selectedTarget) { }

    public virtual void Ability6(Unit selectedUnit, Unit selectedTarget) { }

    public void OverlaySelect(Unit selectedUnit, int attack, int minrange, int maxrange) //attack = 0 -> ability, attack=1 -> attack
    {
		int[] tempRanges = { selectedUnit.minAttackRange, selectedUnit.maxAttackRange };
        BoardHighlights.Instance.Hidehighlights();
		HashSet<Coord>[] moves;
        selectedUnit.minAttackRange = minrange;
        selectedUnit.maxAttackRange = maxrange;
        if (attack == 0) {
            moves = selectedUnit.PossibleAbility();
            BoardHighlights.Instance.HighlightAllowedAbilities(moves);
        }
        if (attack == 1) {
            moves = selectedUnit.PossibleAttack();
            BoardHighlights.Instance.HighlightAllowedAttacks(moves);
        }
		selectedUnit.minAttackRange = tempRanges[0];
		selectedUnit.maxAttackRange = tempRanges[1];
    }

}
