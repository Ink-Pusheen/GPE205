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

    public Transform playerTarget;

    public float fleeDistance;

    public float hearingDistance = 10f;
    public float visionDistance = 10f;
    public float fovAngle = 60;


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

    public bool CanSee(GameObject target)
    {
        RaycastHit hit;

        //TODO: Field of view check


        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;
        if (Physics.Raycast(pawn.transform.position, vectorToTarget, out hit, visionDistance))
        {
            if (hit.collider.gameObject == target) return true;
        }

        //Else return false
        return false;
    }

    public bool CanHear(GameObject target)
    {
        //Check if target has "NoiseMaker"
        NoiseMaker targetNoiseMaker = target.GetComponent<NoiseMaker>();
        if(targetNoiseMaker == null) return false;

        //If yes, is there ongoing noise? (>0)
        if (targetNoiseMaker.noiseVolume > 0)
        {
            //If so, is the distance between the two centers smaller than the two radii added together?
            float totaleDistance = Vector3.Distance(target.transform.position, pawn.transform.position);

            if (totaleDistance <= targetNoiseMaker.noiseVolume + hearingDistance) return true;
        }

        //Otherwise, return false
        return false;
    }

    public void DoChase()
    {
        //Find the vector to the player
        Vector3 targetAngle = playerTarget.position - pawn.transform.position;

        //Move  the calculated direction
        pawn.Move(targetAngle);
    }

    public void DoFlee()
    {
        //TODO: Whatever is in case Flee
        //Find a vector to the player
        Vector3 vectorToTarget = pawn.transform.position - playerTarget.position;

        float distanceToPlayer = vectorToTarget.magnitude; //Distance between

        //Reversal
        vectorToTarget = -vectorToTarget;

        //Find the distance to flee
        vectorToTarget.Normalize();

        float percentOfFleeing = distanceToPlayer / fleeDistance;
        percentOfFleeing = Mathf.Clamp01(percentOfFleeing);
        float flippedPercentOfFleeing = 1 - percentOfFleeing;
        float newFleeDistance = flippedPercentOfFleeing * fleeDistance;

        Vector3 targetPos = pawn.transform.position + (vectorToTarget * newFleeDistance);
    }

    public void FindPickup()
    {
        //TODO: Find the nearest pickup
    }
}
