using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerPlayer : Controller
{
    //Input system
    [SerializeField] PlayerInput pI;

    InputAction rotateTank;
    InputAction moveTank;
    InputAction fire;

    Rigidbody rbTank;

    //Attributes

    [SerializeField] float tankSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }

    public override void MakeDecisions()
    {
        //TODO: Write the logic of the player here

        if (rotateTank.IsPressed())
        {
            float rotation = rotateTank.ReadValue<float>();
            Vector3 rotateInput = new Vector3(rotation, 0, 0);

            pawn.Rotate(rotateInput);
            //pawn.gameObject.transform.Rotate(0, rotation, 0);
            //Debug.Log("REEEEE");
        }

        if (moveTank.IsPressed())
        {
            float moveVal = moveTank.ReadValue<float>();
            Vector3 directionInput = new Vector3(0, moveVal, 0);

            pawn.Move(directionInput);
            //rbTank.linearVelocity = pawn.gameObject.transform.TransformDirection((Vector3.forward * moveVal));
        }

        if (fire.WasPressedThisFrame())
        {
            Debug.Log("IMMA FIRIN MY LAZAR");
        }
    }

    public override void SetupControls()
    {
        rbTank = pawn.rb;

        pI = GetComponent<PlayerInput>(); //Gets the player Input and actions

        rotateTank = pI.actions.FindAction("Rotate");

        moveTank = pI.actions.FindAction("Movement");

        fire = pI.actions.FindAction("Fire");
    }
}
