using System;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public Pawn pawn;
    public UIBase tankUI;

    public int lives;

    public virtual void Awake()
    {
        tankUI = GetComponent<UIBase>();
    }

    public void Update()
    {
        MakeDecisions();
    }

    public abstract void MakeDecisions();

    public abstract void SetupControls();
    public virtual void Possess(Pawn pawnToPossess)
    {
        pawnToPossess.controller = this;
        this.pawn = pawnToPossess;
        //Debug.Log(pawnToPossess);
    }

    public void Unpossess()
    {
        pawn.controller = null;
        pawn = null;
    }

    public void updateHealth()
    {
        if (pawn == null) throw new ArgumentException("Error, pawn null");
        if (tankUI == null) throw new ArgumentException("Error, UI null");
        //tankUI.updateHealthBar(pawn.health.currentHealth, pawn.health.maxHealth);
        tankUI.updateHealthBar(1, 1);
    }
}
