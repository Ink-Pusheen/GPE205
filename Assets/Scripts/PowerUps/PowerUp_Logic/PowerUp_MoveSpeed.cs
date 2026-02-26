using System;

[Serializable]
public class PowerUp_MoveSpeed : PowerUp
{
    public float speedBoostAmount;

    public override void Apply(Pawn target)
    {
        //Increase target move speed
        target.moveSpeed += speedBoostAmount;
    }

    public override void Remove(Pawn target)
    {
        //Decrease target move speed
        target.moveSpeed -= speedBoostAmount;
    }
}
