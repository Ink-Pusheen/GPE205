using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public enum AIState
{
    ChooseRoamDirection, Roam, Attack, TurnAndShoot, Flee, Chase, Idle, Patrol, Rotate
}

public class ControllerAI : Controller
{
    [Header("Variables")]

    [SerializeField] protected Quaternion roamDirection = Quaternion.identity;

    protected float transitionChangeTime;

    [SerializeField] protected AIState currentState = AIState.Roam;

    

    public float fleeDistance;

    public float hearingDistance = 10f;
    public float visionDistance = 10f;
    public float fovAngle = 60;

    public GameObject[] roamPoints;

    public override void Awake()
    {
        //Save our initial transition time
        transitionChangeTime = Time.deltaTime; //Debug.Log(transitionChangeTime);

        //Temp testing
        //Possess(this.pawn);

        
    }

    private void Start()
    {
        //Add this to the AI List
        GameManager.instance.aIs.Add(this);
    }

    public void ChangeState(AIState newState)
    {
        //Save the time we changed states
        transitionChangeTime = Time.time;

        //Change the current state
        currentState = newState;
    }

    public override void MakeDecisions()
    {
        //throw new System.NotImplementedException();
    }

    public override void SetupControls()
    {
        //throw new System.NotImplementedException();
    }

    public bool CanMoveForward(float distance)
    {
        //TODO: Raycast forward for the distance it'll move in one frame draw
        //TODO: If it hits something, return false, else return true;
        RaycastHit hit;

        //TODO: Field of view check


        Vector3 vectorToTarget = pawn.transform.forward;
        if (Physics.Raycast(pawn.transform.position + new Vector3(0, 0.25f, 0), vectorToTarget, out hit, distance))
        {
            if (hit.collider.gameObject != null && hit.collider.gameObject != this.pawn)
            {
                //Debug.Log(hit.collider.gameObject.name);
                return false;
            }
        }

        //Else return false
        return true;
    }

    public bool IsObjectInRange(GameObject objectToCheck, float range)
    {
        //Find the distance between the target and self pawn
        //If that is < range, return true, else return false

        //Vector3 offset = objectToCheck.transform.position - transform.position;
        float dist = Vector3.Distance(objectToCheck.transform.position, pawn.transform.position);

        if (dist < range)
        {
            //Debug.Log(Vector3.Distance(objectToCheck.transform.position, transform.position));
            return true;
        }
        Debug.Log(dist);
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
            if (hit.collider.gameObject == target)
            {
                Debug.Log("Player");
                return true;
            }
        }
        else
        {
            Debug.Log(hit.collider.name);
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

    public void DoFlee()
    {
        //TODO: Whatever is in case Flee
        //Find a vector to the player
        Vector3 vectorToTarget = pawn.transform.position - playerTarget.transform.position;

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

        pawn.Move(targetPos);
    }

    public void FindPickup()
    {
        //TODO: Find the nearest pickup
    }

    public override void Possess(Pawn pawnToPossess)
    {
        base.Possess(pawnToPossess);

        tankUI = pawn.GetComponentInChildren<UIBase>();
    }
}
