using UnityEngine;

public class Pickup_SpeedBoost : Pickup
{
    public PowerUp_MoveSpeed pickup;

    public override void ApplyPowerup(PowerupManager PUM)
    {
        PUM.ApplyPowerup(pickup);
    }
}
