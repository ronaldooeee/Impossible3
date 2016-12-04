using UnityEngine;
using System.Collections;

public class Abilities : MonoBehaviour {

    public bool[] unlockedAbilities;

    public virtual void RegAttack(Unit selectedUnit, Unit selectedTarget) { }

    public virtual void Ability1(Unit selectedUnit, Unit selectedTarget) { }

    public virtual void Ability2(Unit selectedUnit, Unit selectedTarget) { }

    public virtual void Ability3(Unit selectedUnit, Unit selectedTarget) { }

    public virtual void Ability4(Unit selectedUnit, Unit selectedTarget) { }

    public virtual void Ability5(Unit selectedUnit, Unit selectedTarget) { }

    public virtual void Ability6(Unit selectedUnit, Unit selectedTarget) { }

}
