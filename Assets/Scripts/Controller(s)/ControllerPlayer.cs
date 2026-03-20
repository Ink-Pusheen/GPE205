using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerPlayer : Controller
{
    //Input system
    [SerializeField] PlayerInput pI;

    InputAction rotateTank;
    InputAction moveTank;
    InputAction fire;
    InputAction flip;

    Rigidbody rbTank;

    //Attributes

    [SerializeField] float tankSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }

    public override void Start()
    {
        //Add controller to gamemanager
        GameManager.instance.players.Add(this);
    }

    private void OnDestroy()
    {
        //Remove this from the gamemanager
        GameManager.instance.players.Remove(this);
    }

    public override void MakeDecisions()
    {
        //TODO: Write the logic of the player here

        if (pI == null) return;

        if (rotateTank.IsPressed())
        {
            float rotation = rotateTank.ReadValue<float>();
            Vector3 rotateInput = new Vector3(rotation, 0, 0);

            pawn.Rotate(rotateInput);
        }

        if (moveTank.IsPressed())
        {
            float moveVal = moveTank.ReadValue<float>();
            Vector3 directionInput = new Vector3(0, 0, moveVal);
            pawn.Move(directionInput);
        }

        if (fire.WasPressedThisFrame())
        {
            pawn.Shoot(pawn.shootPower);
        }

        if (flip.WasPressedThisFrame())
        {
            pawn.Flip();
        }
    }

    public override void SetupControls()
    {
        rbTank = pawn.rb;

        pI = GetComponent<PlayerInput>(); //Gets the player Input and actions

        rotateTank = pI.actions.FindAction("Rotate");

        moveTank = pI.actions.FindAction("Movement");

        fire = pI.actions.FindAction("Fire");

        flip = pI.actions.FindAction("Flip");

        //Enable inputs
        pI.enabled = true;
    }
}
