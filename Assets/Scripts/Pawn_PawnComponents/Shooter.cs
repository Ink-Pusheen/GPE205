using UnityEngine;

public abstract class Shooter : MonoBehaviour
{

    public Transform muzzleTransform;
    public GameObject bulletPrefab;
    public float fireRate; //How many bullets per second the tank can fire
    public float nextShootTime;

    public Pawn pawn;

    private void Awake()
    {
        pawn = GetComponent<Pawn>();
        nextShootTime = Time.time; //I can shoot now
    }

    public abstract void Fire();
    public abstract void Fire(float power);

}
