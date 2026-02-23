using UnityEngine;

public enum States
{

}

public class ControllerAI_Roamer : ControllerAI
{
    public override void MakeDecisions()
    {
        //Look at the current state
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

    public void DoChooseRoamDirection()
    {
        //TODO: Whatever is in case ChooseRoamDirection
    }

    public void DoRoam()
    {
        //TODO: Whatever is in case Roam
    }

    public void DoAttack()
    {
        //TODO: Whatever is in case Attack
    }

    public void DoTurnAndShoot()
    {
        //TODO:Whatever is in case TurnAndShoot
    }

    

    public void DoChase()
    {
        //TODO: Whatever is in case Chase
        //Turn towards chase target
        pawn.RotateTowards(playerTarget.position);

        //Move forward
        pawn.Move(new Vector3(0, 0, 1));
    }

    public void DoIdle()
    {
        //TODO: Whatever is in case idle
    }

    public void DoPatrol()
    {
        //TODO: Whatever is in case Patrol
    }
}
