using UnityEngine;

public class Score_Pickup : Pickup
{
    public PowerUp_Score powerup;

    public override void ApplyPowerup(PowerupManager PUM, GameObject pickupHost)
    {
        //Play the collect sound
        if (audioPlayer != null)
        {
            audioPlayer.transform.SetParent(null, false);
            audioPlayer.PlaySoundOneShot(0, 0, 0);
            audioPlayer.QueueSelfDestruct(4);
        }

        PUM.ApplyPowerup(powerup);

        Destroy(pickupHost);
    }
}
