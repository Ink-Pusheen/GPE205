using System;

[Serializable]
public class PowerUp_Health : PowerUp
{
    public float healthToHeal;
    public override void Apply(Pawn target)
    {
        //TODO: Heal Player
        
        //Check if the pawn we are "healing" has a health component
        if (target.health != null)
        {
            //Call to the health component to heal the specified amount
            target.health.Heal(healthToHeal);
        }
    }

    public override void Remove(Pawn target)
    {
        throw new System.NotImplementedException();
    }
}
