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
}
