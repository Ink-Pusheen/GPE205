using UnityEngine;
using UnityEngine.InputSystem;

public class TmpTankController : MonoBehaviour
{
    //Input system
    PlayerInput pI;

    InputAction rotateTank;
    InputAction moveTank;

    Rigidbody rbTank;

    //Attributes

    [SerializeField] float tankSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rbTank = GetComponent<Rigidbody>();

        pI = GetComponent<PlayerInput>(); //Gets the player Input

        rotateTank = pI.actions.FindAction("Rotate");

        moveTank = pI.actions.FindAction("Movement");

        Debug.Log(rotateTank.name + " " + moveTank.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateTank.IsPressed())
        {
            float rotation = rotateTank.ReadValue<float>();

            transform.Rotate(0, rotation, 0);
            //Debug.Log("REEEEE");
        }

        if (moveTank.IsPressed())
        {
            float moveVal = moveTank.ReadValue<float>();

            rbTank.linearVelocity = transform.TransformDirection((Vector3.forward * moveVal) *tankSpeed);
        }

    }
}
