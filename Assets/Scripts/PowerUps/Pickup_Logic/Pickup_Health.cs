using System;
using UnityEngine;

[Serializable]
public class Pickup_Health : Pickup
{
    public PowerUp_Health pickup;

    public override void ApplyPowerup(PowerupManager PUM, GameObject pickupHost)
    {
        bool fullHealth = pickup.CanHeal(PUM.pawn);
        Debug.Log("Colliding tank is full health: " + fullHealth);
        if (!fullHealth)
        {
            //Play the collect sound
            if (audioPlayer != null)
            {
                audioPlayer.transform.SetParent(null, false);
                audioPlayer.PlaySoundOneShot(0, 0, 0);
                audioPlayer.QueueSelfDestruct(4);
            }

            PUM.ApplyPowerup(pickup);
            Destroy(pickupHost);
        }
    }
}
