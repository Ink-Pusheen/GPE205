using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] public List<PowerUp> powerups = new List<PowerUp>();
    private Pawn pawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pawn = GetComponent<Pawn>();
    }

    public void Update()
    {
        //TODO: Check for expired powerups and remove them
        //TODO: WAY LATER - Not in this class, but would be fun to force hehe - check for effects that are 'over time'
    }

    public void ApplyPowerup(PowerUp powerup)
    {
        //Apply the powerups effects
        powerup.Apply(pawn);

        //Add it to the list
        powerups.Add(powerup);
    }

    public void RemovePowerup(PowerUp powerup)
    {
        //Remove the powerups effects
        powerup.Remove(pawn);

        //Remove it form the list
        powerups.Remove(powerup);
    }
}
