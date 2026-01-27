using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    [HideInInspector] public Controller controller;
    protected Mover mover;

    public Rigidbody rb;

    public float moveSpeed;
    public float rotationSpeed;

    public abstract void Move(Vector3 moveDirection);
    public abstract void Rotate(Vector3 rotateDirection);

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        mover = GetComponent<Mover>();
    }

}
