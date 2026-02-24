using System;
using UnityEngine;

[Serializable]
public class Pickup_Health : Pickup
{
    public PowerUp_Health pickup;

    public override void ApplyPowerup(PowerupManager PUM)
    {
        PUM.ApplyPowerup(pickup);
    }
}
