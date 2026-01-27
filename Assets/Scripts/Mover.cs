using UnityEngine;

public abstract class Mover : MonoBehaviour
{

    public abstract void Move(Vector2 moveInput);
    public abstract void Rotate(Vector2 rotateInput);
}
