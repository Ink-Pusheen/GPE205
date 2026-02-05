using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ShooterTank : Shooter
{
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Fire()
    {
        if (Time.time > nextShootTime)
        {
            Fire(pawn.shootPower);
            nextShootTime = Time.time + (1/fireRate); //Inverses the formula from seconds per shot from shots per second
        }
        
    }

    public override void Fire(float power)
    {
        //Instantiate the bullet at the muzzle and rotation
        GameObject bullet = Instantiate(bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);

        //Push it forward
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(muzzleTransform.forward * power);
    }
}
