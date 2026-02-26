using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] List<PowerUp> powerups = new List<PowerUp>();
    private Pawn pawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pawn = GetComponent<Pawn>();
    }

    public void Update()
    {
        //Decay time from powerups
        UpdatePowerupLifeSpan();

        //Check for expired powerups and remove them
        CheckForExpiredPowerups();

        //TODO: WAY LATER - Not in this class, but would be fun to force hehe - check for effects that are 'over time'
    }

    public void UpdatePowerupLifeSpan()
    {
        foreach (var powerup in powerups)
        {
            if (powerup.lifeSpan == -1) continue; //Infinite powerup

            powerup.lifeSpan -= Time.deltaTime;
        }
    }

    public void CheckForExpiredPowerups()
    {
        //Create a temporary list to hold powerups that need to be removed
        List<PowerUp> powerupsToRemove = new List<PowerUp>();

        foreach (PowerUp powerup in powerups)
        {
            if (powerup.lifeSpan == -1) continue; //Infinite powerup

            if (powerup.lifeSpan <= 0)
            {
                powerupsToRemove.Add(powerup);
            }
        }

        //Then remove them from the main list
        //Preventinng errors due removal from the main list during checks
        foreach (PowerUp powerup in powerupsToRemove)
        {
            RemovePowerup(powerup);
        }
    }

    public void ApplyPowerup(PowerUp powerup)
    {
        //Apply the powerups effects
        powerup.Apply(pawn);

        //Add it to the list
        if(powerup.lifeSpan != -1) powerups.Add(powerup);
    }

    public void RemovePowerup(PowerUp powerup)
    {
        //Remove the powerups effects
        powerup.Remove(pawn);

        //Remove it form the list
        powerups.Remove(powerup);
    }
}
