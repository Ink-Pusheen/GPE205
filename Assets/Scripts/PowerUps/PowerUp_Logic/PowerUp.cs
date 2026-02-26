using System;

[Serializable]
public abstract class PowerUp
{
    public float lifeSpan;

    public abstract void Apply(Pawn target);
    public abstract void Remove(Pawn target);
}
