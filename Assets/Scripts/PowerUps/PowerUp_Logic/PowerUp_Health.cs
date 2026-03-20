using System;

[Serializable]
public class PowerUp_Health : PowerUp
{
    public float healthToHeal;
    public override void Apply(Pawn target)
    {
        //Check if the pawn we are "healing" has a health component
        if (target.health != null)
        {
            //Don't heal if target health is full
            if (target.health.currentHealth == target.health.maxHealth) return;

            //Call to the health component to heal the specified amount
            target.health.Heal(healthToHeal);
        }
    }

    public override void Remove(Pawn target)
    {
        //Do nothing as this shouldn't be removed
    }

    public bool CanHeal(Pawn target)
    {
        return target.health.currentHealth == target.health.maxHealth;
    }
}
