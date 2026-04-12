using UnityEngine;

public class ControllerAI_SmokeScreen : ControllerAI
{
    public override void MakeDecisions()
    {
        if (playerTarget == null) ChangeState(AIState.Idle); //Default idle if no players exist

        if (pawn == null)
        {
            //Remove it from the Gamemanager list
            GameManager.instance.RemoveAiTankController(this);

            //Destroy this controller
            Destroy(gameObject);

            return;
        }

        //Look at the current state
        switch (currentState)
        {
            case AIState.ChooseRoamDirection:
                //Rotate towards a certain direction if player is not in sight

                DoChooseRoamDirection();

                //Check for transitions

                if (IsObjectInRange(playerTarget, visionDistance) && CanSee(playerTarget.gameObject))
                {
                    ChangeState(AIState.TurnAndShoot);
                }

                if (!CanMoveForward(2)) ChangeState(AIState.ChooseRoamDirection);

                ChangeState(AIState.Rotate);

                break;

            case AIState.Roam:
                //Rotate to that direction and move forward

                DoRoam();

                //Check for transitions
                if (IsObjectInRange(playerTarget, visionDistance) && CanSee(playerTarget.gameObject))
                {
                    ChangeState(AIState.TurnAndShoot);
                }

                if (Time.time > transitionChangeTime + 5)
                {
                    ChangeState(AIState.ChooseRoamDirection);
                }

                if (!CanMoveForward(2)) ChangeState(AIState.ChooseRoamDirection);

                break;

            case AIState.Attack:
                //Turn towards the player and fire

                DoAttack();

                //Check for transitions

                if (!CanSee(playerTarget.gameObject)) ChangeState(AIState.Roam);

                break;

            case AIState.TurnAndShoot:
                //Turn towards the player then attack

                DoTurnAndShoot();

                //Check for transitions

                if (!CanSee(playerTarget.gameObject)) ChangeState(AIState.ChooseRoamDirection);

                if (!IsObjectInRange(playerTarget, visionDistance)) ChangeState(AIState.Roam);

                break;

            case AIState.Flee:
                //Flee away from the player

                DoFlee();

                //Check for transitions

                if (!IsObjectInRange(playerTarget, 5)) ChangeState(AIState.Idle);

                break;

            case AIState.Chase:
                //
                DoChase();

                //Check for transitions
                if (IsObjectInRange(playerTarget, visionDistance)) ChangeState(AIState.TurnAndShoot);

                if (!CanSee(playerTarget.gameObject)) ChangeState(AIState.Roam);

                break;

            case AIState.Idle:
                //Do Nothing

                DoIdle();

                //Check for transitions

                if (playerTarget != null && IsObjectInRange(playerTarget, visionDistance) && CanSee(playerTarget.gameObject))
                    ChangeState(AIState.TurnAndShoot);

                if (Time.time > transitionChangeTime + 5) ChangeState(AIState.ChooseRoamDirection);

                break;

            case AIState.Patrol:
                //Patrol

                DoPatrol();

                //Check for transitions

                if (IsObjectInRange(playerTarget, visionDistance) && CanSee(playerTarget.gameObject))
                    ChangeState(AIState.TurnAndShoot);

                if (!CanMoveForward(2)) ChangeState(AIState.ChooseRoamDirection);

                break;

            case AIState.Rotate:
                //Rotate the tank

                DoQuaternionRotate();

                //Check for Transitions
                if (IsObjectInRange(playerTarget, visionDistance) && CanSee(playerTarget.gameObject))
                    ChangeState(AIState.TurnAndShoot);

                if (Time.time > transitionChangeTime + 2) ChangeState(AIState.Roam);

                break;
        }

        Debug.DrawRay(pawn.transform.position + new Vector3(0, 0.25f, 0), pawn.transform.forward, Color.red, 2);
    }

    public void DoChooseRoamDirection()
    {
        //TODO: Whatever is in case ChooseRoamDirection
        //Set a random rotation
        Quaternion newRotation = Quaternion.Euler(transform.position.x, Random.Range(0, 360), transform.position.z);

        roamDirection = newRotation;
    }

    public void DoRoam()
    {
        //TODO: Whatever is in case Roam
        //Move forward after selecting a direction
        pawn.Move(Vector3.forward);
    }

    public void DoAttack()
    {
        //TODO: Whatever is in case Attack
        //Shoot
        pawn.Shoot(pawn.shootPower);
    }

    public void DoTurnAndShoot()
    {
        //TODO:Whatever is in case TurnAndShoot
        //Rotate towards player
        pawn.RotateTowards(playerTarget.position);

        //Shoot
        pawn.Shoot(pawn.shootPower);
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
        pawn.Rotate(new Vector3(0, 1, 0));
    }

    public void DoPatrol()
    {
        //TODO: Whatever is in case Patrol
        //Move forward after selecting a direction
        pawn.Move(Vector3.forward);
    }

    public void DoQuaternionRotate()
    {
        Quaternion rotateQuaternion = Quaternion.Euler(0, roamDirection.eulerAngles.y, 0);
        pawn.QuaternionRotateTowards(rotateQuaternion);
    }
}
