using System;
using UnityEngine;

[Serializable]
public class PowerUp_Health : PowerUp
{
    public float healthToHeal;
    public override void Apply(Pawn target)
    {
        //TODO: Heal Player
        Debug.Log("Healed");
    }

    public override void Remove(Pawn target)
    {
        throw new System.NotImplementedException();
    }
}
