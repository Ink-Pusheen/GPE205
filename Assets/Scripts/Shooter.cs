using UnityEngine;

public abstract class Shooter : MonoBehaviour
{

    public Transform muzzleTransform;
    public GameObject bulletPrefab;

    public Pawn pawn;

    private void Start()
    {
        pawn = GetComponent<Pawn>();
    }

    public abstract void Fire();

}
