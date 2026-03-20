using UnityEngine;

public abstract class Mover : MonoBehaviour
{

    public abstract void Move(Vector3 moveInput);
    public abstract void Rotate(Vector2 rotateInput);
    public abstract void RotateTowards(Vector3 position);
}
