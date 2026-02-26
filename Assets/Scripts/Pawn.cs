using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    [HideInInspector] public Controller controller;
    protected Mover mover;
    [HideInInspector] public Health health;

    public Rigidbody rb;

    public float moveSpeed;
    public float rotationSpeed;

    public float shootPower;

    public abstract void Move(Vector3 moveDirection);
    public abstract void Rotate(Vector3 rotateDirection);
    public abstract void Flip();
    public abstract void Shoot(float power);
    public abstract void RotateTowards(Vector3 position);

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();

        mover = GetComponent<Mover>();

        health = GetComponent<Health>();
    }

    void Awake()
    {
        
    }

    

}
