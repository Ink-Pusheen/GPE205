using UnityEngine;
using UnityEngine.EventSystems;

public class MoverTank : Mover
{
    private Pawn pawn;

    private void Start()
    {
        pawn = GetComponent<Pawn>();
    }

    public override void Move(Vector3 moveInput)
    {
        Vector3 target = pawn.rb.position + transform.TransformDirection(moveInput) * pawn.moveSpeed * Time.fixedDeltaTime;

        pawn.rb.MovePosition((target));
    }

    public override void Rotate(Vector2 rotateInput)
    {
        transform.Rotate(0, rotateInput.x * pawn.rotationSpeed * Time.deltaTime, 0);
    }

    public override void RotateTowards(Vector3 position)
    {
        //Find the target vector to target
        Vector3 target = position - transform.position;

        //Find the quaternion look rotation for the vector
        Quaternion rotation = Quaternion.LookRotation(target);

        //Rotate over time towards new rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, pawn.rotationSpeed * Time.deltaTime);
    }
}
