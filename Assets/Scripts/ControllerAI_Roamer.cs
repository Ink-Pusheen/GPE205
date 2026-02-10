using UnityEngine;

public class ControllerAI_Roamer : ControllerAI
{
    public override void MakeDecisions()
    {
        switch (currentState)
        {
            case AIState.ChooseRoamDirection:
                //
                //Check for transitions
                break;

            case AIState.Roam:
                //Rotate to that direction and move forward
                //Check for transitions
                break;

            case AIState.Attack:
                //Turn towards the player and fire
                //Check for transitions
                if (!CanMoveForward(5)) ChangeState(AIState.Roam);
                break;

            case AIState.TurnAndShoot:
                //
                //Check for transitions
                break;

            case AIState.Flee:
                //
                //Check for transitions
                break;

            case AIState.Chase:
                //
                //Check for transitions
                break;

            case AIState.Idle:
                //Do Nothing
                //Check for transitions
                break;

            case AIState.Patrol:
                //
                //Check for transitions
                break;
        }
    }

    public void DoIdle()
    {
        //TODO: Whatever is in case idle
    }
}
