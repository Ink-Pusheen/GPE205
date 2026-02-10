using UnityEngine;

public enum AIState
{
    ChooseRoamDirection, Roam, Attack, TurnAndShoot, Flee, Chase, Idle, Patrol
}

public class ControllerAI : Controller
{
    [Header("Variables")]

    private Quaternion roamDirection = Quaternion.identity;

    private float transitionChangeTime;

    protected AIState currentState = AIState.Roam;


    public override void Start()
    {
        //Save our initial transition time
        transitionChangeTime = Time.deltaTime;
    }

    public void ChangeState(AIState newState)
    {
        //Change the current state
        currentState = newState;

        //Save the time we changed states
        transitionChangeTime = Time.time;
    }

    public override void MakeDecisions()
    {
        throw new System.NotImplementedException();
    }

    public override void SetupControls()
    {
        throw new System.NotImplementedException();
    }

    public bool CanMoveForward(float distance)
    {
        //TODO: Raycast forward for the distance it'll move in one frame draw
        //TODO: If it hits something, return false, else return true;
        //Ray ray = new Ray();

        return true;
    }

    public bool IsObjectInRange(Transform objectToCheck, float range)
    {
        //Find the distance between the target and self pawn
        //If that is < range, return true, else return false

        if (Vector3.Distance(objectToCheck.position, transform.position) < range) return true;

        return false; //No need for an else here as the prior 'return' already exited the function.
    }

    public bool IsRoamDirectionChosen()
    {
        //TODO: If yes, return true, else return false

        if(roamDirection != Quaternion.identity) return true;

        return false;
    }

    public bool HasTimeElapsed(float seconds)
    {
        //If the current time minus the time we last changed is > the time we are waiting
        if (Time.time - transitionChangeTime > -seconds) return true;

        //Otherwise, the time has not yet passed
        return false;
    }

}
