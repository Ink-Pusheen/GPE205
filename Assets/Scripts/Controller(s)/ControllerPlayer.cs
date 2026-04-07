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
    InputAction devDamage;

    Rigidbody rbTank;

    //Camera
    private CameraScript cam;

    //Attributes

    [SerializeField] float tankSpeed;

    public override void Awake()
    {
        base.Awake();

        lives = 3;

        //Add controller to gamemanager
        GameManager.instance.players.Add(this);

        //Get the player Camera
        cam = GetComponentInChildren<CameraScript>();
    }

    private void Start()
    {
        updateHealth();
    }

    private void OnDestroy()
    {
        //Remove this from the gamemanager
        GameManager.instance.players.Remove(this);
    }

    public override void MakeDecisions()
    {
        //TODO: Write the logic of the player here
        if (pawn == null) return;

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

        if (devDamage.WasPressedThisFrame())
        {
            pawn.health.TakeDamage(5);
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

        devDamage = pI.actions.FindAction("DevDamage");

        //Enable inputs
        pI.enabled = true;

        //Sets up the camera
        SetupCamera();
    }

    public void SetupCamera()
    {
        cam.PawnToFollow = pawn.gameObject; //Set the target to follow

        cam.transform.rotation = Quaternion.Euler(10, 0, 0); //Set base rotation

        cam.SetPosition(); //Set the position and rotation
        cam.transform.SetParent(pawn.transform, false);
    }
}
