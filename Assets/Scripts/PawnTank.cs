using UnityEngine;

public class PawnTank : Pawn
{
    public override void Move(Vector3 moveDirection)
    {
        mover.Move(moveDirection);
    }

    public override void Rotate(Vector3 rotateDirection)
    {
        mover.Rotate(rotateDirection);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
