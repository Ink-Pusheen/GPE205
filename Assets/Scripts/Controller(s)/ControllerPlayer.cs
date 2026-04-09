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

    public int playerIndex;

    public override void Awake()
    {
        base.Awake();

        lives = 3;

        //Add controller to gamemanager
        GameManager.instance.players.Add(this);

        //Get the player Camera
        cam = GetComponentInChildren<CameraScript>();

        //Get the player input
        pI = GetComponent<PlayerInput>(); //Gets the player Input and actions
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
            pawn.health.TakeDamage(5, null);
        }
    }

    public override void SetupControls()
    {
        rbTank = pawn.rb;

        pI.SwitchCurrentActionMap(name);

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

        //Multiplayer changes
        if (MapGenerator.instance.mapLogic.playerMultiplayer)
        {
            switch (name)
            {
                case "Player1":

                    cam.GetComponent<Camera>().rect = new Rect(0, 0.5f, 1, 0.5f);

                    break;

                case "Player2":

                    cam.GetComponent<Camera>().rect = new Rect(0, 0, 1, 0.5f);

                    break; 
            }
        }
    }
}
