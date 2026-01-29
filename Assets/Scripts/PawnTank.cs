using UnityEngine;

public class PawnTank : Pawn
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        //Save tank to gamemanager
        GameManager.instance.tanks.Add(this);

        //Do what all pawns do
        base.Start();
    }

    private void OnDestroy()
    {
        //Remove tank fro gamemanager
        GameManager.instance.tanks.Remove(this);
    }

    public override void Move(Vector3 moveDirection)
    {
        mover.Move(moveDirection);
    }

    public override void Rotate(Vector3 rotateDirection)
    {
        mover.Rotate(rotateDirection);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
