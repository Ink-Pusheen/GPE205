using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public Pawn pawn;

    public void Update()
    {
        MakeDecisions();
    }

    public abstract void MakeDecisions();

    public abstract void SetupControls();
    public void Possess(Pawn pawnToPossess)
    {
        pawnToPossess.controller = this;
        this.pawn = pawnToPossess;
    }

    public void Unpossess()
    {
        pawn.controller = null;
        pawn = null;
    }
}
