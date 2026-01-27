using UnityEngine;
using UnityEngine.EventSystems;

public class MoverTank : Mover
{
    private Pawn pawn;

    private void Start()
    {
        pawn = GetComponent<Pawn>();
    }

    public override void Move(Vector2 moveInput)
    {
        transform.forward += new Vector3(moveInput.x, 0, moveInput.y) * (pawn.moveSpeed * Time.deltaTime);
    }

    public override void Rotate(Vector2 rotateInput)
    {
        transform.Rotate(0, rotateInput.x * pawn.rotationSpeed * Time.deltaTime, 0);
    }
}
