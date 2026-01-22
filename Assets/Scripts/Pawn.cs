using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public Controller controller;

    public Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}
