using System;
using UnityEngine;

[Serializable]
public class PowerUp_Score : PowerUp
{
    public override void Apply(Pawn target)
    {
        target.controller.scoredPoints += (50 * UnityEngine.Random.Range(2, 5));

        target.controller.updateScore();
    }

    public override void Remove(Pawn target)
    {
        //Do nothing as it is not meant to be added to the list
    }
}
